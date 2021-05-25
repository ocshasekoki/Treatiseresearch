using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start() ///オブジェクトが生成、読み込まれた時に動く///
    {
        Tinko();
        StartCoroutine(Hikakin());
    }

    // Update is called once per frame
    void Update() /*フレーム毎に呼ばれる*/
    {

    }

    IEnumerator Hikakin() /*非同期に動かすメソッド*/
    {
        yield return new WaitForSeconds(1f);
        Tinko();
        StartCoroutine(Hikakin());

    }
    private int Mange()
    {
        return 1;
    }
    private float Ketsuge()
    {
        return 1.5f;
    }
    private string Tinge()
    {
        return "ちんげ";
    }
    public void Tinko() ///なにも返さないときに使用
    {
        //Debug.Log(Tinge());
        //Debug.Log(Mange());
        //Debug.Log(Ketsuge());
       // Debug.Log(coin);
        //Debug.Log(Coin);
        Coin = 3;
        //Debug.Log(coin);
        Debug.Log(Coin);
 

    }
    int coin;
    int Coin
        {
        get { return coin; } //読み込まれるとき
        //Coin = 10 格納されるとき
        //coin -=10
        set { coin -= value; }
        }
    public void Test()
    {

    }
}
