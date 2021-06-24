using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Test :MonoBehaviour
{
    /// <summary>
    /// テストメソッド
    /// </summary>
    public static void TestProdic(Dic dic)
    {
        int appearsum = 0;
        int bonussum = 0;
        foreach (ProData p in dic.prodic)
        {
            p.Dump();
            appearsum += p.appearpro;
            bonussum += p.bonuspro;
            foreach (Config conf in Enum.GetValues(typeof(Config))) 
            {
                foreach (Condition cond in Enum.GetValues(typeof(Condition)))
                {
                    Dictionary<Role, int> d = Prodic.GetPro(dic,conf,cond);
                    foreach(Role r in d.Keys)
                    {
                        appearsum += d[r];
                    }
                    Debug.Log("設定：" + conf.ToString() + " 状態：" + cond.ToString() + " 総合値：" + appearsum);
                    foreach (Role r in d.Keys)
                    {
                        Debug.Log("小役：" + r.ToString() + " 確率：" + d[r] +"/" + appearsum);
                    }
                    appearsum = 0;
                    bonussum = 0;
                }

            } 
        }
    }


}

