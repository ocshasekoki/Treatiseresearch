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

        public static Dictionary<Role,int> rolecoin = new Dictionary<Role,int>() {
        {Role.NONE,0},
        {Role.STRONGCHERRY,3},
        {Role.CHERRY,2},
        {Role.WEAKCHERRY,1},
        {Role.WATERMELON,5},
        {Role.BELL,15 },
        {Role.REPLAY,3},
        {Role.QUESTION,8},
        {Role.REGBONUS,10},
        {Role.BIGBONUS,12},
        {Role.FREEZE,15 },
        };

        /// <summary>
        /// 状態に対応した色の演出の設定
        /// </summary>
        public static Dictionary<Condition, Color> concolor = new Dictionary<Condition, Color>()
        { { Condition.AT,Color.red},
          { Condition.CZ,Color.yellow},
          { Condition.BIGBONUS,Color.green},
          { Condition.BONUS,Color.cyan},
          { Condition.FREEZE,Color.black},
          { Condition.NOMAL,Color.clear},
        };
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
        public Config conf;         /// <summary>設定</summary>
        public Condition cond;      /// <summary>状態</summary>
        public Role role;           /// <summary>小役</summary>
        public int bonuspro;        /// <summary>レギュラーボーナス確率</summary>
        public int bigbonuspro;     /// <summary>ビッグボーナス確率</summary>
        public int freezepro;       /// <summary>フリーズ確率</summary>
        public int chancezonepro;   /// <summary>チャンスゾーンの確率</summary>
        public int appearpro;       /// <summary>出現確率</summary>

        /// <summary>
        /// 現在の状態を出力
        /// </summary>
        public void Dump()
        {
            Debug.Log("設定：" + conf + " 小役：" + role + " 状態：" + cond + " 出現確率：" + (double)appearpro/10 + "%\nフリーズ確率：" + (double)freezepro/100 + "% ビッグボーナス確率：" + (double)bigbonuspro+"% ボーナス確率：" + (double)bonuspro / 100 + "% CZ確率：" + (double)chancezonepro / 100+"%");
        }
    }
    [SerializeField]
    public class PlayerData
    {

        /// <summary>チャンスゾーンのフラグ </summary>
        protected bool chanceZone = false;
        /// <summary>ATのフラグ </summary>
        protected bool at = false;
        /// <summary>ボーナスのフラグ </summary>
        protected bool bonus = false;
        /// <summary>ビッグボーナスのフラグ </summary>
        protected bool bigBonus = false;
        /// <summary>フリーズのフラグ</summary>
        protected bool freeze = false;

        /// <summary>フリーズのプロパティ</summary>
        public bool Freeze
        {
            get { return freeze; }
            set { freeze = value; }
        }
        /// <summary>チャンスゾーンのプロパティ</summary>
        public bool CZ
        {
            get{return chanceZone; } 
            set{chanceZone = value; }
        }

        /// <summary>ATのプロパティ</summary>
        public bool AT
        {
            get { return at; }
            set { at = value; }
        }

        /// <summary>ボーナスのプロパティ</summary>
        public bool Bonus
        {
            get { return bonus; }
            set { bonus = value; }
        }

        /// <summary>ビッグボーナスのプロパティ</summary>
        public bool BigBonus
        {
            get { return bigBonus; }
            set { bigBonus = value; }
        }
    }
}


