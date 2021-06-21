﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Prodic :MonoBehaviour
{
    public static int GetOc(Dic dic,Config c,Role r,Condition cd)
    {
        return dic.ocdic.Find(x => x.conf == c&&x.role ==r&&x.cond == cd).param;
    }
    public static int GetPro(Dic dic,Config c, Role r, Condition cd)
    {
        return dic.prodic.Find(x => x.conf == c && x.role == r && x.cond == cd).param;
    }
    /// <summary>
    /// 確率書き込み（ツール用）
    /// </summary>
    /// <param name="dic">書き込むデータ</param>
    /// <param name="name">ファイル名</param>
    public static void OutputDic(string name ,Dic dic)
    {
        string json = JsonUtility.ToJson(dic);
        string path = Application.streamingAssetsPath + "/"+name+".json";
        File.WriteAllText(path,json);
        Debug.Log("出力");
    }
    public static Dic InputDic()
    {
        string path = Application.streamingAssetsPath + "/prodic.json";
        string str = File.ReadAllText(path);
        Debug.Log(str);
        return JsonUtility.FromJson<Dic>(str);
        
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
