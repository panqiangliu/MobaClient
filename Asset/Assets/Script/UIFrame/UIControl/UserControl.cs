using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProtoMsg;

public class UserControl: UIControlBase
{
    UserWindow UserWindow;
    private UserWindow GetWindow()
    {
        if (UserWindow == null)
        {
            if (GetComponent<UserWindow>() == null)
            {
                UserWindow = gameObject.AddComponent<UserWindow>();
            }
            else
            {
                UserWindow= GetComponent<UserWindow>();
            }
        }
        return UserWindow;
    }

    protected override void AddListener()
    {
        base.AddListener();
        #region 添加监听网络事件
        NetEvent.Instance.AddEventListener(1000,HandleUserRegisterS2C);
        NetEvent.Instance.AddEventListener(1001,HandleUserLoginS2C);
        NetEvent.Instance.AddEventListener(1002,HandleUserWechatLoginS2C);
        NetEvent.Instance.AddEventListener(1003,HandleUserQuitS2C);

		#endregion
    }

    protected override void OnEnable()
    {
        base.OnEnable();
    }

    protected override void RemoveListener()
    {
        base.RemoveListener();
        #region 移除监听网络事件
        NetEvent.Instance.RemoveEventListener(1000,HandleUserRegisterS2C);
        NetEvent.Instance.RemoveEventListener(1001,HandleUserLoginS2C);
        NetEvent.Instance.RemoveEventListener(1002,HandleUserWechatLoginS2C);
        NetEvent.Instance.RemoveEventListener(1003,HandleUserQuitS2C);

		#endregion
    }

	#region 处理网络协议
    /// <summary> 注册帐号 </summary>
    public void HandleUserRegisterS2C(BufferEntity p)
    {
        UserRegisterS2C s2cMsg = UserRegisterS2C.Parser.ParseFrom(p.body);
        GetWindow().HandleUserRegisterS2C(s2cMsg);
    }

    /// <summary> 帐号密码登录结果 </summary>
    public void HandleUserLoginS2C(BufferEntity p)
    {
        UserLoginS2C s2cMsg = UserLoginS2C.Parser.ParseFrom(p.body);
        GetWindow().HandleUserLoginS2C(s2cMsg);
    }

    /// <summary> 微信登录结果 </summary>
    public void HandleUserWechatLoginS2C(BufferEntity p)
    {
        UserWechatLoginS2C s2cMsg = UserWechatLoginS2C.Parser.ParseFrom(p.body);
        GetWindow().HandleUserWechatLoginS2C(s2cMsg);
    }

    /// <summary> 返回退出账号结果 </summary>
    public void HandleUserQuitS2C(BufferEntity p)
    {
        UserQuitS2C s2cMsg = UserQuitS2C.Parser.ParseFrom(p.body);
        GetWindow().HandleUserQuitS2C(s2cMsg);
    }



	#endregion


	#region 发送网络协议
    /// <summary> 注册帐号 </summary>
    public void SendUserRegisterC2S(UserRegisterC2S UserRegisterC2S)
    {

    }

    /// <summary> 帐号密码登录 </summary>
    public void SendUserLoginC2S(UserLoginC2S UserLoginC2S)
    {

    }

    /// <summary> 微信登录 </summary>
    public void SendUserWechatLoginC2S(UserWechatLoginC2S UserWechatLoginC2S)
    {

    }

    /// <summary> 请求退出账号(换号的正常流程) </summary>
    public void SendUserQuitC2S(UserQuitC2S UserQuitC2S)
    {

    }



	#endregion



}
