using Google.Protobuf;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BufferEntity 
{
    public int id = 0;//协议ID
    public int type = 0;//协议类型 proto xml json
    public byte[] body;//包体

    public IMessage message;//pb实体

    public BufferEntity()
    {

    }

    public BufferEntity(int id, int type, IMessage message)
    {
        this.id = id;
        this.type = type;
        this.message = message;
        this.body = message.ToByteArray();//序列化成byte数组
    }
}
