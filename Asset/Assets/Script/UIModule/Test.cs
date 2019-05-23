using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour 
{


	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.A))
        {
            UIMgr.Instance.OpenWindow(UIPrefab.TestA);
        }


        if (Input.GetKeyDown(KeyCode.B))
        {
            UIMgr.Instance.OpenWindow(UIPrefab.TestB);
        }


        if (Input.GetKeyDown(KeyCode.C))
        {
            UIMgr.Instance.CloseWindow(UIPrefab.TestA);
        }

        if (Input.GetKeyDown(KeyCode.V))
        {
            UIMgr.Instance.CloseWindow(UIPrefab.TestB);
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            UIMgr.Instance.ReturnLast();
        }
    }
}
