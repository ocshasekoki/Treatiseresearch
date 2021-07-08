using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotMachine_hase : MonoBehaviour
{
    private int symbolLeft = 0;
    private int symbolCenter = 0;
    private int symbolRight = 0;
    private Dictionary<Role, ProData> diction; 
    private Condition condition = Condition.NOMAL;
    private Config config = 0;
    private Dic dic;
    private Data data;
    private int pro = 0;
    private Role role;

    private List<GameObject> leftsymbol = null;
    private List<GameObject> centersymbol = null;
    private List<GameObject> rightsymbol = null;

    [SerializeField] private GameObject leftReal = null;
    [SerializeField] private GameObject centerReal = null;
    [SerializeField] private GameObject rightReal = null;
    [SerializeField] private List<GameObject> effectList = null;
    [SerializeField] private GameObject effectArea = null;
    [SerializeField] private GameObject colorTest = null;

    [SerializeField] private Dropdown configDD;
    Dictionary<Role, GameObject> prefDic = new Dictionary<Role, GameObject>();

    
    public void Start()
    {
        prefDic.Clear();
        foreach (Role r in Enum.GetValues(typeof(Role)))
        {
            prefDic.Add(r, PrefLoad(r));
        }
        Debug.Log(prefDic.Count);
        foreach(GameObject g in prefDic.Values)
        {
            Debug.Log(g.name);
        }

        config =(Config)UnityEngine.Random.Range(0,2);
        condition = Condition.NOMAL;
        dic = Prodic.LoadDic();
        ChangeMode(dic);
        //Test.TestProdic(dic);

        leftsymbol = SetReal(leftReal);
        centersymbol = SetReal(centerReal);
        rightsymbol = SetReal(rightReal);

        SetConfigDD();
    }
    private List<GameObject> SetReal(GameObject obj)
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

    private void RealRotate()
    {
        foreach(GameObject g in leftsymbol)
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
    private void RandomRole()
    {
        int rand = UnityEngine.Random.Range(1,pro);
        role = DecideRole(rand);
        SetColor(role,colorTest);
        CreatePrefab(role);
        DecideSymbol(role);
    }

    private void CreatePrefab(Role r)
    {
        Debug.Log(r.ToString());
        Instantiate(prefDic[r],effectArea.transform);
    }

    /// <summary>
    /// 役からそろう柄を決定するメソッド
    /// <param name="rand">生成された乱数</param>
    /// </summary>
    private void DecideSymbol(Role r)
    {
        Debug.Log("小役:" + r + " ID:" + diction[r]);
        Debug.Log(Data.symbolDic[r]);
        symbolLeft = Data.symbolDic[r].l;
        symbolCenter = Data.symbolDic[r].c;
        symbolRight = Data.symbolDic[r].r;
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
        foreach(Role r in diction.Keys)
        {
            sum += diction[r].appearpro;
            if(random <= sum)
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

    private void ChangeMode(Dic dic)
    {
        pro = 0;
        diction = Prodic.GetPro(dic, config, condition);
        foreach(Role r in diction.Keys)
        {
            pro += diction[r].appearpro;
        }
    }

    public void btnPush(string pos)
    {
        Position p = (Position)Enum.Parse(typeof(Position),pos);
        switch (p)
        {
            case Position.LEFT:
                AssistDicision(leftsymbol, (Symbol)Data.symbolDic[role].l);
                break;
            case Position.MIDDLE:
                AssistDicision(centersymbol, (Symbol)Data.symbolDic[role].c);
                break;
            case Position.RIGHT:
                AssistDicision(rightsymbol, (Symbol)Data.symbolDic[role].r);
                break;
        }
    }

    private void AssistDicision(List<GameObject> list,Symbol s)
    {
        if (s == 0)
        {
            s = (Symbol)UnityEngine.Random.Range(2,7);
        }
        foreach (GameObject obj in list)
        {
            Symbol sm = obj.GetComponent<SymbolData>().GetSymbol();
            if (obj.transform.localPosition.y >= 0f&& sm == s)
            {
                StartCoroutine(Assist(obj));
            }
        }
    }

    private IEnumerator Assist(GameObject obj)
    {
        yield return new WaitWhile(() =>obj.transform.localPosition.y >= 10f);
        obj.GetComponent<SymbolScript>().RealStop();
        AllRealStop(obj.GetComponent<SymbolData>().GetPos());
    }

    private void AllRealStop(Position p)
    {
        switch (p)
        {
            case Position.LEFT:
                foreach(GameObject obj in leftsymbol)
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

    private void EffectVisible(GameObject effect)
    {
        Instantiate(effect,effectArea.transform);
    }

    [Obsolete]
    private void SetColor(Role r,GameObject obj)
    {
        Debug.Log(r.ToString());
        Debug.Log(Data.rolecolor[r]);
        obj.GetComponent<ParticleSystem>().startColor = Data.rolecolor[r];
    }

    public void SetConfig()
    {
        config =(Config) configDD.value;
        ChangeMode(dic);
    }
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
    private GameObject PrefLoad(Role r)
    {
        GameObject obj = null;
        Debug.Log(r);
        //r.ToString()  (Role名)_prefのプレハブ読み込み
        //obj = (GameObject)Resources.Load("/Prefabs/" + r + "_pref");
        obj = Resources.Load<GameObject>("Prefabs/"+ r+"_pref");
        
        Debug.Log(obj.name);
        //CHERRY_pref
        return obj;
    }
}