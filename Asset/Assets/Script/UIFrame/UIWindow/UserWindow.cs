using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProtoMsg;

public class UserWindow: UIWindowBase{
    
	#region 全局变量
    Transform TipsText_dyn;
    #endregion

    UserControl UserControl;
    private UserControl GetControl()
    {
        if (UserControl == null)
        {
            if (GetComponent<UserControl>() == null)
            {
                UserControl = gameObject.AddComponent<UserControl>();
            }
            else
            {
                UserControl= GetComponent<UserControl>();
            }
        }
        return UserControl;
    }
	
    protected override void OnEnable()
    {
        base.OnEnable();
    }

    protected override void Init()
    {
        base.Init();
        for (int i = 0; i < btns.Length; i++)
        {
            switch (btns[i].name)
            {
                #region 注册按钮事件
                case "LoginBtn":
                    btns[i].onClick.AddListener(LoginBtnOnClick);
                    break;
                case "RegisterBtn":
                    btns[i].onClick.AddListener(RegisterBtnOnClick);
                    break;
                #endregion
                default:
                    break;
            }
        }
		#region 变量初始化
        TipsText_dyn=transform.Find("LogonPanel/TipsText_dyn");

		#endregion
    }

    protected override void AddListener()
    {
        base.AddListener();
    }
	
	#region 内部接口
    public void LoginBtnOnClick()
    {

    }
    public void RegisterBtnOnClick()
    {

    }

	

    #endregion
	
	#region 对外接口
    /// <summary> 注册帐号 </summary>
    public void HandleUserRegisterS2C(UserRegisterS2C p)
    {

    }

    /// <summary> 帐号密码登录结果 </summary>
    public void HandleUserLoginS2C(UserLoginS2C p)
    {

    }

    /// <summary> 微信登录结果 </summary>
    public void HandleUserWechatLoginS2C(UserWechatLoginS2C p)
    {

    }

    /// <summary> 返回退出账号结果 </summary>
    public void HandleUserQuitS2C(UserQuitS2C p)
    {

    }
    #endregion


    protected override void RemoveListener()
    {
        base.RemoveListener();
    }
}
