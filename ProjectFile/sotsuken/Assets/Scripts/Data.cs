using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data
{
    public static Dictionary<Role, (int l, int c, int r)> symbolDic = new Dictionary<Role, (int l, int c, int r)>()
    {
        {Role.NONE,(0,0,0) },
        {Role.STRONGCHERRY,(1,1,1)},
        {Role.CHERRY,(1,1,0)  },
        {Role.WEAKCHERRY,(1, 0,0) },
        {Role.WATERMELON,(2, 2, 2) },
        {Role.BELL,(3, 3, 3) },
        {Role.REPLAY,(4, 4, 4) },
        {Role.QUESTION,(5, 5, 5) },
        {Role.REGBONUS,(7, 7, 6) },
        {Role.BIGBONUS,(7, 7, 7) },
        {Role.FREEZE,(7, 7, 7) },
    };

    private int correctcount = 0;
    public int Cor
    {
        get { return correctcount; }
        set { correctcount = value; }
    }
    private int coin;
    public int Coin
    {
        get { return coin; }
        set { coin = value; }
    }
}

[Serializable]
public class Dic
{
    public List<ProData> prodic = new List<ProData>();
}
[Serializable]
public class ProData
{
    public Config conf;         //設定
    public Condition cond;      //状態
    public Role role;           //小役
    public int bonuspro;        //ボーナス確率
    public int appearpro;       //出現確率

    public void Dump()
    {
        Debug.Log("設定：" + conf + " 小役：" + role + " 状態：" + cond + " 出現確率：" + appearpro + " ボーナス確立：" + bonuspro);
    }
}
