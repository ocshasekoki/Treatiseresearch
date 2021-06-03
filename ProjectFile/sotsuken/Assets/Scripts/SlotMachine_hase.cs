using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotMachine_hase : MonoBehaviour
{
    int symbolLeft;
    int symbolCenter;
    int symbolRight;


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

    void Start()
    {
        PushSlot();
    }
    void PushSlot()
    {
        //役生成
        int rand = UnityEngine.Random.Range(0, 8);
        
        Debug.Log("小役:" + ((Role)rand).ToString() + " ID:" + rand);
        Debug.Log(dic[(Role)rand]);

        symbolLeft = dic[(Role)rand].l;
        symbolCenter = dic[(Role)rand].c;
        symbolRight = dic[(Role)rand].r;
        
        Debug.Log("左：" + symbolLeft + "　中：" + symbolCenter + "　右：" + symbolRight);
    }
}

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
    FREEZE = 8
}
public enum Role
{
    FREEZE = 0,
    BIGBONUS = 1,
    REGBONUS = 2,
    BELL = 3,
    STRONGCHERRY = 4,
    CHERRY = 5,
    WEAKCHERRY = 6,
    WATERMELON = 7,
    QUESTION = 8,
    REPLAY = 9,
    NONE = 10,
}
