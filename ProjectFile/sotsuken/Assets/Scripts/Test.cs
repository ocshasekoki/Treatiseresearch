using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Test
{
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

    public static void BonusJudgle(Role role)
    {
        Debug.Log(Dic.percentdic[Role.BELL]);
        Debug.Log(Dic.percentdic[Role.CHERRY]);
        Debug.Log(Dic.percentdic[Role.STRONGCHERRY]);
        Debug.Log(Dic.percentdic[Role.WEAKCHERRY]);
        Debug.Log(Dic.percentdic[Role.WATERMELON]);
        Debug.Log(Dic.percentdic[Role.QUESTION]);
        Debug.Log("ボーナス判定" + SlotMachine_hase.BonusJudge(Dic.percentdic[role]));
    }

}

