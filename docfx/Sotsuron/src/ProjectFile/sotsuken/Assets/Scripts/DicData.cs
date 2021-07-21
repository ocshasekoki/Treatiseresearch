using System;
using System.Collections.Generic;
using UnityEngine;
using EnumDic;
using Prob;

namespace Data
{
    /// <summary>
    /// 役のデータ
    /// </summary>
    public class DicData
    {
        /// <summary>
        /// 役に対応した図柄のディクショナリ
        /// </summary>
        public static Dictionary<Role, (Symbol l, Symbol c, Symbol r)> symbolDic = new Dictionary<Role, (Symbol l, Symbol c, Symbol r)>()
        {
        {Role.NONE,(Symbol.NONE,Symbol.NONE,Symbol.NONE)},
        {Role.STRONGCHERRY,(Symbol.CHERRY,Symbol.CHERRY,Symbol.CHERRY)},
        {Role.CHERRY,(Symbol.CHERRY,Symbol.CHERRY,Symbol.NONE)  },
        {Role.WEAKCHERRY,(Symbol.CHERRY,Symbol.NONE,Symbol.NONE) },
        {Role.WATERMELON,(Symbol.WATERMELON, Symbol.WATERMELON, Symbol.WATERMELON) },
        {Role.BELL,(Symbol.BELL, Symbol.BELL, Symbol.BELL) },
        {Role.REPLAY,(Symbol.REPLAY, Symbol.REPLAY, Symbol.REPLAY) },
        {Role.QUESTION,(Symbol.QUESTION, Symbol.QUESTION, Symbol.QUESTION) },
        {Role.REGBONUS,(Symbol.SEVEN, Symbol.SEVEN, Symbol.BAR) },
        {Role.BIGBONUS,(Symbol.SEVEN, Symbol.SEVEN, Symbol.SEVEN) },
        {Role.FREEZE,(Symbol.BAR, Symbol.BAR, Symbol.BAR) },
        };

        /// <summary>
        /// 役に対応した色の演出の設定
        /// </summary>
        public static Dictionary<Role, Color> rolecolor = new Dictionary<Role, Color>() {
        {Role.NONE,Color.clear },
        {Role.STRONGCHERRY,new Color(0.75f,0,0.75f)},
        {Role.CHERRY,  Color.red},
        {Role.WEAKCHERRY,new Color(1f,0.64f,1f) },
        {Role.WATERMELON,Color.green },
        {Role.BELL,Color.yellow },
        {Role.REPLAY,Color.blue },
        {Role.QUESTION,new Color(0.1333f,0.6039f,0.4509f) },
        {Role.REGBONUS,new Color(1f,0.3058f,0.4705f) },
        {Role.BIGBONUS,new Color(1f,0.3058f,0.4705f) },
        {Role.FREEZE,new Color(1f,0.7764f,0) },
        };
        /// <summary>
        /// 正解数のカウント
        /// </summary>
        protected int correctcount = 0;

        /// <summary>
        /// 正解数のカウントプロパティ
        /// </summary>
        public int Cor 
        {
            get { return correctcount; }
            set { correctcount = value; }
        }

        /// <summary>
        /// コインの枚数
        /// </summary>
        protected int coin;

        /// <summary>
        /// コインのプロパティ
        /// </summary>
        public int Coin
        {
            get { return coin; }
            set { coin = value; }
        }
    }
    /// <summary>
    /// 小役のすべてのデータ
    /// </summary>
    [Serializable]
    public class Dic
    {
        /// <summary>
        /// ProDataのリスト
        /// </summary>
        public List<ProData> prodic = new List<ProData>();
    }
    /// <summary>
    /// 状態、設定、小役それぞれに対応した小役の確率が入ったクラス
    /// </summary>
    [Serializable]
    public struct ProData
    {
        public Config conf;         /// <summary>設定/// </summary>
        public Condition cond;      /// <summary>状態/// </summary>
        public Role role;           /// <summary>小役/// </summary>
        public int bonuspro;        /// <summary>ボーナス確率/// </summary>
        public int bigbonuspro;     /// <summary>ビッグボーナス確率 /// </summary>
        public int freezepro;       /// <summary>フリーズ確率/// </summary>
        public int chancezonepro;   /// <summary>/// チャンスゾーンの確率</summary>
        public int appearpro;       /// <summary>出現確率/// </summary>
        
        /// <summary>
        /// 現在の状態を出力
        /// </summary>
        public void Dump()
        {
            Debug.Log("設定：" + conf + " 小役：" + role + " 状態：" + cond + " 出現確率：" + appearpro + " ボーナス確率：" + bonuspro);
        }
    }
}


