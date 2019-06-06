using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIWindowBase : MonoBehaviour
{
    public bool isInit = false;//是否已初始化
    public UIPrefab myUIPrefab;//当前的UI类型
    public Button[] btns;//遍历到的所有的按钮


    protected virtual void OnEnable()
    {
        Debug.Log("OnEnable:" + this.name);
        //隔离其他UI窗口
        if (this.gameObject.GetComponent<Button>() != null)
        {
            this.gameObject.GetComponent<Button>().onClick.AddListener(() => {
                UIMgr.Instance.CloseWindow(myUIPrefab);
            });
        }

        if (isInit == false)
        {
            //初始化所有按钮 true表示也可以获取到隐藏的物体
            btns = transform.GetComponentsInChildren<Button>(true);
            Init();
            isInit = true;
        }
        AddListener();
    }
    //OnOpen
    protected virtual void Init() { }
    protected virtual void AddListener() { }

    protected virtual void RemoveListener() { }


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


}
