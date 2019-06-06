using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProtoMsg
{
    public class PBConfig: Singleton<PBConfig>
    {
       // S server  C client
        public  Dictionary<int, Type> PBC2SDic = new Dictionary<int, Type>();
        public  Dictionary<int, Type> PBS2CDic = new Dictionary<int, Type>();

        /// <summary>
        /// 初始化 两个字典
        /// </summary>
        public PBConfig()
        {
            //工具自动化生成
            C2SmsgInit();
            S2CmsgInit();
        }

        void C2SmsgInit() {
            
            PBC2SDic.Add(1000,typeof(UserRegisterC2S));
            PBC2SDic.Add(1001,typeof(UserLoginC2S));
            PBC2SDic.Add(1002,typeof(UserWechatLoginC2S));
            PBC2SDic.Add(1003,typeof(UserQuitC2S));
            PBC2SDic.Add(1100,typeof(ServerGetRecommendC2S));
            PBC2SDic.Add(1101,typeof(ServerSelectC2S));
            PBC2SDic.Add(1102,typeof(ServerGetCustomC2S));
            PBC2SDic.Add(1200,typeof(NewsGetLGC2S));
            PBC2SDic.Add(1300,typeof(RolesGetInfoC2S));
            PBC2SDic.Add(1400,typeof(HeroGetListC2S));
            PBC2SDic.Add(1401,typeof(HeroBuyC2S));
            PBC2SDic.Add(1500,typeof(LinkmanGetListC2S));
            PBC2SDic.Add(1501,typeof(LinkmanAddFriendC2S));
            PBC2SDic.Add(1502,typeof(LinkmanResponseAddC2S));
        }

        void S2CmsgInit() {
            
            PBS2CDic.Add(1000,typeof(UserRegisterS2C));
            PBS2CDic.Add(1001,typeof(UserLoginS2C));
            PBS2CDic.Add(1002,typeof(UserWechatLoginS2C));
            PBS2CDic.Add(1003,typeof(UserQuitS2C));
            PBS2CDic.Add(1100,typeof(ServerGetRecommendS2C));
            PBS2CDic.Add(1101,typeof(ServerSelectS2C));
            PBS2CDic.Add(1102,typeof(ServerGetCustomS2C));
            PBS2CDic.Add(1200,typeof(NewsGetLGS2C));
            PBS2CDic.Add(1300,typeof(RolesGetInfoS2C));
            PBS2CDic.Add(1400,typeof(HeroGetListS2C));
            PBS2CDic.Add(1401,typeof(HeroBuyS2C));
            PBS2CDic.Add(1500,typeof(LinkmanGetListS2C));
            PBS2CDic.Add(1501,typeof(LinkmanAddFriendS2C));
            PBS2CDic.Add(1502,typeof(LinkmanResponseAddS2C));
        }

        //客户端发送给服务器 检查发送消息ID是否合法
        public bool CheckC2SPB(int id)
        {
            //检查是否包含key的方法
            return PBC2SDic.ContainsKey(id);
        }

        //服务器发送给客户端 检查消息ID是否合法
        public bool CheckS2CPB(int id)
        {
            return PBS2CDic.ContainsKey(id);
        }
    }
}