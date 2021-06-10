using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Status
{
    public int hp;
    public int mp;
    public string name;
    public void Dump()
    {
        Debug.Log(hp);
        Debug.Log(mp);
        Debug.Log(name);
    }
}
public class Test : MonoBehaviour
{
    private void Start()
    {
        Status status1 = new Status();
        status1.hp = 100;
        status1.mp = 80;
        status1.name = "hoge";
        status1.Dump();

        Status status2 = new Status();
        status2.hp = 150;
        status2.mp = 50;
        status2.name = "moge";
        status2.Dump();
    }
    /* 非同期に動かすメソッド */
    IEnumerator Hoge()
    {
        yield return new WaitForSeconds(1f);
        Coin = 3;
        Debug.Log(Coin);
        StartCoroutine(Hoge());
    }
    public void Moge()
    {
        Debug.Log(Tinge());
        Debug.Log(Mange());
        Debug.Log(Ketsuge());
        Debug.Log(coin);
        Debug.Log(Coin);
        Coin = 3;
        Debug.Log(coin);
    }

    private string Tinge()
    {
        return "ちんげ"; 
    }

    private int Mange()
    {
        return 1;
    }

    private float Ketsuge()
    {
        return 1.5f;
    }

    int coin = 100;
    int Coin
    {
        //読み込まれるとき
        get { return coin; }
        //Coin = 10  格納されるとき
        //coin -=10
        set { coin -= value; }
    }
    public static void Poge()
    {
        Debug.Log("ぽげ");
    }
    public static IEnumerator<int> Unchi()
    {
        yield return 1;
        yield return 2;
        yield return 3;
        yield break;
    }
}

