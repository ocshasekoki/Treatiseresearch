using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Slot_Noguchi : MonoBehaviour
{
    int correctcount = 0;   //正解数
    Dictionary<Role, int> percentdic = new Dictionary<Role, int>()
    {
        { Role.WEAKCHERRY,1 },
        {Role.CHERRY,20 },
        {Role.QUESTION, 5 },
        {Role.BELL, 1 },
        {Role.WATERMELON, 10 },
        {Role.STRONGCHERRY, 50 },
    };

    private void RoleDecide(Role role)
    {
         Debug.Log("ボーナス判定" + BonusJudge(percentdic[role]));
    }

    /// <summary>
    /// 回答に対応する確率をとってくる関数
    /// </summary>
    /// <param name="currect">正解かどうか</param>
    /// <returns>確率(%)</returns>
    int Answer(bool currect)
    {
        if (currect) correctcount++;
        else correctcount = 0;
        return correctcount * percentdic[Role.QUESTION];
    }
    /// <summary>
    /// ボーナス判定
    /// </summary>
    /// <param name="percent">確率(%)</param>
    /// <returns>ボーナスの判定</returns>
    public bool BonusJudge(int percent)
    {
        int rand = UnityEngine.Random.Range(1, 100);
        Debug.Log("乱数：" + rand);
        if (rand <= percent) return true;
        else return false;
    }

    public void Test()
    {
        Debug.Log(Answer(true));
        Debug.Log(Answer(true));
        Debug.Log(Answer(true));
        Debug.Log(Answer(true));
        Debug.Log(Answer(true));
        Debug.Log(Answer(false));
        Debug.Log(Answer(true));
        Debug.Log(Answer(true));
    }
    public void Test2()
    {
        Debug.Log("強チェリー");
        RoleDecide(Role.STRONGCHERRY);
        Debug.Log("中チェリー");
        RoleDecide(Role.CHERRY);
        Debug.Log("弱チェリー");
        RoleDecide(Role.WEAKCHERRY);
        Debug.Log("スイカ");
        RoleDecide(Role.WATERMELON);
        Debug.Log("ベル");
        RoleDecide(Role.BELL);
    }
}

//ifの分岐を減らす
//listを使用してコードの分量を減らす
//とにかく減らして見やすくすること

