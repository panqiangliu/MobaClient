using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameConfig : Singleton<GameConfig>
{

    //是否通过AB的形式加载 Assetbundle
    public bool IsABLoad = false;

    //网关服务器(暂时假设与游戏服务器是一起的)
    public string ServerAddress = "192.168.1.12:3333";
    public string ServerApplicationName = "MobaServer";
}
