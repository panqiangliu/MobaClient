using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//事件基类
//1.提供被继承，实现不同事件分发器、不同参数的能力
//2.提供订阅接口
//3.提供移除订阅的接口
//4.提供推送的接口：将数据传递给订阅者
public class EventBase<T,P,X> : IDisposable
    where T:new ()
    where P:class
{

    #region 单例：保证唯一性
    private static T instance;

    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new T();
            }
            return instance;
        }
    }
    #endregion

    public void Dispose()
    {
        
    }

    //委托原型：目的是将方法当做参数进行传递
    public delegate void OnActionHandler(P p);

    //需要维护的数据结构：key是消息ID Value是委托绑定的成员方法 
    //用List数据结构来解决广播一个消息，让所有订阅者同时调度绑定的方法
    public Dictionary<X, List<OnActionHandler>> dic = new Dictionary<X, List<OnActionHandler>>();

    /// <summary>
    /// 添加订阅：可以在消息推送之后，取得推送的数据，并且执行相应的动作（逻辑）
    /// </summary>
    /// <param name="key">消息ID</param>
    /// <param name="handler">方法</param>
    public void AddEventListener(X key, OnActionHandler handler)
    {
        //判断键是否已经存在 如果存在则添加到存储方法的容器中
        if (dic.ContainsKey(key))
        {
            dic[key].Add(handler);
        }
        //如果不存在，则需要声明一个容器，将参数handler添加到容器中
        //将容器指定给dic结构中的值
        else
        {
            List<OnActionHandler> lstHandler = new List<OnActionHandler>();
            lstHandler.Add(handler);
            dic[key] = lstHandler;
        }
        //消息 ID1001
        //模块A AddEventListener（1001，TestA）
        //模块B AddEventListener（1001，TestB）
    }


    /// <summary>
    /// 取消订阅：则不再关注某条命令
    /// </summary>
    /// <param name="key">消息ID</param>
    /// <param name="handler">方法</param>
    public void RemoveEventListener(X key, OnActionHandler handler)
    {
        //如果包含该消息ID
        if (dic.ContainsKey(key))
        {
            //先取得ID绑定的容器 从容器中移除handler
            List<OnActionHandler> lstHandler = dic[key];
            lstHandler.Remove(handler);
            //移除之后进行判断，如果容器中没有订阅的成员了
            //则对数据结构dic进行维护，移除key对应的容器
            if (lstHandler.Count == 0)
            {
                dic.Remove(key);
            }
        }
    }

    /// <summary>
    /// 推送消息 带参数
    /// </summary>
    /// <param name="key">消息ID</param>
    /// <param name="p">携带的参数</param>
    public void Dispatch(X key, P p)
    {

        //如果字典包含这个键
        if (dic.ContainsKey(key))
        {
            //从数据结构dic中取到存储所有绑定的方法
            List<OnActionHandler> lstHandler = dic[key];

            if (lstHandler != null && lstHandler.Count > 0)
            {
                for (int i = 0; i < lstHandler.Count; i++)
                {
                    if (lstHandler[i] != null)
                    {
                        //通过遍历调度每一个方法，传递参数P
                        lstHandler[i](p);
                    }
                }
            }
        }
    }

    //推送消息 不带参数
    public void Dispatch(X key)
    {
        //将参数设置为空就表示不进行传递参数
        Dispatch(key, null);
    }

}
