using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleletonBase<T> : IDisposable where T : new()
{
    public static T instance;
    
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
    public void Dispose()
    {

    }

}

