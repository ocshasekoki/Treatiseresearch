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

    
    public static Dictionary<(int,int,int), string> dic = new Dictionary<(int, int, int), string>()
    {
        {(1,1,1),"強チェリー"},
        {(1,1,2|3|4|5|6|7) ,"中チェリー" },
        {(1, 2|3|4|5|6|7, 2|3|4|5|6|7) ,"弱チェリー" },
    };

    void Start()
    {
        PushSlot();
    }
    void PushSlot()
    {
        List<Symbol> list = CreateSymbol();
        foreach(Symbol s in list)
        {
            Debug.Log(s.ToString());
            Debug.Log((int)s);
        }
    }

    List<Symbol> CreateSymbol()
    {
        List<Symbol> list = new List<Symbol>();
        Symbol sm = Symbol.CHERRY;
        for(int i = 0; i < 3; i++)
        {
            int ramd = Random.Range(1, 7);
            switch (ramd)
            {
                case 1:
                    sm = Symbol.CHERRY;
                    break;
                case 2:
                    sm = Symbol.BAR;
                    break;
                case 3:
                    sm = Symbol.BELL;
                    break;
                case 4:
                    sm = Symbol.QUESTION;
                    break;
                case 5:
                    sm = Symbol.REPLAY;
                    break;
                case 6:
                    sm = Symbol.SEVEN;
                    break;
                case 7:
                    sm = Symbol.WATERMELON;
                    break;
                default:
                    Debug.LogError("定義していない数字");
                    break;
            }
            list.Add(sm);
        }
        
        return list;
    }
}

public enum Symbol
{
    CHERRY = 1,
    BAR = 2,
    WATERMELON = 3,
    SEVEN = 4,
    BELL = 5,
    REPLAY = 6,
    QUESTION = 7
}
