using Google.Protobuf;
using ProtoMsg;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;


//协议格式：包体大小（4）+协议ID（4）+协议类型（4）+实际包体
public class ProtoMgr
{
    #region 单例
    private static ProtoMgr instance;

    //缓存池：将所有未粘包、拆包逻辑的数据存到这里
    List<byte> ReceiveCache = new List<byte>();

    //消息队列：协议ID+协议类型+实际包体
    Queue<byte[]> msgQueue = new Queue<byte[]>();

    //来源于消息队列，进行最后的解码操作
    Queue<byte[]> msgDecodeQueue = new Queue<byte[]>();

    //完整的报文长度：包体大小（4）+协议ID（4）+协议类型（4）+实际包体
    int packLength = 0;


    public static ProtoMgr Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new ProtoMgr();
            }
            return instance;
        }
    }
    #endregion

    /// <summary>
    /// 序列化
    /// </summary>
    /// <param name="bufferEntity">发送的消息实体</param>
    /// <returns></returns>
    public byte[] Serialize(BufferEntity bufferEntity)
    {
        //做安全检验：确保发送的协议 是预先定制好的
        if (PBConfig.Instance.CheckC2SPB(bufferEntity.id))
        {
            //序列化的操作 得到包体
            byte[] body = bufferEntity.message.ToByteArray();

            //打印需要发送的消息
            //Debug.Log("发送的pb实体:" + JsonMapper.ToJson(bufferEntity.message));
            try
            {
                //内存操作流程
                MemoryStream ms = new MemoryStream();
                BinaryWriter bw = new BinaryWriter(ms, Encoding.Default);

                //包体的长度
                bw.Write(BitConverter.GetBytes(body.Length));//包体长度
                bw.Write(BitConverter.GetBytes(bufferEntity.id));//消息编号
                bw.Write(BitConverter.GetBytes(bufferEntity.type));//消息传输类型
                //写入消息体
                bw.Write(body);
                byte[] data = ms.ToArray();

                bw.Close();//关闭内存操作流
                ms.Dispose();//释放清理
                return data;
            }
            catch (Exception e)
            {
                Debug.Log(e.Message);
                return null;
            }
        }
        else
        {
            //协议未在配置中 不合法 。。。
            Debug.Log("协议未在配置中 不合法");
            return null;
        }
    }


    /// <summary>
    /// 拆包的入口
    /// </summary>
    /// <param name="data">从网络接收到的数据</param>
    public void Deserialize(byte[] data)
    {

        //复制之后加入全局缓存池
        ReceiveCache.AddRange(data);

        //1.拆包的逻辑 获取到每条完整的数据
        DecoderPack();

        //拷贝到另一个队列中，进行最终的解码处理
        while (msgQueue.Count > 0)
        {
            msgDecodeQueue.Enqueue(msgQueue.Dequeue());
        }
        //msgQueue.Clear();
        //msgQueue = new Queue<byte[]>();

        //02.获取协议ID+协议类型+包体 实际的数据
        DecodeMsg();

        //反序列化的操作 是在广播事件之后，监听者在绑定的方法里去将body进行反序列化的
    }


    /// <summary>数据源：拆包逻辑</summary>
    private Queue<byte[]> DecoderPack()
    {
        //粘包可能导致两个情况:
        //1.单条数据包含了多条完整的数据 
        //2.或者 其中最后一条是不完整的 

        //另外 一条完整的数据包的格式是：
        //包体长度（4）+协议ID（4）+协议类型（4）+实际包体

        //拆包逻辑：将缓存池的数据写入内存流
        MemoryStream ms = new MemoryStream(ReceiveCache.ToArray());
        BinaryReader br = new BinaryReader(ms, Encoding.Default);
        //释放缓存池
        ReceiveCache.Clear();

        //网络数据都是通过byte[] 进行传输的，通过数据边界来确定数据的完整性



        //包体长度为0的逻辑（初始化默认为0)
        if (packLength == 0)
        {
            //数据的边界=从内存流中读取4个字节得到包体的长度，再加上协议ID（4）+协议类型（4）
            packLength = BitConverter.ToInt32(br.ReadBytes(4), 0) + 8;

        }

        //每次从流中读取数据，Position属性就会移动到读取的位置
        //以上读取了4个字节，那么br.BaseStream.Position=4

        //内存流中数据完整性的逻辑
        //完整性：内存中数据的长度-已读的4个，还大于或者等于上面计算的数据边界
        if (br.BaseStream.Length - br.BaseStream.Position >= packLength)
        {
            //读取完整的一条数据：协议ID+协议类型+实际包体
            byte[] buff = br.ReadBytes(packLength);
            packLength = 0;
            msgQueue.Enqueue(buff);

            //如果内存中还有未读取的数据，需要存储到缓存池中，参与下一次解码
            if ((br.BaseStream.Length - br.BaseStream.Position) > 0)
            {
                ReceiveCache.AddRange(br.ReadBytes((int)(br.BaseStream.Length - br.BaseStream.Position)));
                //如果池子的长度满足粘包与拆包逻辑（12）那么回调继续粘包与拆包的逻辑
                if (ReceiveCache.Count >= 12)
                {
                    DecoderPack();
                }
            }
        }
        //否则本次接收的数据并非完整的
        else
        {
            //从当前位置偏移0位继续读取
            br.BaseStream.Seek(0, SeekOrigin.Current);
            //数据不完整性的逻辑就是重新加入到缓存池中，等下次一起处理
            ReceiveCache.AddRange(br.ReadBytes((int)(br.BaseStream.Length - br.BaseStream.Position)));
        }

        //释放内存
        br.Close();
        ms.Close();
        if (ms != null) { ms.Dispose(); }
        return msgQueue;
    }

    /// <summary>解码：获取协议ID 协议类型 包体</summary>
    private void DecodeMsg()
    {
        //消息队列的数量大于0
        while (msgDecodeQueue.Count > 0)
        {
            BufferEntity bufferEntity = new BufferEntity();

            //出列 读取最先压入的数据
            byte[] buff = msgDecodeQueue.Dequeue();

            try
            {
                //因为这个buff是去掉了包头4个字节的 所以直接读取就可以了
                MemoryStream ms = new MemoryStream(buff);
                BinaryReader br = new BinaryReader(ms);
                bufferEntity.id = BitConverter.ToInt32(br.ReadBytes(4), 0);//byte[]->int
                bufferEntity.type = BitConverter.ToInt32(br.ReadBytes(4), 0);
                //包体
                bufferEntity.body = br.ReadBytes(buff.Length - 8);
            }
            catch (Exception e)
            {
                
                bufferEntity.id = 0;
                bufferEntity.type = 0;
                Debug.Log(e.Message);
            }

            //协议标志 channel 
            if (bufferEntity.id == 0)
            {
                //解码异常！
                return;
            }
            Debug.Log("收到消息，id:" + bufferEntity.id);
            //广播
            //SocketEvent.MsgBroadcast(bufferEntity);
            NetEvent.Instance.Dispatch(bufferEntity.id, bufferEntity);
        }
    }
}
