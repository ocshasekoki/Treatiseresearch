using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using EnumDic;
using Prob;
using Data;

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
        /// <summary>condition:状態（低確、高確、超高確）</summary>
        protected Condition condition = Condition.NOMAL;
        /// <summary>config:設定（LOW,MIDDLE,HIGH）</summary>
        protected Config config = 0;
        /// <summary> dic :すべての確率のリスト</summary>
        protected Dic dic;
        /// <summary>data:役に対応する図柄のディクショナリなどのデータが格納された関数</summary>
        protected DicData data;
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
        /// <summary>configDD: 設定の変更のドロップダウン</summary>
        [SerializeField] protected Dropdown configDD;
        Dictionary<Role, GameObject> prefDic = new Dictionary<Role, GameObject>();

        public void Start()
        {
            prefDic.Clear();
            foreach (Role r in Enum.GetValues(typeof(Role)))
            {
                prefDic.Add(r, PrefLoad(r));
            }
            Debug.Log(prefDic.Count);
            foreach (GameObject g in prefDic.Values)
            {
                Debug.Log(g.name);
            }

            config = (Config)UnityEngine.Random.Range(0, 2);
            condition = Condition.NOMAL;
            dic = Prob.Prodic.LoadDic();
            ChangeMode(dic);

            leftsymbol = SetReal(leftReal);
            centersymbol = SetReal(centerReal);
            rightsymbol = SetReal(rightReal);

            SetConfigDD();
        }
        /// <summary>
        /// 指定したリールの絵柄すべてをリストに格納する。
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
        /// ればーおん！
        /// </summary>
        public void LeverOn()
        {

            RandomRole();
            RealRotate();
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
        /// ボーナス判定
        /// </summary>
        /// <param name="percent">ボーナス確率(%)</param>
        /// <returns>ボーナスの判定</returns>
        public static bool BonusJudge(int percent)
        {
            int rand = UnityEngine.Random.Range(1, 1000);
            if (rand <= percent) return true;
            else return false;
        }

        /// <summary>
        /// 小役QUESTION(問題)で問題を答えた時に動かすメソッド
        /// </summary>
        /// <param name="currect">正解かどうか</param>
        /// <returns>連続正解した数＊設定、状態に対応した確率</returns>
        public void Answer(bool currect)
        {
            if (currect) data.Cor++;
            else data.Cor = 0;
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
        /// <param name="pos">リールの場所</param>
        public void btnPush(string pos)
        {
            Position p = (Position)Enum.Parse(typeof(Position), pos);
            switch (p)
            {
                case Position.LEFT:
                    AssistDicision(leftsymbol, (Symbol)DicData.symbolDic[role].l);
                    break;
                case Position.MIDDLE:
                    AssistDicision(centersymbol, (Symbol)DicData.symbolDic[role].c);
                    break;
                case Position.RIGHT:
                    AssistDicision(rightsymbol, (Symbol)DicData.symbolDic[role].r);
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
        }
        /// <summary>
        /// ボタンを押したとき全ての図柄を止める。
        /// </summary>
        /// <param name="p">リールの位置</param>
        protected void AllRealStop(Position p)
        {
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
        }

        /// <summary>
        /// 演出に合わせた色を出力する。
        /// </summary>
        /// <param name="r">小役</param>
        /// <param name="obj">演出のオブジェクト</param>
        [Obsolete]
        protected void SetColor(Role r, GameObject obj)
        {
            Debug.Log(r.ToString());
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
    }
}

