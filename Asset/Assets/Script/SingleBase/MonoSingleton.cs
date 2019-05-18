using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoSingleton<T> : MonoBehaviour where T:MonoBehaviour
{
    private static T instance;

    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                if (GameObject.Find("ComSingleton") == null)
                {
                    GameObject go=new GameObject("ComSingleton");
                    DontDestroyOnLoad(go);
                }
                if(GameObject.Find("ComSingleton") != null)
                {
                    if (GameObject.Find("ComSingleton").GetComponent<T>()!=null)
                    {
                        instance = GameObject.Find("ComSingleton").AddComponent<T>();
                    }
                }
            }
            return instance;
        }
    }
}
