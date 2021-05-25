using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    //
    // オブジェクトが生成、読み込まれたとき動く
    //
    void Start()
    {
        //Moge();
        StartCoroutine(Hoge());
    }



    /*
     *フレーム毎呼ばれる 
     */
    void Update()
    {
        
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
}
