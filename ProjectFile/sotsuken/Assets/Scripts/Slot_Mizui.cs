using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot_Mizui : MonoBehaviour
{
    public static void Start()
    {


        var test = new Slot_Mizui();
        var dataList = new List<int>()
        {
                -1,1000, 0, 770, 895, 945, 985, 993, 997, 999, 894, 944, 984, 992, 996, 998
        };
        foreach (var data in dataList)
        {
            Debug.Log(data);
    
        }
    }
   


    // 共通変数
    private int MaxLottery;     // spinボタン用フラグ

    // 小役位置返却メソッド
    public float LotteryKoyakuPoint()
    {
        // 抽選
        int lottery = Lottery();

        // 抽選結果から小役を選択
        Koyaku koyaku = DecideRole(lottery);

        // 小役からwin枚数判別処理
        //GameObject.Find("Canvas").GetComponent<CanvasController>().GetMedal(koyaku);

        // 抽選結果をもとに小役判定、位置をretrun
        return KoyakuPoint(koyaku);
    }

   

    // 抽選処理（ここの偏り一つで世界が変わる）
    private int Lottery()
    {
        var random = new System.Random();

        MaxLottery = 1000;

        // 0～999まで（MaxLottery個）の結果をreturn
        return random.Next(MaxLottery);

    }
    ///<summary>
    /// 抽選結果から小役振り分け（ここ次第で勝ち負けが大きく変わる）
    ///それぞれの小役に対応する番号と対応する抽選結果の範囲は以下の通り。
    /// 0：青　0～769　（約1.3分の1）
    /// 1：赤　770～894（8分の1）
    /// 2：BAR　945～984（20分の1）
    /// 3：スイカ　945～984（25分の1）
    /// 4：ベル  985～992（125分の1）
    /// 5：チェ　993～996（250分の1）
    /// 6：リプ　997～998（500分の1）
    /// 7：NONE　999     （1000分の1）
    /// </summary>
    public enum Koyaku
    {
        BLUE = 0,
        RED = 2,
        BAR = 8,
        WATERMELON = 32,
        BEL = 128,
        CHERRY = 256,
        REPLAY = 512,
        NONE = 999
    }
        private Koyaku DecideRole(int random)
        {
            
            foreach (int index in Enum.GetValues(typeof(Koyaku)))
            {
                if (random <= index)
                {
                    return (Koyaku)index;
                }
            }
            return 0;
        }
       

    ///<summary>
    /// 抽選結果をもとに小役判別して小役位置を返すメソッド
    /// それぞれの小役に対応する番号と位置は以下の通り。
    /// 0：NONE　-9.50f（本来は-10.20f、リール周期の都合上、-9.50f）
    /// 1：リプ　2.57
    /// 2：チェ　7.71
    /// 3：ベル　0.00
    /// 4：ｽｲｶ 　-2.57
    /// 5：バー　5.14
    /// 6：赤７　-7.71
    /// 7：青７　-5.14
    /// </summary>
    private float KoyakuPoint(Koyaku koyaku)
    {

            // リール位置返却
            switch (koyaku)
            {
                case Koyaku.NONE: return -9.50f;
                case Koyaku.REPLAY: return 2.57f;
                case Koyaku.CHERRY: return 7.71f;
                case Koyaku.BEL: return 0.00f;
                case Koyaku.WATERMELON: return -2.57f;
                case Koyaku.BAR: return 5.14f;
                case Koyaku.RED: return -7.71f;
                case Koyaku.BLUE: return -5.14f;
                default: return -9.50f;
            }
    }
}

