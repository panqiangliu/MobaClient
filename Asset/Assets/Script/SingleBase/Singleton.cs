using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : IDisposable where T : new()
{

    private static T instance;

    public static T Instance
    {
        //只读
        get
        {
            if (instance == null)
            {
                instance = new T();
            }
            return instance;
        }

    }

    public void Dispose()
    {
       //手动释放内存 不用等待系统GC回收
    }


}
