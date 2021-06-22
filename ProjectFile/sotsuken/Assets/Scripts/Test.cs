using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Test :MonoBehaviour
{
    void Start()
    {
        TestCase();
        Dic dic = Prodic.LoadDic();
    }

    /// <summary>
    /// テストメソッド
    /// </summary>
    public static void TestCase()
    {
        string str = "";
        string rolename = "";
        int count = 0;
        for (int i = 0; i <= 8192; i++)
        {
            rolename = Enum.GetName(typeof(Role), SlotMachine_hase.DecideRole(i));
            count++;
            if (str != rolename)
            {
                if (str != "")
                {
                    Debug.Log("役：" + str + " 確率：" + count + "/8192");
                }
                str = rolename;
                count = 0;
            }
        }
    }


}

