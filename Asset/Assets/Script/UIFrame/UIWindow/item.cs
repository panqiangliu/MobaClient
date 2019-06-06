using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProtoMsg;

public class item: UIWindowBase{

    #region 全局变量

    #endregion

    //item item;
    item Item;
    private item GetControl()
    {
        if (Item == null)
        {
            if (GetComponent<item>() == null)
            {
                Item = gameObject.AddComponent<item>();
            }
            else
            {
                Item = GetComponent<item>();
            }
        }
        return Item;
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

				#endregion
                default:
                    break;
            }
        }
		#region 变量初始化

		#endregion
    }

    protected override void AddListener()
    {
        base.AddListener();
    }
	
	#region 内部接口

	

    #endregion
	
	#region 对外接口
	

    #endregion


    protected override void RemoveListener()
    {
        base.RemoveListener();
    }
}
