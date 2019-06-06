using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProtoMsg;

public class TestWindow: UIWindowBase{
    
	#region 全局变量
    Transform TipsText_dyn;

	#endregion

    /*
	
	TestControl TestControl;
    private TestControl GetControl()
    {
        if (TestControl == null)
        {
            if (GetComponent<TestControl>() == null)
            {
                TestControl = gameObject.AddComponent<TestControl>();
            }
            else
            {
                TestControl= GetComponent<TestControl>();
            }
        }
        return TestControl;
    }
	*/
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
	

    #endregion


    protected override void RemoveListener()
    {
        base.RemoveListener();
    }
}
