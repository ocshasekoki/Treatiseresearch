using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot_Mizui : MonoBehaviour
{
    public static void Main()
    {
        // Your code here!

        var test = new Slot_Mizui();
        var dataList = new List<int>(){
            -1,1000, 0, 770, 895, 945, 985, 993, 997, 999, 894, 944, 984, 992, 996, 998
        };
        foreach (var data in dataList)
        {
            System.Console.WriteLine(data);
            System.Console.WriteLine(GetKoyaku(data));
        }
    }
    // 抽選関連の処理をまとめたクラス


        // 共通変数
        private int MaxLottery;     // spinボタン用フラグ

        // 小役位置返却メソッド
        public float LotteryKoyakuPoint()
        {
            // 抽選
            var lottery = Lottery();

            // 抽選結果から小役を選択
            int koyaku = GetKoyaku(lottery);

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

        // 抽選結果から小役振り分け（ここ次第で勝ち負けが大きく変わる）
        // それぞれの小役に対応する番号と対応する抽選結果の範囲は以下の通り。
        // 0：ＢＲ　0～769　（約1.3分の1）
        // 1：リプ　770～894（8分の1）
        // 2：チェ　945～984（20分の1）
        // 3：ベル　945～984（25分の1）
        // 4：ｽｲｶ   985～992（125分の1）
        // 5：バー　993～996（250分の1）
        // 6：赤７　997～998（500分の1）
        // 7：青７　999     （1000分の1）
        public static int GetKoyaku(int lottery)
        {
            if (lottery <= 769 || lottery > 999) return 0;

            var result = 1;
            var conditionLists = new List<List<int>>(6){
                new List<int>(2){770, 894},
                new List<int>(2){895,944},
                new List<int>(2){945,984},
                new List<int>(2){985,992},
                new List<int>(2){993,996},
                new List<int>(2){997,998},
            };

            foreach (var conditionList in conditionLists)
            {
                if (lottery >= conditionList[0] && lottery <= conditionList[1]) return result;
                result += 1;
            }
            return result;

        }
    

    // 抽選結果をもとに小役判別して小役位置を返すメソッド
    // それぞれの小役に対応する番号と位置は以下の通り。
    // 0：ＢＲ　-9.50f（本来は-10.20f、リール周期の都合上、-9.50f）
    // 1：リプ　2.57
    // 2：チェ　7.71
    // 3：ベル　0.00
    // 4：ｽｲｶ 　-2.57
    // 5：バー　5.14
    // 6：赤７　-7.71
    // 7：青７　-5.14
    private float KoyakuPoint(int koyaku)
    {

        // リール位置返却
        switch (koyaku)
        {
            case 0: return -9.50f;
            case 1: return 2.57f;
            case 2: return 7.71f;
            case 3: return 0.00f;
            case 4: return -2.57f;
            case 5: return 5.14f;
            case 6: return -7.71f;
            case 7: return -5.14f;
            default: return -9.50f;
        }
    }

}

