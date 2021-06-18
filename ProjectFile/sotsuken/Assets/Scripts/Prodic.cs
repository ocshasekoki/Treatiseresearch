using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
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

    private int correctcount;
    public int Cor
    {
        get { return correctcount; }
        set { correctcount=value; }
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
    public List<ProData> ocdic = new List<ProData>();
    public List<ProData> prodic = new List<ProData>();
}
[Serializable]
public class ProData
{
    public Config conf;
    public Condition cond;
    public Role role;
    public int param;

    public void Dump()
    {
        Debug.Log("設定：" + conf +" 小役：" + role+ " 状態：" + cond+" 数値：" + param);
    }
}

public class Prodic :MonoBehaviour
{
    private Dic dic = new Dic();
    public int GetOc(Config c,Role r,Condition cd)
    {
        return dic.ocdic.Find(x => x.conf == c&&x.role ==r&&x.cond == cd).param;
    }
    public int GetPro(Config c, Role r, Condition cd)
    {
        return dic.prodic.Find(x => x.conf == c && x.role == r && x.cond == cd).param;
    }

    public void OutputDic()
    {
        string json = JsonUtility.ToJson(dic);
        string path = Application.streamingAssetsPath + "/prodic.json";
        File.WriteAllText(path,json);
        Debug.Log("出力");
    }
    public void InputDic()
    {
        string path = Application.streamingAssetsPath + "/prodic.json";
        string str = File.ReadAllText(path);
        Debug.Log(str);
        dic = JsonUtility.FromJson<Dic>(str);
        foreach(ProData d in dic.ocdic)
        {
            d.Dump();
        }
    }
    public void AddOc(ProData data)
    {
        dic.ocdic.Add(data);
    }
    public void AddPro(ProData data)
    {
        dic.prodic.Add(data);
    }
}


/// <summary>
/// 柄の列挙型
/// <para>NONE：役無し</para>
/// <para>CHERRY：チェリー</para>
/// <para>WATERMELON：スイカ</para>
/// <para>BELL：ベル</para>
/// <para>REPLAY：リプレイ</para>
/// <para>QUESTION：問題</para>
/// <para>BAR：バー</para>
/// <para>SEVEN：７</para>
/// </summary>
public enum Symbol
{
    NONE = 0,
    CHERRY = 1,
    WATERMELON = 2,
    BELL = 3,
    REPLAY = 4,
    QUESTION = 5,
    BAR = 6,
    SEVEN = 7,
}


/// <summary>
/// 役の列挙型
/// <para>FREEZE：フリーズ 1/8192</para>
/// <para>BIGBONUS：ビッグボーナス 1/4096</para>
/// <para>REGBONUS：レギュラーボーナス 1/2048</para>
/// <para>STRONGCHERRY：強チェリー 1/250</para>
/// <para>CHERRY：中チェリー 1/150</para>
/// <para>WEAKCHERRY：弱チェリー 1/80</para>
/// <para>WATERMELON：スイカ 1/120</para>
/// <para>QUESTION：問題出題 1/30</para>
/// <para>REPLAY：リプレイ 1/5</para>
/// <para>NONE：役無し</para>
/// </summary>

public enum Role
{
    FREEZE = 0,
    BIGBONUS=1,
    REGBONUS=2,
    STRONGCHERRY=3,
    CHERRY=4,
    WEAKCHERRY=5,
    WATERMELON=6,
    QUESTION=7,
    BELL=8,
    REPLAY=9,
    NONE=10,
}


public enum Condition
{
    NOMAL=0,
    HIGH=1,
    SUPERHIGH=2,
    CZ = 3,
    BONUS =4,
    AT= 5,
}

public enum Config
{
    LOW = 0,
    MIDDLE = 1,
    HIGH = 2
}
