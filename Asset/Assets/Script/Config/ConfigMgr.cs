using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfigMgr : MonoSingleleton<ConfigMgr>
{
    public bool CheckIsABLoad()
    {
        return GameConfig.Instance.IsABLoad;
    }
}
