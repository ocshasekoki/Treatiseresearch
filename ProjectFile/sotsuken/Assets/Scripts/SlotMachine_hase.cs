using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotMachine_hase : MonoBehaviour
{
    private int symbolLeft = 0;
    private int symbolCenter = 0;
    private int symbolRight = 0;

    /// <summary>
    /// 役に対応する柄のディクショナリ
    /// <para>Role :役のこと。列挙型</para>
    /// <para>l :leftの略。左の柄のIDを示す。</para>
    /// <para>c :centerの略。中央の柄のIDを示す。</para>
    /// <para>r :rightの略。右の柄のIDを示す。</para>
    /// </summary>
    public static Dictionary<Role, (int l, int c, int r)> dic = new Dictionary<Role, (int l, int c, int r)>()
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

    private void Start()
    {
        //RandomRole();
        TestCase();
    }

    /// <summary>
    /// ランダムに役を決定するメソッド
    /// </summary>
    private void DecideSymbol(int rand)
    {
        Debug.Log("小役:" + ((Role)rand).ToString() + " ID:" + rand);
        Debug.Log(dic[(Role)rand]);

        symbolLeft = dic[(Role)rand].l;
        symbolCenter = dic[(Role)rand].c;
        symbolRight = dic[(Role)rand].r;
        
        Debug.Log("左：" + symbolLeft + "　中：" + symbolCenter + "　右：" + symbolRight);
    }
    private void RandomRole()
    {
        int rand = UnityEngine.Random.Range(0, 8192);
        Debug.Log(DecideRole(rand));
    }

    private int DecideRole(int random)
    {
        List<int> roleList = new List<int>();
        roleList.Add(0);
        foreach (int index in Enum.GetValues(typeof(Role)))
        {
            roleList.Add(index);
            if(random <= roleList[roleList.Count - 1])
            {
                return roleList[roleList.Count - 1];
            }
        }
        return 8192;
    }
    private void TestCase()
    {
        string str = "";
        string rolename = "";
        int count = 0;
        for (int i = 0; i <= 8192; i++)
        {
            rolename = Enum.GetName(typeof(Role), DecideRole(i));
            count++;
            if (str != rolename)
            {
                str = rolename;
                Debug.Log("役：" + str +" 確率："+count + "/8192");
                count = 0;
            }
        }
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
    BIGBONUS = 2,
    REGBONUS = 8,
    STRONGCHERRY = 49,
    CHERRY = 104,
    WEAKCHERRY = 206,
    WATERMELON = 274,
    QUESTION = 550,
    BELL = 960,
    REPLAY = 2598,
    NONE = 8192,
}
