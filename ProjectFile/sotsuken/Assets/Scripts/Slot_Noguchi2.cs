//https://qiita.com/hunyu/items/4547d1d9fb734d81d189
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.LotteryCreator
{
    public class LotteryCreator
    {
        private int MaxLottery;

        public float LotteryKoyakuPoint()
        {
            var lottery = Lottery();

            int koyaku = GetKoyaku(lottery);

            GameObject.Find("Canvas").GetComponent<Controller>().GetMedal(koyaku);

            return KoyakuPoint(koyaku);

        }

        private int Lottery()
        {
            var random = new System.Random();

            MaxLottery = 1000;

            return random.Next(MaxLottery);
        }

    // 確率記載（役振り分け）
    // ハズレ　1/1.3
    // 問題    1/8
    // チェ　  1/20
    // ベル    1/25
    // スイカ  1/125
    // バー    1/250
    // 777     1/500
        // 抽選結果から小役振り分け（ここ次第で勝ち負けが大きく変わる）
        // 【memo】
        // それぞれの小役に対応する番号と対応する抽選結果の範囲は以下の通り。
        // 0：ＢＲ　0～769　（約1.3分の1）
        // 1：リプ　770～894（8分の1）or 問題
        // 2：チェ　945～984（20分の1）
        // 3：ベル　945～984（25分の1）
        // 4：ｽｲｶ   985～992（125分の1）
        // 5：バー　993～996（250分の1）
        // 6：赤７　997～998（500分の1）
        // 7：青７　999     （1000分の1）

        private int GetKoyaku(int lottery)
        {
            if (lottery >= 0 && lottery <= 769) return 0;
            else if (lottery >= 770 && lottery <= 894) return 1;
            else if (lottery >= 895 && lottery <= 944) return 2;
            else if (lottery >= 945 && lottery <= 984) return 3;
            else if (lottery >= 985 && lottery <= 992) return 4;
            else if (lottery >= 997 && lottery <= 998) return 5;
            else if (lottery <= 999) return 7;
            else return 0;
        }
        private float KoyakuPoint(int koyaku)
        {
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
}
//リプレイどないしよ