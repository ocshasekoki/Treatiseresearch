using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotMachine_hase : MonoBehaviour
{
    //エディタ上で定義できる
    [SerializeField]
    Button btn;         //ボタン
    [SerializeField]
    Text text;          //テキスト

    int symbolLeft;
    int symbolCenter;
    int symbolRight;
    Action<int> action;     //戻り値無し
    Func<int, int> func;    //戻り値あり

    Dictionary<int, Action<int>> aclist;
    delegate void Test();
    public void Main()
    {
        aclist.Add(1,hoge);
        aclist.Add(2,moge);
    }
    public void hoge(int a)
    {
        Debug.Log(111);
    }
    public void moge(int a)
    {
        Debug.Log("あああ");
    }

    public void hage(int a)
    {
        Debug.Log(111);
        Debug.Log("あああ");
    }
    public void aaa()
    {
        /*
        action = a =>  Debug.Log(a);
        action += hoge;
        action += moge;
        action -= hoge;
        action(1);
        */
        action = hoge;
        action += moge;

        action(1);
        hage(1);

    }
    public void fff(Action<int> ac)
    {
        ac(1);
    }
    public void ggg(Action<int> ac)
    {
        ac(2);
    }

    public static Dictionary<Role, (int l, int c, int r)> dic = new Dictionary<Role, (int l, int c, int r)>()
    {
        {Role.NONE,(0,0,0) },
        {Role.STRONGCHERRY,(1,1,1)},
        {Role.CHERRY,(1,1,0)  },
        {Role.WEAKCHERRY,(1, 0,0) },
        {Role.REGBONUS,(2, 2, 2) },
        {Role.WATERMELON,(3, 3, 3) },
    };

    void Start()
    {
        PushSlot();
    }
    void PushSlot()
    {
        //役生成
        int rand = UnityEngine.Random.Range(0, 5);
        
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
    BAR = 2,
    WATERMELON = 3,
    SEVEN = 4,
    BELL = 5,
    REPLAY = 6,
    QUESTION = 7
}
public enum Role
{
    NONE = 0,
    WEAKCHERRY = 1,
    CHERRY = 2,
    STRONGCHERRY = 3,
    REGBONUS = 4,
    WATERMELON = 5,
}
