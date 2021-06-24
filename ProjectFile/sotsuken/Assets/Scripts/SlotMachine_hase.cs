using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class SlotMachine_hase : MonoBehaviour
{
    private int symbolLeft = 0;
    private int symbolCenter = 0;
    private int symbolRight = 0;
    private Dictionary<Role, int> diction; 
    private Condition condition = Condition.NOMAL;
    private Config config = 0;
    private Dic dic;
    private Data data;

    private int pro = 0;
    public void Start()
    {
        config =(Config)UnityEngine.Random.Range(0,2);
        condition = Condition.NOMAL;
        dic = Prodic.LoadDic();
        ChangeMode(dic);
        Test.TestProdic(dic);
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
        int rand = UnityEngine.Random.Range(0,pro);
        DecideSymbol(DecideRole(rand));
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
        foreach(Role r in diction.Keys)
        {
            if(random <= diction[r])
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
    public int Answer(bool currect)
    {
        if (currect) data.Cor++;
        else data.Cor = 0;
        return data.Cor*Prodic.GetOc(dic,config,Role.QUESTION,condition);
    }

    private void ChangeMode(Dic dic)
    {
        pro = 0;
        diction = Prodic.GetPro(dic, config, condition);
        foreach(Role r in diction.Keys)
        {
            pro += diction[r];
        }
    }
}