using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//此脚本和UIWindowBase相同
public class UIBase : MonoBehaviour
{
    public bool isInit=false;    //判断是否已经初始化
    public UIPrefab myUIPrefab;   //当前的UI类型
    public Button[] btns;          //遍历到所有的按钮

    protected virtual void OnEnable()
    {
        Debug.Log("OnEnable:"+this.name);
        //隔离其他的UI窗口
        if(this.gameObject.GetComponent<Button>()!=null)
        {
            this.gameObject.GetComponent<Button>().onClick.AddListener(()=>
            {
                UIMgr.Instance.CloseWindow(myUIPrefab);
            });
        }
        if (isInit==false)
        {
            btns =transform.GetComponentsInChildren<Button>(true);
            Init();
            isInit=true;
        }
        AddListener();
    }
    protected virtual void Init(){}
    protected virtual void AddListener(){}
    protected virtual void RemoveListener(){}

    protected void OnDisable()
    {
        Debug.Log("OnDisable:"+this.name);
        RemoveListener();
    }

    protected void OnDestroy()
    {
        Debug.Log("OnDestory:"+this.name);
        RemoveListener();
    }
    
}
