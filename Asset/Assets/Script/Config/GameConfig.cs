using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameConfig :SingleletonBase<GameConfig>
{
    //判断是否通过AB的形式加载AssetNundle
    public bool IsABLoad = false; 
}
