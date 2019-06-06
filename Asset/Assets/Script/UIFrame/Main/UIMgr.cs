using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class UIMgr : MonoSingleton<UIMgr>
{

    //缓存克隆出来的窗口
    Dictionary<UIPrefab, GameObject> windowDic = new Dictionary<UIPrefab, GameObject>();
    UIPrefab currentWindow;

    public Transform workstation;//工作站:打开的UI窗口
    public Transform recyclePool;//回收站:关闭的UI窗口

    public int recyclePoolMaxCount = 10;//回收站最高数量 影响清理调用的次数
    Stack<UIPrefab> recycleStack = new Stack<UIPrefab>();//栈结构 存储所有关闭的窗口

    /// <summary>
    /// 初始化工作站和回收站的节点
    /// </summary>
    private void CheckParent()
    {

        if (workstation == null)
        {
            workstation = GameObject.Find("UIRoot").transform.Find("workstation");

        }
        if (recyclePool == null)
        {
            //if (GameObject.Find("UIRoot").transform.Find("recyclePool")==null)
            //{
            //    recyclePool = new GameObject("recyclePool").transform;
            //    recyclePool.SetParent(GameObject.Find("UIRoot").transform);
            //}
            recyclePool = GameObject.Find("UIRoot").transform.Find("recyclePool");
        }
    }

    //打开窗口
    public void OpenWindow(UIPrefab ui)
    {
        CheckParent();
        string path = UIConfig.Instance.GetLoadPath(ui);
        GameObject go;
        if (windowDic.ContainsKey(ui))
        {
            go = windowDic[ui];
        }
        else
        {
            go = ResMgr.Instance.LoadPrefab(path);
            windowDic.Add(ui, go);
        }

        if (go != null)
        {
            if (go.GetComponent<UIWindowBase>() == null)
            {
                //添加windowbase
                if (Type.GetType(ui.ToString()) != null)
                {
                    go.AddComponent(Type.GetType(ui.ToString()));
                }
            }
            //添加ui控制器 Ctrl+K  F
            string con = ui.ToString().Replace("Window", "Control");
            if (go.GetComponent(Type.GetType(con)) == null)
            {

                if (Type.GetType(con) != null)
                {

                    go.AddComponent(Type.GetType(con));
                }

            }

            go.GetComponent<UIWindowBase>().myUIPrefab = ui;
            //第二个参数false 表示不修改它本身预设的transform
            go.transform.SetParent(workstation, false);
            go.name = ui.ToString();
            go.SetActive(true);
            currentWindow = ui;
        }

    }

    //关闭窗口
    public void CloseWindow(UIPrefab ui)
    {
        CheckParent();
        if (windowDic.ContainsKey(ui))
        {
            windowDic[ui].SetActive(false);
            windowDic[ui].transform.SetParent(recyclePool, false);
            recycleStack.Push(ui);//入栈
            RemoveRecyclePool();

        }
    }

    //返回上一个窗口
    public void ReturnLast()
    {
        if (windowDic[currentWindow].activeInHierarchy == true)
        {
            if (windowDic.ContainsKey(currentWindow))
            {
                windowDic[currentWindow].SetActive(false);
                windowDic[currentWindow].transform.SetParent(recyclePool, false);
            }
        }

        //返回上一个关闭的
        if (recycleStack.Count > 0)
        {
            UIPrefab ui = recycleStack.Pop();//出栈 并且移除 
            OpenWindow(ui);
        }
        RemoveRecyclePool();
    }

    //池子清理逻辑
    private void RemoveRecyclePool()
    {
        //检查是否到达控制边界 进行统一回收释放内存
        if (recyclePool.childCount > recyclePoolMaxCount)
        {
            //移除掉剩下五个
            for (int i = 0; i < recyclePool.childCount; i++)
            {
                if (recyclePool.childCount > 5)
                {
                    UIPrefab removeUI = recyclePool.GetChild(i).GetComponent<UIWindowBase>().myUIPrefab;
                    if (windowDic.ContainsKey(removeUI))
                    {
                        windowDic.Remove(removeUI);
                    }
                    Destroy(recyclePool.GetChild(i).gameObject);
                }
            }
        }
    }

    //关闭所有的窗口
    public void CloseAll()
    {
        foreach (var item in windowDic.Keys)
        {
            Destroy(windowDic[item]);
        }
        windowDic.Clear();
        recycleStack.Clear();
    }

    //获取控制器 看必要性扩展

}
