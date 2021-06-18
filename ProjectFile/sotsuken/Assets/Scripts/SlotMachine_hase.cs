using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotMachine_hase : MonoBehaviour
{
    private int symbolLeft = 0;
    private int symbolCenter = 0;
    private int symbolRight = 0;

    private Condition condition = Condition.NOMAL;
    private Config config = 0;
    private Data data;
    /// <summary>
    /// ればーおん！
    /// </summary>
    public void LeverOn()
    {
        RandomRole(condition);
    }
    /// <summary>
    /// ランダムな数値を出す
    /// </summary>
    /// <param name="condition">現在のスロットの状態</param>
    private void RandomRole(Condition con)
    {
        int rand = UnityEngine.Random.Range(0,8192);
        Debug.Log(rand);
        DecideSymbol(DecideRole(rand));
    }

    /// <summary>
    /// 役からそろう柄を決定するメソッド
    /// <param name="rand">生成された乱数</param>
    /// </summary>
    private void DecideSymbol(int rand)
    {
        Debug.Log("小役:" + ((Role)rand).ToString() + " ID:" + rand);
        Debug.Log(Data.symbolDic[(Role)rand]);

        symbolLeft = Data.symbolDic[(Role)rand].l;
        symbolCenter = Data.symbolDic[(Role)rand].c;
        symbolRight = Data.symbolDic[(Role)rand].r;
        
        Debug.Log("左：" + symbolLeft + "　中：" + symbolCenter + "　右：" + symbolRight);
    }

    /// <summary>
    /// 数値に対応する役を抽出するメソッド
    /// </summary>
    /// <param name="random">確率の分母となる引数</param>
    /// <returns>enumの役の格納ナンバー</returns>
    public static int DecideRole(int random)
    {
        foreach (int index in Enum.GetValues(typeof(Role)))
        {
            if(random <= index)
            {
                return index;
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
        int rand = UnityEngine.Random.Range(1, 100);
        Debug.Log("乱数：" + rand);
        if (rand <= percent) return true;
        else return false;
    }

    /// <summary>
    /// 小役QUESTION(問題)で問題を答えた時に動かすメソッド
    /// </summary>
    /// <param name="currect">正解かどうか</param>
    /// <returns></returns>
    public int Answer(bool currect)
    {
        if (currect) data.Cor++;
        else data.Cor = 0;
        return data.Cor;
        //return data.correctcount * Dic.dic[Role.QUESTION][(int)config,(int)condition];
    }
}

