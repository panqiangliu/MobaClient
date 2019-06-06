using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoSingleton<T> : MonoBehaviour where T: MonoBehaviour {

    private static T instance;
    
    //属性访问器
    public static T Instance
    {
        get
        {
            
            
            if (instance == null)
            {
                
                //单例设计的思想:
                //1.把所有的继承mono的单例都挂载到一个游戏物体上ComSingleton
                //2.通过GetCom..来获取相应的组件
                //3.ComSingleton这个物体只提供所有单例的访问能力 不做任何的游戏模块的逻辑
                if (GameObject.Find("ComSingleton") == null)
                {
                    
                    GameObject go = new GameObject("ComSingleton");
                    DontDestroyOnLoad(go);
                }
                //物体创建成功
                if (GameObject.Find("ComSingleton") != null)
                {
                    //获取组件<T>
                    if (GameObject.Find("ComSingleton").GetComponent<T>() != null)
                    {
                        instance = GameObject.Find("ComSingleton").GetComponent<T>();
                    }
                    else
                    {
                        instance = GameObject.Find("ComSingleton").AddComponent<T>();
                    }
                }
            }
           
            return instance;
        }
    }
}
