﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotMachine_hase : MonoBehaviour
{
    private int symbolLeft = 0;
    private int symbolCenter = 0;
    private int symbolRight = 0;
    private Dictionary<Role, ProData> diction; 
    private Condition condition = Condition.NOMAL;
    private Config config = 0;
    private Dic dic;
    private Data data;
    private int pro = 0;
    private Role role;

    private List<GameObject> leftsymbol;
    private List<GameObject> centersymbol;
    private List<GameObject> rightsymbol;

    [SerializeField] private GameObject leftReal;
    [SerializeField] private GameObject centerReal;
    [SerializeField] private GameObject rightReal;


    public void Start()
    {
        config =(Config)UnityEngine.Random.Range(0,2);
        condition = Condition.NOMAL;
        dic = Prodic.LoadDic();
        ChangeMode(dic);
        //Test.TestProdic(dic);

        leftsymbol = SetReal(leftReal);
        centersymbol = SetReal(centerReal);
        rightsymbol = SetReal(rightReal);

    }
    private List<GameObject> SetReal(GameObject obj)
    {
        List<GameObject> list = new List<GameObject>();
        for (int i = 0; i < obj.transform.childCount; i++)
        {
         list.Add(obj.transform.GetChild(i).gameObject);
        }
        return list;
    }
    /// <summary>
    /// ればーおん！
    /// </summary>
    public void LeverOn()
    {
        RandomRole();
    }

    /// <summary>
    /// ランダムな数値を出す
    /// </summary>
    /// <param name="condition">現在のスロットの状態</param>
    private void RandomRole()
    {
        int rand = UnityEngine.Random.Range(1,pro);
        role = DecideRole(rand);
        DecideSymbol(role);
    }

    /// <summary>
    /// 役からそろう柄を決定するメソッド
    /// <param name="rand">生成された乱数</param>
    /// </summary>
    private void DecideSymbol(Role r)
    {
        Debug.Log("小役:" + r + " ID:" + diction[r]);
        Debug.Log(Data.symbolDic[r]);
        symbolLeft = Data.symbolDic[r].l;
        symbolCenter = Data.symbolDic[r].c;
        symbolRight = Data.symbolDic[r].r;
        Debug.Log("左：" + symbolLeft + "　中：" + symbolCenter + "　右：" + symbolRight);
    }

    /// <summary>
    /// 数値に対応する役を抽出するメソッド
    /// </summary>
    /// <param name="random">確率の分母となる引数</param>
    /// <returns>enumの役の格納ナンバー</returns>
    public Role DecideRole(int random)
    {
        int sum = 0;
        foreach(Role r in diction.Keys)
        {
            sum += diction[r].appearpro;
            if(random <= sum)
            {
                return r;
            }
        }
        return 0;
    }

    /// <summary>
    /// ボーナス判定
    /// </summary>
    /// <param name="percent">ボーナス確率(%)</param>
    /// <returns>ボーナスの判定</returns>
    public static bool BonusJudge(int percent)
    {
        int rand = UnityEngine.Random.Range(1, 1000);
        if (rand <= percent) return true;
        else return false;
    }

    /// <summary>
    /// 小役QUESTION(問題)で問題を答えた時に動かすメソッド
    /// </summary>
    /// <param name="currect">正解かどうか</param>
    /// <returns>連続正解した数＊設定、状態に対応した確率</returns>
    public void Answer(bool currect)
    {
        if (currect) data.Cor++;
        else data.Cor = 0;
    }

    private void ChangeMode(Dic dic)
    {
        pro = 0;
        diction = Prodic.GetPro(dic, config, condition);
        foreach(Role r in diction.Keys)
        {
            pro += diction[r].appearpro;
        }
    }

    public void btnPush(string pos)
    {
        Position p = (Position)Enum.Parse(typeof(Position),pos);
        switch (p)
        {
            case Position.LEFT:
                AssistDicision(leftsymbol, (Symbol)Data.symbolDic[role].l);
                break;
            case Position.MIDDLE:
                AssistDicision(centersymbol, (Symbol)Data.symbolDic[role].c);
                break;
            case Position.RIGHT:
                AssistDicision(rightsymbol, (Symbol)Data.symbolDic[role].r);
                break;
        }
    }

    private void AssistDicision(List<GameObject> list,Symbol s)
    {
        foreach(GameObject obj in list)
        {
            Symbol sm = obj.GetComponent<SymbolData>().GetSymbol();
            if (obj.transform.localPosition.y >= -100f&& sm == s &&sm!=0)
            {
                StartCoroutine(Assist(obj));
            }
        }
    }
    private IEnumerator Assist(GameObject obj)
    {
        Debug.Log(obj.name);
        yield return new WaitWhile(() =>obj.transform.localPosition.y >= 0f);
        obj.GetComponent<SymbolScript>().RealStop();
        AllRealStop(obj.GetComponent<SymbolData>().GetPos());
    }
    private void AllRealStop(Position p)
    {
        switch (p)
        {
            case Position.LEFT:
                foreach(GameObject obj in leftsymbol)
                {
                    obj.GetComponent<SymbolScript>().RealStop();
                }
                break;
            case Position.MIDDLE:
                foreach (GameObject obj in leftsymbol)
                {
                    obj.GetComponent<SymbolScript>().RealStop();
                }
                break;
            case Position.RIGHT:
                foreach (GameObject obj in leftsymbol)
                {
                    obj.GetComponent<SymbolScript>().RealStop();
                }
                break;
        }
    }
}