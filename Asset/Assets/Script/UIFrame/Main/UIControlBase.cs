using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIControlBase : MonoBehaviour {

    protected virtual void OnEnable()
    {
        AddListener();
    }

    protected virtual void AddListener()
    {

    }

    protected virtual void RemoveListener()
    {

    }
    
    protected void OnDisable()
    {
        Debug.Log("OnDisable:" + this.name);
        RemoveListener();
    }

    protected void OnDestroy()
    {
        Debug.Log("OnDestroy:" + this.name);
        RemoveListener();
    }

    //发送消息给服务器的接口

    //接收来自服务器消息的接口

    //全局监听的事件:而监听的模块只做自身相应的逻辑 
    //比如网络模块 就做网络模块自身的逻辑
    //而帐号模块就做帐号模块的逻辑就行了



}
