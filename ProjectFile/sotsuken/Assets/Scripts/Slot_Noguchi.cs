using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Assets.LotteryCreator;

public class Slot_Noguchi : MonoBehaviour
{
    private bool SpinButtonFlg;
    private bool StopButtonFlg;

    private float TargetPointA;
    private float TargetPointB;

    LotteryCreator lottery = new LotteryCreator();
    void Start()
    {
        SpinButtonFlg = false;
        StopButtonFlg = false;
        TargetPointA = 0f;
        TargetPointB = 0f;
    }
    void Update()
    {
        if (StopButtonFlg &&
           (transform.position.y <= TargetPointA && transform.position.y > TargetPointB)
          )
        {
            SpinButtonFlg = false;
            StopButtonFlg = false;

            // win枚数反映処理
            GameObject.Find("Canvas").GetComponent<CanvasController>().WinMedal();
        }

        // spinボタンが押されたら
        if (SpinButtonFlg)
        {
            // リール回転処理
            transform.Translate(0, -0.6425f, 0);

            // リール位置が一定位置を超えたら先頭に戻る
            if (transform.position.y < -10.25f)
            {
                transform.position = new Vector3(0, 10.2f, 0);
            }
        }

    }

    //　画面上に配置しているボタンを押したときの処理
    void OnGUI()
    {
        // spinボタン
        if (GUI.Button(new Rect(10, 320, 100, 30), "bet and spin"))
        {

            // bet枚数反映処理
            GameObject.Find("Canvas").GetComponent<CanvasController>().InsertMedal();

            // リール停止位置AとBを取得
            TargetPointA = lottery.LotteryKoyakuPoint();
            TargetPointB = TargetPointA - 0.6425f;

            SpinButtonFlg = true;

        }

        // stopボタン
        if (GUI.Button(new Rect(385, 320, 100, 30), "stop"))
        {
            StopButtonFlg = true;
        }
    }

}

//ifの分岐を減らす
//listを使用してコードの分量を減らす
//とにかく減らして見やすくすること