using System;
using System.Collections.Generic;
using UnityEngine;
using EnumDic;
using Prob;

namespace Data
{
    public class DicData
    {
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

        protected int correctcount = 0;
        public int Cor
        {
            get { return correctcount; }
            set { correctcount = value; }
        }
        protected int coin;
        public int Coin
        {
            get { return coin; }
            set { coin = value; }
        }
    }
    /// <summary>
    /// ProDateのリスト
    /// </summary>
    [Serializable]
    public class Dic
    {
        public List<ProData> prodic = new List<ProData>();
    }

    [Serializable]
    public struct ProData
    {
        public Config conf;         //設定
        public Condition cond;      //状態
        public Role role;           //小役
        public int bonuspro;        //ボーナス確率
        public int bigbonuspro;
        public int freezepro;
        public int chancezonepro;
        public int appearpro;       //出現確率
        public void Dump()
        {
            Debug.Log("設定：" + conf + " 小役：" + role + " 状態：" + cond + " 出現確率：" + appearpro + " ボーナス確率：" + bonuspro);
        }
    }
}


