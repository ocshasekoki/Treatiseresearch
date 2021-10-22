using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using EnumDic;
using Prob;
using Data;
using System.IO;

namespace Slot
{
    public class SlotMachine_hase : MonoBehaviour
    {
        ///<summary>symbolLeft:左の図柄</summary>
        protected Symbol symbolLeft = 0;
        ///<summary>symbolCenter:中央の図柄</summary> 
        protected Symbol symbolCenter = 0;
        /// <summary> symbolRight:右の図柄 </summary>
        protected Symbol symbolRight = 0;
        /// <summary>diction:役別の各確率が入ったディクショナリ</summary>
        protected Dictionary<Role, ProData> diction;
        /// <summary>condition:確率の状態（低確、高確、超高確）</summary>
        protected Condition condition = Condition.NOMAL;
        /// <summary>config:設定（LOW,MIDDLE,HIGH）</summary>
        protected Config config = 0;
        /// <summary> dic :すべての確率のリスト</summary>
        protected Dic dic;
        /// <summary>data:役に対応する図柄のディクショナリなどのデータが格納された関数</summary>
        protected DicData data;

        protected PlayerData pdata = new PlayerData();
        /// <summary>pro:確率の分母/ </summary>
        protected int pro = 0;
        /// <summary>role :現在の小役 </summary>
        protected Role role;

        /// <summary> leftsymbol:左の図柄のリスト</summary>
        protected List<GameObject> leftsymbol = null;
        /// <summary> centersymbol:中央の図柄のリスト</summary>
        protected List<GameObject> centersymbol = null;
        /// <summary>rightReal:右のとまる場所 </summary>
        protected List<GameObject> rightsymbol = null;

        /// <summary> leftReal:左のとまる場所 </summary>
        [SerializeField] protected GameObject leftReal = null;
        /// <summary>centerReal:真ん中のとまる場所 </summary>
        [SerializeField] protected GameObject centerReal = null;
        /// <summary>rightReal:右のとまる場所</summary>
        [SerializeField] protected GameObject rightReal = null;
        /// <summary> effectArea:エフェクト発生のエリア </summary>
        [SerializeField] protected GameObject effectArea = null;
        /// <summary>colorTest:色（テスト用） </summary>
        [SerializeField] protected GameObject colorTest = null;

        [SerializeField] protected GameObject mondaipanel;
        /// <summary>configDD: 設定の変更のドロップダウン</summary>
        [SerializeField] protected Dropdown configDD = null;
        Dictionary<Role, GameObject> prefDic = null;
        /// <summary>m:テスト用の問題データ</summary>
        [SerializeField] protected Mondai m = null;
        /// <summary>mondaiText:問題のテキスト</summary>
        [SerializeField] Text mondaiText = null;
        /// <summary>gogunText:語群のテキストの配列</summary>
        [SerializeField] Text[] gogunText = null;
        [SerializeField] Text coinText = null;
        [SerializeField] Text gameCounterText = null;
        protected int bonusgrace = 0;
        protected int chancegrace = 0;

        /// <summary>betcoin:掛け金</summary>
        protected const int betcoin = 3;
        /// <summary>realcon:リールの状態</summary>
        protected int realcon = 0;
        protected bool[] rotate;
        public void Start()
        {
            Invisible();
            prefDic = new Dictionary<Role, GameObject>();
            realcon = 0;
            prefDic.Clear();
            foreach (Role r in Enum.GetValues(typeof(Role)))
            {
                prefDic.Add(r, PrefLoad(r));
            }

            config = (Config)UnityEngine.Random.Range(0, 2);
            condition = Condition.NOMAL;
            dic = Prodic.LoadDic();
            ChangeMode(dic);

            CoinText();
            leftsymbol = SetReal(leftReal);
            centersymbol = SetReal(centerReal);
            rightsymbol = SetReal(rightReal);

            SetConfigDD();
        }
        /// <summary>
        /// 指定したリールの絵柄すべてをリストに格納する。
        /// 
        /// </summary>
        /// <param name="obj">各リールの親</param>
        /// <returns>絵柄のリスト</returns>
        protected List<GameObject> SetReal(GameObject obj)
        {
            List<GameObject> list = new List<GameObject>();
            for (int i = 0; i < obj.transform.childCount; i++)
            {
                list.Add(obj.transform.GetChild(i).gameObject);
            }
            return list;
        }

        public void PushBet()
        {
            if ((pdata.Coin - betcoin < 0 || realcon != (int)Real.NOBET)&&realcon != (int)Real.ALLSTOP)
            {
                Debug.Log("コインが足りないかベット済");
                return;
            }
            Debug.Log("ベット");
            pdata.Coin -= betcoin;
            CoinText();
            realcon = (int)Real.BET;
        }
        /// <summary>
        /// ればーおん！
        /// </summary>
        public void LeverOn()
        {
            if (realcon == (int)Real.BET)
            {
                RandomRole();
                RealRotate();
                GameCounter();
            }
        }
        /// <summary>
        /// リールを回すスクリプト
        /// </summary>
        protected void RealRotate()
        {
            foreach (GameObject g in leftsymbol)
            {
                g.GetComponent<SymbolScript>().RealStart(20);
            }
            foreach (GameObject g in centersymbol)
            {
                g.GetComponent<SymbolScript>().RealStart(20);
            }
            foreach (GameObject g in rightsymbol)
            {
                g.GetComponent<SymbolScript>().RealStart(20);
            }
            rotate = new bool[3];
            for (int i = 0; i < rotate.Length; i++)
            {
                rotate[i] = true;
            }
            realcon = (int)Real.ROTATE;
        }

        /// <summary>
        /// ランダムな数値を出す
        /// </summary>
        /// <param name="condition">現在のスロットの状態</param>
        protected void RandomRole()
        {
            int rand = UnityEngine.Random.Range(1, pro);
            role = DecideRole(rand);
            SetColor(role, colorTest);
            CreatePrefab(role);
            DecideSymbol(role);
            if (role == Role.QUESTION) SetMondai();
        }

        /// <summary>
        /// 役に対しての演出設定
        /// </summary>
        /// <param name="r"></param>
        protected void CreatePrefab(Role r)
        {
            //Debug.Log(r.ToString());
            Instantiate(prefDic[r], effectArea.transform);
        }

        /// <summary>
        /// 役からそろう柄を決定するメソッド
        /// <param name="rand">生成された乱数</param>
        /// </summary>
        protected void DecideSymbol(Role r)
        {
            Debug.Log("小役:" + r + " ID:" + diction[r]);
            Debug.Log(DicData.symbolDic[r]);
            symbolLeft = DicData.symbolDic[r].l;
            symbolCenter = DicData.symbolDic[r].c;
            symbolRight = DicData.symbolDic[r].r;
            Debug.Log("左：" + symbolLeft + "　中：" + symbolCenter + "　右：" + symbolRight);
        }

        /// <summary>
        /// 数値に対応する役を抽出するメソッド
        /// </summary>
        /// <param name="random">確率の分母となる引数</param>
        /// <returns>enumの役の格納ナンバー</returns>
        public Role DecideRole(int random)
        {
            int sum = 0;
            foreach (Role r in diction.Keys)
            {
                sum += diction[r].appearpro;
                if (random <= sum)
                {
                    return r;
                }
            }
            return 0;
        }



        /// <summary>
        /// 小役QUESTION(問題)で問題を答えた時に動かすメソッド
        /// </summary>
        /// <param name="currect">正解かどうか</param>
        /// <returns>連続正解した数＊設定、状態に対応した確率</returns>
        public void AnswerDicision(bool currect)
        {
            //if (currect) data.Cor++;
            //else data.Cor = 0;
        }
        /// <summary>
        /// 状態に対応したdictionaryに変更する関数
        /// </summary>
        /// <param name="dic"></param>
        protected void ChangeMode(Dic dic)
        {
            pro = 0;
            diction = Prodic.GetPro(dic, config, condition);
            foreach (Role r in diction.Keys)
            {
                pro += diction[r].appearpro;
            }
        }
        /// <summary>
        /// 押されたボタンを判定して、対応する箇所の図柄を止める
        /// </summary>
        /// <param name="pos"></param>
        public void btnPush(string pos)
        {
            Position p = (Position)Enum.Parse(typeof(Position), pos);
            switch (p)
            {
                case Position.LEFT:
                    if (rotate[(int)Position.LEFT]) AssistDicision(leftsymbol, (Symbol)DicData.symbolDic[role].l);
                    break;
                case Position.MIDDLE:
                    if (rotate[(int)Position.MIDDLE]) AssistDicision(centersymbol, (Symbol)DicData.symbolDic[role].c);
                    break;
                case Position.RIGHT:
                    if (rotate[(int)Position.RIGHT]) AssistDicision(rightsymbol, (Symbol)DicData.symbolDic[role].r);
                    break;
            }
        }


        /// <summary>
        /// 取ってきた図柄をsmに入れる。図柄と役が一致しているか判定。
        /// </summary>
        /// <param name="list"></param>
        /// <param name="s"></param>
        protected void AssistDicision(List<GameObject> list, Symbol s)
        {
            if (realcon < (int)Real.ROTATE && realcon > (int)Real.ALLSTOP)
            {
                return;
            }
            if (s == Symbol.NONE)
            {
                s = (Symbol)UnityEngine.Random.Range(2, 7);
            }
            foreach (GameObject obj in list)
            {
                Symbol sm = obj.GetComponent<SymbolData>().GetSymbol();
                if (obj.transform.localPosition.y >= 0f && sm == s)
                {
                    StartCoroutine(Assist(obj));
                }
            }
        }


        /// <summary>
        /// 役に対応した図柄をアシストする
        /// </summary>
        /// <param name="obj">図柄に対応したオブジェクト</param>
        /// <returns></returns>
        protected IEnumerator Assist(GameObject obj)
        {
            yield return new WaitWhile(() => obj.transform.localPosition.y >= 10f);
            obj.GetComponent<SymbolScript>().RealStop();
            AllRealStop(obj.GetComponent<SymbolData>().GetPos());

            Debug.Log((Real)realcon);
        }

        /// <summary>
        /// ボタンを押したとき全ての図柄を止める。
        /// </summary>
        /// <param name="p">リールの位置</param>
        protected void AllRealStop(Position p)
        {
            realcon++;
            if ((Real)realcon == Real.ONESTOP && role == Role.QUESTION)
            {
                AnswerDicision(Answer(p));
            }
            switch (p)
            {
                case Position.LEFT:
                    rotate[(int)Position.LEFT] = false;
                    foreach (GameObject obj in leftsymbol)
                    {
                        obj.GetComponent<SymbolScript>().RealStop();
                    }
                    break;
                case Position.MIDDLE:
                    rotate[(int)Position.MIDDLE] = false;
                    foreach (GameObject obj in centersymbol)
                    {
                        obj.GetComponent<SymbolScript>().RealStop();
                    }
                    break;
                case Position.RIGHT:
                    rotate[(int)Position.RIGHT] = false;
                    foreach (GameObject obj in rightsymbol)
                    {
                        obj.GetComponent<SymbolScript>().RealStop();
                    }
                    break;
            }
            if ((Real)realcon == Real.ALLSTOP)
            {
                NomalJudge(role);
                NomalJudgeTest();
                RoleJudgement(role);
            }
        }

        /// <summary>
        /// 判定のテスト
        /// </summary>
        protected void NomalJudgeTest()
        {
            Debug.Log("BiGBonusの状態;" + pdata.BigBonus) ;
            Debug.Log("Bonusの状態;" + pdata.Bonus) ;
            Debug.Log("CZの状態;" + pdata.CZ) ;
            Debug.Log("Freezeの状態;" + pdata.Freeze);


        }

        /// <summary>
        /// 演出に合わせた色を出力する。
        /// </summary>
        /// <param name="r">小役</param>
        /// <param name="obj">演出のオブジェクト</param>
        [Obsolete]
        protected void SetColor(Role r, GameObject obj)
        {
            Debug.Log(DicData.rolecolor[r]);
            obj.GetComponent<ParticleSystem>().startColor = DicData.rolecolor[r];
        }

        /// <summary>
        ///　設定でモードを変更する
        /// </summary>
        public void SetConfig()
        {
            config = (Config)configDD.value;
            ChangeMode(dic);
        }

        /// <summary>
        /// コンフィグのドロップダウンの中身を書き換える処理
        /// </summary>
        public void SetConfigDD()
        {
            string[] ops = Enum.GetNames(typeof(Config));
            List<string> ddvalues = new List<string>();
            foreach (string typename in ops)
            {
                ddvalues.Add(typename);
            }
            configDD.ClearOptions();
            configDD.AddOptions(ddvalues);
        }

        /// <summary>
        /// 小役に対応したResources内のプレハブを読み込む
        /// </summary>
        /// <param name="r">小役</param>
        /// <returns>小役に対応したゲームオブジェクト</returns>
        protected GameObject PrefLoad(Role r)
        {
            GameObject obj = null;
            Debug.Log(r);
            obj = Resources.Load<GameObject>("Prefabs/" + r + "_pref");
            Debug.Log(obj.name);
            return obj;
        }
        /// <summary>
        /// 小役別の処理
        /// </summary>
        /// <param name="r">小役</param>
        protected void RoleJudgement(Role r)
        {
            switch (r)
            {
                case Role.BELL:
                    pdata.Coin += 15;
                    break;
                case Role.REPLAY:
                    realcon = (int)Real.BET;
                    break;
                case Role.BIGBONUS:
                    break;
                case Role.CHERRY:
                    pdata.Coin += 3;
                    break;
                case Role.FREEZE:
                    break;
                case Role.NONE:
                    break;
                case Role.QUESTION:
                    pdata.Coin += 8;
                    break;
                case Role.REGBONUS:
                    break;
                case Role.STRONGCHERRY:
                    pdata.Coin += 3;
                    break;
                case Role.WATERMELON:
                    pdata.Coin += 5;
                    break;
                case Role.WEAKCHERRY:
                    pdata.Coin += 1;
                    break;
            }
            Debug.Log(pdata.Coin);
            CoinText();
        }
        /// <summary>
        /// ATの当たり判定
        /// </summary>
        /// <param name="prob">確率(prob/10000)</param>
        /// <returns>当たり判定の真偽</returns>
        protected static bool ATjudge (int prob) 
        {
            int rand = UnityEngine.Random.Range(1, 10000);
            if (rand <= prob) return true;
            return false;
        }
        /// <summary>
        /// ボーナス判定
        /// </summary>
        /// <param name="percent">ボーナス確率(%)</param>
        /// <returns>ボーナスの判定</returns>
        public static bool Judge(int percent)
        {
            int rand = UnityEngine.Random.Range(1, 10000);
            if (rand <= percent) return true;
            return false;
        }

        /// <summary>
        /// 通常時の役の判定
        /// </summary>
        /// <param name="r">小役</param>
        public void NomalJudge(Role r)
        {
            Debug.Log("BigBonus確率;" + diction[r].bigbonuspro);
            Debug.Log("RegBonus確率;" + diction[r].bonuspro);
            Debug.Log("ChanceZone確率;" + diction[r].chancezonepro);
            Debug.Log("Freeze確率;" + diction[r].freezepro);

            if (pdata.Freeze)
            {
                if (pdata.GameCounter == bonusgrace)
                {
                    condition = Condition.FREEZE;
                }
            }
            else if (!pdata.BigBonus&&!pdata.Bonus)
{
                pdata.Freeze = Judge(diction[r].freezepro);
                if (pdata.Freeze) bonusgrace = pdata.GameCounter + UnityEngine.Random.Range(1, 8);
            }

            if (pdata.BigBonus)
            {
                if (pdata.GameCounter == bonusgrace)
                {

                    condition = Condition.BIGBONUS;


                }
            }
            else if (!pdata.Bonus&&!pdata.Freeze)
            {
                pdata.BigBonus = Judge(diction[r].bigbonuspro);
                if (pdata.BigBonus) bonusgrace = pdata.GameCounter + UnityEngine.Random.Range(1, 8);
            }

            if (pdata.Bonus)
            {
                if (pdata.GameCounter == bonusgrace)
                {
                    condition = Condition.BONUS;

                }
            }
            else if (!pdata.BigBonus && !pdata.Freeze)
            {
                pdata.Bonus = Judge(diction[r].bonuspro);
                if(pdata.Bonus)bonusgrace = pdata.GameCounter + UnityEngine.Random.Range(1, 8);
            }
            if(!pdata.CZ)pdata.CZ = Judge(diction[r].chancezonepro);

            Debug.Log(bonusgrace);
            Debug.Log(condition);
        }
        


        /// <summary>
        /// 状態を変更
        /// </summary>
        /// <param name="con">状態</param>
        protected void ConTransition(Condition con)
        {
            condition = con;
        }
        /// <summary>
        /// 問題を生成し、表示させるプログラム
        /// </summary>
        protected void SetMondai()
        {
            Syutudai();
            mondaiText.text = m.GetMondaiText();
            //Mondai mondai = GetMondai(role);
            //リスト初期化
            List<int> numbers = new List<int>();
            //ボタンの数だけ数値を用意({0,1,2})
            for (int i = 0; i < 3; i++)
            {
                //リストに入れる
                numbers.Add(i);
            }
            //リストが１つ以上データがあるとき
            while (numbers.Count > 0)
            {
                //リストの長さだけ乱数生成
                int index = UnityEngine.Random.Range(0, numbers.Count);
                //「乱数で出た数値」番目を取り出す
                int ransu = numbers[index];
                //テキストを設置する
                gogunText[numbers.Count-1].text = m.GetT(ransu);
                //入れた数値をリストから削除する
                numbers.RemoveAt(index);
            }
        }
        /// <summary>
        /// 正解の判定
        /// </summary>
        /// <param name="p">リールの列</param>
        /// <returns>正解の判定</returns>
        protected bool Answer(Position p)
        {
            Invisible();
            if (gogunText[2-(int)p].text == m.GetAnswer())
            {
                
                Debug.Log("正解しました");
                return true;
            }
            Debug.Log("不正解");
            return false;
         
        }
        /// <summary>
        /// 問題をロードするクラス
        /// </summary>
        /// <param name="r">小役</param>
        /// <returns>問題のデータ</returns>
        protected static Mondai GetMondai(Role r)
        {           
            string path = Application.streamingAssetsPath + "/question/" + r + ".json";
            string str = File.ReadAllText(path);
            return JsonUtility.FromJson<Mondai>(str);
        }

        /// <summary>問題パネルの表示 </summary>
        public void Syutudai()
        {
            mondaipanel.SetActive(true);
        }

        /// <summary>問題パネルの非表示 </summary>
        public void Invisible()
        {
            mondaipanel.SetActive(false);
        }

        public void CoinText()
        {
            coinText.text = pdata.Coin.ToString();
        }
        protected void GameCounter()
        {
            pdata.GameCounter++;
            gameCounterText.text = pdata.GameCounter.ToString();
        }
        
    }
}

