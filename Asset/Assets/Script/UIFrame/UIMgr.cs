using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMgr : MonoSingleton<UIMgr>
{

    Dictionary<UIPrefab,GameObject> windowDic = new Dictionary<UIPrefab, GameObject>();
    UIPrefab currentWindow;

    public Transform workstation;  //工作站：打开的UI窗口
    public Transform recyclePool;  //回收站：关闭的UI窗口

    public int recyclePoolMaxCount = 10;
    Stack<UIPrefab> recycleStack = new Stack<UIPrefab>();  //栈结构，存储所有关闭的窗口

    //初始化工作站和回收站的节点
    public void CheckParent()
    {
        if(workstation == null)
        {
            workstation = GameObject.Find("UIRoot").transform.Find("workstation");
            recyclePool = GameObject.Find("UIRoot").transform.Find("recyclePool");
        }
    }

    public void OpenWindow(UIPrefab ui)
    {
        CheckParent();
        string path=UIConfig.Instance.GetLoadPath(ui);
        GameObject go;
        if (windowDic.ContainsKey(ui))
        {
            go =windowDic[ui];
        }
        else
        {
            go=ResMgr.Instance.LoadPrefab(path);
            windowDic.Add(ui,go);
        }
        if (go != null)
        {
            if (go.GetComponent<UIBase>() == null)
            {
                //添加ui组件；
                go.AddComponent(Type.GetType(ui.ToString()));
            }
            go.GetComponent<UIBase>().myUIPrefab = ui;
            //第二个参数fasle 表示不修改它本身预设的transform
            go.transform.SetParent(workstation,false);
            go.name=ui.ToString();
            go.SetActive(true);
            currentWindow = ui;
        }
    }

    public void CloseWindow(UIPrefab ui )
    {
        if(windowDic.ContainsKey(ui))
        {
            windowDic[ui].SetActive(false);
            windowDic[ui].transform.SetParent(recyclePool,false);
            recycleStack.Push(ui);  //入栈
            RemoveRecyclePool();
        }
    }

    public void ReturnLast()
    {
        if(windowDic[currentWindow].activeInHierarchy==true)
        {
            if(windowDic.ContainsKey(currentWindow))
            {
                windowDic[currentWindow].SetActive(false);
                windowDic[currentWindow].transform.SetParent(recyclePool,false);
            }
            //返回上一个关闭的
            if(recycleStack.Count > 0)
            {
                UIPrefab ui = recycleStack.Pop();   //出栈并移除
                OpenWindow(ui);
            }
        }
        RemoveRecyclePool();
    }

    //循环池清理逻辑
    public void RemoveRecyclePool()
    {
        //检查是否达到控制边界，进行统一的回收释放内存
        if(recyclePool.childCount > recyclePoolMaxCount)
        {
            //移除掉剩下五个
            for (int i = 0; i < recyclePool.childCount; i++)
            {
                if(recyclePool.childCount > 5)
                {
                    UIPrefab removeUI=recyclePool.GetChild(i).GetComponent<UIBase>().myUIPrefab;
                    if(windowDic.ContainsKey(removeUI))
                    {
                        windowDic.Remove(removeUI);
                    }
                    Destroy(recyclePool.GetChild(i).gameObject);
                }
            }
        }
    }
    //
    public void CloseAll()
    {
        foreach(var item in windowDic.Keys)
        {
            Destroy(windowDic[item]);
        }
        windowDic.Clear();
        recycleStack.Clear();
    }
}
