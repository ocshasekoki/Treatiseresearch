using System;
using System.Collections.Generic;
using UnityEngine;


public class Test :MonoBehaviour
{
    /// <summary>
    /// 全ての設定+状態のそれぞれの確率を出力するテスト
    /// </summary>
    /// <param name="dic">Jsonファイルから読み取ったデータ</param>
    public static void TestProdic(Dic dic)
    {
        int appearsum = 0;
        int bonussum = 0;
        int bigbonussum = 0;
        int freezesum = 0;
        int chancezonesum = 0;

        foreach (Config conf in Enum.GetValues(typeof(Config))) 
        {
            foreach (Condition cond in Enum.GetValues(typeof(Condition)))
            {
                Dictionary<Role, ProData> d = Prodic.GetPro(dic,conf,cond);
                foreach(Role r in d.Keys)
                {
                    appearsum += d[r].appearpro;
                    bonussum += d[r].bonuspro;
                    bigbonussum += d[r].bigbonuspro;
                    freezesum += d[r].freezepro;
                    chancezonesum += d[r].chancezonepro;
                }
                Debug.Log("設定：" + conf.ToString() + " 状態：" + cond.ToString());
                Debug.Log(" 出現総合値（分母）：" + appearsum + " 総ボーナス出現確率（合算）：" + (bonussum+bigbonussum+freezesum+chancezonesum) + "/40000");
                Debug.Log("ボーナス確率：" + bonussum+"/100000");
                Debug.Log("ビッグボーナス確率：" + bigbonussum + "/100000");
                Debug.Log("フリーズ確率：" + freezesum + "/100000");
                Debug.Log("チャンスゾーン確率：" + chancezonesum + "/100000");
                foreach (Role r in d.Keys)
                {
                    Debug.Log("小役：" + r.ToString() + " 出現確率：" + d[r].appearpro +"/" + appearsum);
                    Debug.Log("小役：" + r.ToString() + " ボーナス確率：" + d[r].bonuspro + "/100000");
                    Debug.Log("小役：" + r.ToString() + " ビッグボーナス確率：" + d[r].bigbonuspro + "/100000");
                    Debug.Log("小役：" + r.ToString() + " フリーズ確率：" + d[r].freezepro + "/100000");
                    Debug.Log("小役：" + r.ToString() + " チャンスゾーン確率：" + d[r].chancezonepro + "/100000");
                }
                appearsum = 0;
                bonussum = 0;
                bigbonussum = 0;
                freezesum = 0;
                chancezonesum = 0;
            }

        } 
    }
}


