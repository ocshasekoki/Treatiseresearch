using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using EnumDic;
using Prob;
using Data;
using Mondai;
using DataBase;
using System.Linq;

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
        protected Dic dic = null;
        /// <summary>data:役に対応する図柄のディクショナリなどのデータが格納された関数</summary>
        protected DicData data = null;

        protected PlayerData pdata = new PlayerData();
        protected Rank rank = new Rank(); 
        /// <summary>pro:確率の分母/ </summary>
        protected int pro = 0;
        /// <summary>role :現在の小役 </summary>
        protected Role role = Role.NONE;
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
        [SerializeField] protected GameObject skyboxscript = null;
        [SerializeField] protected GameObject mondaipanel;
        [SerializeField] protected GameObject[] rolePrefList = new GameObject[11];
        [SerializeField] protected GameObject[] conPrefList = new GameObject[6];
        [SerializeField] protected Material[] BackGround = new Material[6];
        [SerializeField] protected GameObject expPref = null;
        Dictionary<Role, GameObject> prefDic = null;
        Dictionary<Condition, GameObject> conprefDic = null;
        /// <summary>mondaiText:問題のテキスト</summary>
        [SerializeField] Text mondaiText = null;

        [SerializeField] GameObject mondaiPanel = null;
        /// <summary>gogunText:語群のテキストの配列</summary>
        [SerializeField] protected Text[] gogunText = null;
        [SerializeField] protected Text coinText = null;
        [SerializeField] protected Text gameCounterText = null;
        [SerializeField] protected Text ccaText = null;
        [SerializeField] protected Text answerText = null;
        [SerializeField] protected Text resultText = null;
        [SerializeField] protected Text conditionText = null;
        [SerializeField] protected Text realconText = null;
        [SerializeField] protected Text gameReamainingText = null;
        [SerializeField] protected Text czCurrectText = null;

        protected int bonusgrace = 0;
        protected int chancegrace = 0;
        protected int reachgrace = 0;
        protected int atgrace = 0;

        protected int czGame = 10;
        protected int atGame = 30;
        protected int bonusVacation = 0;
        protected int bonusG = 20;
        protected int bigbonusG = 30;
        protected int freezeG= 50;
        protected int czCurrect = 0;
        protected string answer = null;
        protected string kaisetsu = null;
        protected List<string> mondaiList = null;
        protected string username;
        /// <summary>betcoin:掛け金</summary>
        protected const int betcoin = 3;
        /// <summary>realcon:リールの状態</summary>
        protected int realcon = 0;
        protected bool[] rotate = new bool[3];
        protected bool[] asisted = new bool[3];
        protected bool reach = false;

        public void Start()
        {
            StartCoroutine(GetRank());
            Invisible();
            prefDic = new Dictionary<Role, GameObject>();
            conprefDic = new Dictionary<Condition, GameObject>();
            realcon = 0;
            foreach (Role r in Enum.GetValues(typeof(Role))) prefDic.Add(r, PrefLoad(r));
            foreach (Condition cond in Enum.GetValues(typeof(Condition))) conprefDic.Add(cond, ConPrefLoad(cond));
            //config = (Config)UnityEngine.Random.Range(0, 2);
            config = 0;
            condition = Condition.NOMAL;
            dic = Prodic.LoadDic();
            ChangeMode();
            leftsymbol = SetReal(leftReal);
            centersymbol = SetReal(centerReal);
            rightsymbol = SetReal(rightReal);
        }
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space)|| Input.GetKeyDown(KeyCode.UpArrow))
            {
                realconText.text = ((Real)realcon).ToString();
                if ((rank.Coin - betcoin >= 0 &&( realcon == (int)Real.NOBET) || realcon == (int)Real.ALLSTOP)) PushBet();
                if (realcon == (int)Real.BET) LeverOn();
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow)) btnPush("LEFT");
            if (Input.GetKeyDown(KeyCode.DownArrow)) btnPush("MIDDLE");
            if (Input.GetKeyDown(KeyCode.RightArrow)) btnPush("RIGHT");
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
        /// <summary>
        /// ベットボタンを押したときの処理
        /// </summary>
        public void PushBet()
        {
            if ((rank.Coin - betcoin < 0 || realcon != (int)Real.NOBET)&&realcon != (int)Real.ALLSTOP)
            {
                return;
            }
            for (int i = 0; i < rotate.Length; i++) rotate[i] = false;
            for (int i = 0; i < asisted.Length; i++) asisted[i] = false;
            DeleteEffect(effectArea);
            PanelVisible(false);
            rank.Coin -= betcoin;
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
                if (reach) EsEffect();
                Debug.Log("状態:"+condition.ToString());
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
            Instantiate(prefDic[r], effectArea.transform);
        }

        /// <summary>
        /// 役からそろう柄を決定するメソッド
        /// <param name="rand">生成された乱数</param>
        /// </summary>
        protected void DecideSymbol(Role r)
        {
            symbolLeft = DicData.symbolDic[r].l;
            symbolCenter = DicData.symbolDic[r].c;
            symbolRight = DicData.symbolDic[r].r;
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
                if (random <= sum)return r;
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
            if (currect)
            {
                rank.AnsSuc++;
                rank.Ansnumber++;
                GameObject pref =Instantiate(expPref, effectArea.transform);
                StartCoroutine(ObjDest(pref,3f));
            }
            else rank.AnsSuc = 0;
            if (condition == Condition.CZ&&currect)
            {
                rank.Ansnumber ++; 
                czCurrect++;
            }
            StartCoroutine(Result(currect));
            SetRank();
        }
        protected IEnumerator ObjDest(GameObject obj,float time)
        {
            yield return new WaitForSeconds(time);
            Destroy(obj);
        }

        protected void CurrentText()
        {
            float percent = 0f;
            int cca = 0;
            if (condition == Condition.CZ)
            {
                percent = czCurrect * diction[Role.QUESTION].chancezonepro / 100;
                cca = czCurrect;
            }
            else
            {
                percent = rank.AnsSuc * diction[Role.QUESTION].chancezonepro / 100;
                cca = rank.AnsSuc;
            }
            czCurrectText.text = percent.ToString();
            ccaText.text = cca.ToString();

        }
        /// <summary>
        /// 状態に対応したdictionaryに変更する関数
        /// </summary>
        /// <param name="dic"></param>
        protected void ChangeMode()
        {
            conditionText.text = condition.ToString();
            RenderSettings.skybox = BackGround[(int)condition];
            skyboxscript.GetComponent<SkyBoxRotate>().SetSkybox(BackGround[(int)condition]);
            pro = 0;
            diction = Prodic.GetPro(dic, config, condition);
            foreach (Role r in diction.Keys)
            {
                pro += diction[r].appearpro;
            }
            reach = false;
            EsEffect();
            StartCoroutine(DeleteEffect(0));
        }

        /// <summary>
        /// 押されたボタンを判定して、対応する箇所の図柄を止める
        /// </summary>
        /// <param name="pos"></param>
        public void btnPush(string pos)
        {
            if (realcon < 2 || realcon > 4) return;
            Position p = (Position)Enum.Parse(typeof(Position), pos);
            if (asisted[(int)p]) return;
            switch (p)
            {
                case Position.LEFT:
                    if (rotate[(int)Position.LEFT]) AssistDicision(leftsymbol, (Symbol)DicData.symbolDic[role].l,Position.LEFT);
                    break;
                case Position.MIDDLE:
                    if (rotate[(int)Position.MIDDLE]) AssistDicision(centersymbol, (Symbol)DicData.symbolDic[role].c, Position.MIDDLE);
                    break;
                case Position.RIGHT:
                    if (rotate[(int)Position.RIGHT]) AssistDicision(rightsymbol, (Symbol)DicData.symbolDic[role].r, Position.RIGHT);
                    break;
            }
        }
        /// <summary>
        /// 取ってきた図柄をsmに入れる。図柄と役が一致しているか判定。
        /// </summary>
        /// <param name="list"></param>
        /// <param name="s"></param>
        protected void AssistDicision(List<GameObject> list, Symbol s,Position p)
        {
            if (realcon < (int)Real.ROTATE && realcon > (int)Real.ALLSTOP) return;
            if (s == Symbol.NONE)s = (Symbol)UnityEngine.Random.Range(2, 7);
            asisted[(int)p] = true;
            GameObject nearObj = null;
            foreach (GameObject obj in list)
            {
                Symbol sm = obj.GetComponent<SymbolData>().GetSymbol();
                if (obj.transform.position.y >=20f&&sm == s)nearObj = obj;
                if (obj == list.Last() && nearObj != null) 
                {
                    rotate[(int)p] = false;
                    StartCoroutine(Assist(nearObj)); 
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
            realcon++;
        }

        /// <summary>
        /// ボタンを押したとき全ての図柄を止める。
        /// </summary>
        /// <param name="p">リールの位置</param>
        protected void AllRealStop(Position p)
        {
            if ((Real)realcon == Real.ROTATE && role == Role.QUESTION)
            {
                AnswerDicision(Answer(p));
            }
            switch (p)
            {
                case Position.LEFT:
                    foreach (GameObject obj in leftsymbol)
                    {
                        obj.GetComponent<SymbolScript>().RealStop();
                    }
                    break;
                case Position.MIDDLE:
                    foreach (GameObject obj in centersymbol)
                    {
                        obj.GetComponent<SymbolScript>().RealStop();
                    }
                    break;
                case Position.RIGHT:
                    foreach (GameObject obj in rightsymbol)
                    {
                        obj.GetComponent<SymbolScript>().RealStop();
                    }
                    break;
            }
            if ((Real)realcon == Real.TWOSTOP)
            {
                BonusJudge(role);
                CZATjudge(role);
                RoleJudgement(role);
            }
        }

        /// <summary>
        /// 小役に対応したResources内のプレハブを読み込む
        /// </summary>
        /// <param name="r">小役</param>
        /// <returns>小役に対応したゲームオブジェクト</returns>
        protected GameObject PrefLoad(Role r)
        {
            GameObject obj = rolePrefList[(int)r];
            return obj;
        }
        protected GameObject ConPrefLoad(Condition cond)
        {
            GameObject obj = conPrefList[(int)cond];
            return obj;
        }
        /// <summary>
        /// 小役別の処理
        /// </summary>
        /// <param name="r">小役</param>
        protected void RoleJudgement(Role r)
        {
            rank.Coin += DicData.rolecoin[r];
            if (r != Role.NONE || r != Role.REPLAY || r != Role.BELL||r!=Role.QUESTION) ReachIsOn();
            if (r== Role.QUESTION) ccaText.text = rank.AnsSuc.ToString();
            if (reach && reachgrace == rank.Count) reach = false;
            CoinText();
        }
        /// <summary>
        /// ATの当たり判定
        /// </summary>
        /// <param name="prob">確率(prob/10000)</param>
        /// <returns>当たり判定の真偽</returns>
        protected void CZATjudge(Role r) 
        {
            int per = 0;
            if (r == Role.QUESTION) per = diction[r].chancezonepro * rank.AnsSuc;
            else per = diction[r].chancezonepro;
            
            //AT
            if (condition == Condition.AT)
            {
                GameRemaining(chancegrace - rank.Count);
                if (chancegrace != rank.Count) return;
                FinishAT();
            }
            //AT当選中
            if (pdata.AT && atgrace == rank.Count)
            {
                StartAT();
                return;
            }
            //CZ
            if (condition == Condition.CZ&&!pdata.AT)
            {
                GameRemaining(atgrace - rank.Count);
                if (atgrace != rank.Count) return;
                per = diction[r].chancezonepro * czCurrect;
                pdata.AT = Judge(per);
                if (atgrace == rank.Count && !pdata.AT) ResetGame();
                if (pdata.AT) StartAT();
                return;
            }
            //CZ当選中
            if (pdata.CZ&&chancegrace == rank.Count)
            {
                pdata.CZ = false;
                condition = Condition.CZ;
                ChangeMode();
                czCurrect = 0;
                czCurrectText.text = czCurrect.ToString();
                atgrace = czGame + rank.Count;
                return;
            }
            //Nomal
            if (!pdata.CZ) pdata.CZ = Judge(per);
            if (!pdata.CZ) return;
            switch (condition) 
            {
                case Condition.BONUS:
                    chancegrace = bonusVacation;
                    return;
                case Condition.BIGBONUS:
                    chancegrace = bonusVacation;
                    return;
                case Condition.FREEZE:
                    pdata.CZ = false;
                    pdata.AT = true;
                    atgrace = bonusVacation;
                    return;
                default:
                    chancegrace = rank.Count + UnityEngine.Random.Range(1, 8);
                    return;
            }
        }

        protected void FinishAT()
        {
            czCurrect = 0;
            condition = Condition.CZ;
            ChangeMode();
            atgrace = czGame + rank.Count;
        }
        protected void StartAT()
        {
            pdata.AT = false;
            condition = Condition.AT;
            chancegrace = rank.Count + atGame;
            ChangeMode();
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
        public void GameRemaining(int gcont)
        {        
            gameReamainingText.text = gcont.ToString();
        }

        public void ResetGame()
        {
            gameReamainingText.text = "";
            condition = Condition.NOMAL;
            ChangeMode();
        }
        /// <summary>
        /// 通常時の役の判定
        /// </summary>
        /// <param name="r">小役</param>
        public void BonusJudge(Role r)
        {
            if (condition == Condition.CZ) return;
            int mag;
            if (r == Role.QUESTION) mag = rank.Ansnumber;
            else mag = 1;
            //フリーズ当選時
            if (pdata.Freeze)
            {
                //当選ゲーム数の時
                if (rank.Count == bonusgrace)
                {
                    condition = Condition.FREEZE;
                    bonusVacation = freezeG + rank.Count;
                    ChangeMode();
                }
                if(rank.Count == bonusVacation)
                {
                    pdata.Freeze = false;
                    StartAT();
                }
                if(rank.Count<bonusVacation) GameRemaining(bonusVacation - rank.Count);
                return;
            }
            //ビッグボーナス当選時
            if (pdata.BigBonus)
            {
                if (rank.Count == bonusgrace)
                {
                    condition = Condition.BIGBONUS;
                    bonusVacation = bigbonusG + rank.Count;
                    ChangeMode();
                }
                if (bonusVacation == rank.Count)
                {
                    condition = Condition.CZ;
                    ChangeMode();
                    czCurrect = 0;
                    czCurrectText.text = czCurrect.ToString();
                    atgrace = czGame + rank.Count;
                    pdata.BigBonus = false;
                }
                if (rank.Count < bonusVacation) GameRemaining(bonusVacation - rank.Count);
                return;
            }
            //ボーナス当選時
            if (pdata.Bonus)
            {
                if (rank.Count == bonusgrace)
                {
                    condition = Condition.BONUS;
                    bonusVacation = bonusG + rank.Count;
                    ChangeMode();
                }
                if (bonusVacation == rank.Count)
                {
                    condition = Condition.CZ;
                    ChangeMode();
                    czCurrect = 0;
                    czCurrectText.text = czCurrect.ToString();
                    atgrace = czGame + rank.Count;
                    pdata.Bonus = false;
                }
                if (rank.Count < bonusVacation) GameRemaining(bonusVacation - rank.Count);
                return;
            }

            //非当選時
            pdata.Freeze = Judge(diction[r].freezepro*mag);
            if(!pdata.Freeze)pdata.BigBonus = Judge(diction[r].bigbonuspro * mag);
            if(!pdata.Freeze&&!pdata.BigBonus)pdata.Bonus = Judge(diction[r].bonuspro * mag);
            if(pdata.Bonus|| pdata.Bonus|| pdata.Bonus) bonusgrace = rank.Count + UnityEngine.Random.Range(1, 8);
        }

        /// <summary>
        /// 問題を生成し、表示させるプログラム
        /// </summary>
        protected void SetMondai()
        {
            PanelVisible(true);
            answerText.text = "";
            resultText.text = "";
            Syutudai();
            if (mondaiList == null || mondaiList.Count == 0) mondaiList = Mondaiscript.GetMondaiList((MondaiGenre)config);
            MondaiData m = Mondaiscript.InputMondai((MondaiGenre)config,mondaiList);
            mondaiText.text = m.MondaiText;
            answer = m.Answer;
            kaisetsu = m.Kaisetsu;
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
                gogunText[numbers.Count-1].text = m.GetGogun(ransu);
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
            answerText.text = "答え："+answer;
            mondaiText.text = "解説：" + kaisetsu;
            if (gogunText[2-(int)p].text == answer)return true;
            return false;
         
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
        /// <summary>
        /// 現在のコイン枚数を表示
        /// </summary>
        public void CoinText()
        {
            coinText.text = rank.Coin.ToString();
        }
        /// <summary>
        /// 現在の回転数を表示
        /// </summary>
        protected void GameCounter()
        {
            rank.Count++;
            gameCounterText.text = rank.Count.ToString();
        }
        /// <summary>
        /// 現在の状態を表示
        /// </summary>

        protected IEnumerator Result(bool r)
        {
            if (r) resultText.text = "正解";
            else resultText.text = "残念";
            CurrentText();
            yield return new WaitForSeconds(1f);
            resultText.text = "";
        }

        IEnumerator DeleteEffect(int time)
        {
            yield return new WaitForSeconds(time);
            int index = effectArea.transform.childCount;
            for(int i = 0; i < index; i++)
            {
                Destroy(effectArea.transform.GetChild(i).gameObject);
            }
        }

        protected void DeleteEffect(GameObject parent)
        {
            int index = effectArea.transform.childCount;
            for (int i = 0; i < index; i++)
            {
                Destroy(parent.transform.GetChild(i).gameObject);
            }
        }

        protected void PanelVisible(bool tn)
        {
            mondaiPanel.SetActive(tn);
        }

        public void EsEffect()
        {
            GameObject pref = (GameObject)Instantiate(conprefDic[condition], effectArea.transform);
        }
        public void ReachIsOn()
        {
            switch (role)
            {
                case Role.REGBONUS:
                    reachgrace =1+ rank.Count;
                    break;
                case Role.BIGBONUS:
                    reachgrace = 1 + rank.Count;
                    break;
                case Role.FREEZE:
                    reachgrace = 1 + rank.Count;
                    break;
                default:
                    reachgrace = UnityEngine.Random.Range(1, 8) + rank.Count;
                    break;
            }
            reach = true;
        }
        private void SetRank()
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("username",username);
            dic.Add("coin",rank.Coin.ToString());
            dic.Add("ansnumber",rank.Ansnumber.ToString());
            dic.Add("count",rank.Count.ToString());
            WWWForm www = SQLConnect.CastWWWForm(dic);
            StartCoroutine(SQLConnect.Post("http://localhost/rank_input.php", www));
        }
        private IEnumerator GetRank() 
        {
            username = PlayerPrefs.GetString("username");
            if (username == "")SceneChanger.ChangeScene("Record");
            IEnumerator coroutine = SQLConnect.Get("http://localhost/rank_output.php" + "?username=" + username);
            yield return StartCoroutine(coroutine);
            if(coroutine.Current == null)
            {
                rank.Username = username;
                rank.Coin = 100;
                SetRank();
                yield break;
            }
            List<Rank> list = SQLConnect.RankJsonDeSer(coroutine.Current.ToString());
            foreach (Rank r in list) r.Dump();
            rank = list[0];
            CoinText();
            gameCounterText.text = rank.Count.ToString();
        }
    }
}

