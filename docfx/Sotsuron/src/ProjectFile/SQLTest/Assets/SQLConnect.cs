using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using Calen;
using System;

public class SQLConnect : MonoBehaviour
{
    private static string defurl = "http://localhost/";
    private static string rank = "rank_";
    private static string userQues = "userques_";
    private static string userInfo = "userinfo_";
    private static string quesInfo = "questioninfo_";
    private static string input = "input.php";
    private static string output = "output.php";
    private string username;
    [SerializeField]private GameObject calenderObj;
    [SerializeField]private GameObject fromObj;
    [SerializeField]private GameObject toObj;
    [SerializeField]private Dropdown duringDD;
    [SerializeField]private Dropdown numberDD;
    [SerializeField]private Dropdown sortKeyDD;
    [SerializeField]private Toggle onlyMine;
    [SerializeField]private Toggle desc;
    [SerializeField]private Toggle disc;
    [SerializeField]private Text resSortKey;
    [SerializeField]private GameObject pref;
    [SerializeField]private GameObject parent;
    private Calendar calendar;
    private void Start()
    {
        calendar = calenderObj.GetComponent<Calendar>();
        username = "hoge";
    }

    public void RankSet()
    {
        StartCoroutine(Post(defurl+rank+input,RankTest()));
    }
    public void RankGet()
    {
        DestroyChildAll(parent);
        string key ="?";
        Dictionary<string, string> dic = new Dictionary<string, string>();
        if(onlyMine.isOn) dic.Add("username",username);
        if(desc.isOn) dic.Add("sort","1");
        else dic.Add("sort","0"); 
        if (disc.isOn) dic.Add("disc", "1");
        else dic.Add("disc", "0");
        dic.Add("fromdate", calendar.SetDate());
        SetSearchText(fromObj, calendar.SetDate());
        dic.Add("todate", GetDuring());
        SetSearchText(toObj,GetDuring());
        dic.Add("sortkey",GetSortKey(sortKeyDD.value,false));
        resSortKey.text = GetSortKey(sortKeyDD.value,true);
        foreach (string s in dic.Keys)
        {
            key += s + "=" + dic[s];
            if (s != "sortkey") key += "&";
        }
        StartCoroutine(RankAccess(defurl + rank + output+key));
    }

    private void DestroyChildAll(GameObject obj)
    {
        for(int i = 0; i < obj.transform.childCount; i++)
        {
            Destroy(obj.transform.GetChild(i).gameObject);
        }
    }
    private string GetDuring()
    {
        DateTime defdate =DateTime.Parse(calendar.SetDate());
        DateTime afdate = defdate;
        switch (duringDD.value)
        {
            case 0:
                afdate = defdate.AddDays(-(numberDD.value+1));
                break;
            case 1:
                afdate = defdate.AddDays(-(numberDD.value + 1) *7);
                break;
            case 2:
                afdate = defdate.AddMonths(-(numberDD.value + 1));
                break;
            case 3:
                afdate = defdate.AddYears(-(numberDD.value + 1));
                break;
        }
        string[] strs = afdate.ToString().Split('/');
        string key = "";
        for(int i = 0;i<3;i++)
        {
            if (i != 2)
            {
                int num = int.Parse(strs[i]);
                key += num.ToString() + "-";
            }
            else
            {
               int num = int.Parse(strs[i].Split(' ')[0]);
                key += num.ToString();
            }
        }
        return key;
    }

    private string GetSortKey(int value,bool jpn)
    {
        switch (value)
        {
            case 0:
                if (jpn) return "コイン枚数";
                return "coin";
            case 1:
                if (jpn) return "正解率";
                return "percent";
            case 2:
                if (jpn) return "正解数";
                return "ansnumber";
        }
        return "";
    }

    public void UserQuesSet()
    {
        StartCoroutine(Post(defurl+userQues+input,RankTest()));
    }
    public void UserQuesGet()
    {
        StartCoroutine(RankAccess(defurl + userQues + output));
    }    
    public void UserInfoSet()
    {
        StartCoroutine(Post(defurl+userInfo+input,UserTest()));
    }
    public void UserInfoGet()
    {
        StartCoroutine(UserAccess(defurl+userInfo+output));
    }    
    public void QuesInfoSet()
    {
        StartCoroutine(Post(defurl+quesInfo+input,QuestionTest()));
    }
    public void QuesInfoGet()
    {
        StartCoroutine(QuestionAccess(defurl+quesInfo+output));
    }
    private void SetSearchText(GameObject obj,string date)
    {
        string[] strs = date.Split('-');
        obj.transform.Find("year").GetComponent<Text>().text = strs[0];
        obj.transform.Find("month").GetComponent<Text>().text = strs[1];
        obj.transform.Find("day").GetComponent<Text>().text = strs[2];
    }
    private IEnumerator RankAccess(string defurl)
    {
        IEnumerator coroutine = Get(defurl);
        yield return StartCoroutine(coroutine);
        Debug.Log(coroutine.Current.ToString());
        List<Rank> list = RankJsonDeSer(coroutine.Current.ToString());

        int index = 1;
        if (!desc.isOn) index = list.Count;

        foreach(Rank r in list)
        {
            if (disc.isOn) CreateRankPanel(r, index);
            else 
            { 
                if (parent.transform.Find(r.Username) == null) CreateRankPanel(r, index);
                else 
                { 
                    if (!desc.isOn)
                    {
                        ChangeRankPanel(r, parent.transform.Find(r.Username).gameObject, index);
                    }              
                }
            }
            if (desc.isOn) index++;
            else index--;
        }

    }
    public void CreateRankPanel(Rank r,int index)
    {
        r.Dump();
        GameObject obj = Instantiate(pref, parent.transform);
        obj.name = r.Username;
        obj.transform.Find("username").GetComponent<Text>().text = r.Username;
        obj.transform.Find("anstime").GetComponent<Text>().text = r.Anstime;
        obj.transform.Find("rank").GetComponent<Text>().text = index.ToString();
        obj.transform.Find("coin").GetComponent<Text>().text = r.Coin.ToString();
        obj.transform.Find("ansnumber").GetComponent<Text>().text = r.Ansnumber.ToString();
        obj.transform.Find("count").GetComponent<Text>().text = r.Count.ToString();
    }
    public void ChangeRankPanel(Rank r,GameObject obj,int rank)
    {
        obj.transform.SetAsLastSibling();
        obj.transform.Find("anstime").GetComponent<Text>().text = r.Anstime;
        obj.transform.Find("rank").GetComponent<Text>().text = rank.ToString();
        obj.transform.Find("coin").GetComponent<Text>().text = r.Coin.ToString();
        obj.transform.Find("ansnumber").GetComponent<Text>().text = r.Ansnumber.ToString();
        obj.transform.Find("count").GetComponent<Text>().text = r.Count.ToString();
    }

    private IEnumerator UserAccess(string defurl)
    {
        IEnumerator coroutine = Get(defurl);
        yield return StartCoroutine(coroutine);
        Debug.Log(coroutine.Current.ToString());
        List<UserInfo> list = UserInfoJsonDeSer(coroutine.Current.ToString());
    }
    private IEnumerator QuestionAccess(string defurl)
    {
        IEnumerator coroutine = Get(defurl);
        yield return StartCoroutine(coroutine);
        Debug.Log(coroutine.Current.ToString());
        List<QuestionInfo> list = QuesInfoJsonDeSer(coroutine.Current.ToString());
    }

    private static IEnumerator Post(string defurl,WWWForm form)
    {
        UnityWebRequest request = UnityWebRequest.Post(defurl,form);
        yield return request.SendWebRequest();
        //3.isNetworkErrorとisHttpErrorでエラー判定
        if (request.isHttpError || request.isNetworkError)
        {
            //4.エラー確認
            Debug.Log(request.error);
        }
        else
        {
            Debug.Log("送信完了");
            Debug.Log(request.downloadHandler.text);
            yield return request.downloadHandler.text;
        }
    }
    private static IEnumerator Get(string defurl)
    {
        UnityWebRequest request = UnityWebRequest.Get(defurl);
        yield return request.SendWebRequest();
        //3.isNetworkErrorとisHttpErrorでエラー判定
        if (request.isHttpError || request.isNetworkError)
        {
            //4.エラー確認
            yield return request.error;
        }
        else
        {
            yield return request.downloadHandler.text;
        }
    }

    private static List<Rank> RankJsonDeSer(string str)
    {
        List<Rank> list = new List<Rank>();
        string[] sp = { "_jnl" };
        string[] strs = str.Split(sp,StringSplitOptions.None);
        foreach (string s in strs)
        {
            Rank rank = JsonUtility.FromJson<Rank>(s);
            list.Add(rank);
        }
        return list;
    }
    private List<UserInfo> UserInfoJsonDeSer(string str)
    {
        List<UserInfo> list = new List<UserInfo>();
        string[] sp = { "_jnl" };
        string[] strs = str.Split(sp,StringSplitOptions.None);
        foreach (string s in strs)
        {
            UserInfo info = JsonUtility.FromJson<UserInfo>(s);
            list.Add(info);
        }
        return list;
    }
    private static List<QuestionInfo> QuesInfoJsonDeSer(string str)
    {
        List<QuestionInfo> list = new List<QuestionInfo>();
        string[] sp = { "_jnl" };
        string[] strs = str.Split(sp, StringSplitOptions.None);
        foreach (string s in strs)
        {
            QuestionInfo info = JsonUtility.FromJson<QuestionInfo>(s);
            list.Add(info);
        }
        return list;
    }

    private WWWForm RankTest()
    {
        Rank rank = new Rank();
        rank.Username = "hoge";
        rank.Coin = 250;
        rank.Ansnumber = 15;
        rank.Count = 30;
        WWWForm form = new WWWForm();
        form.AddField("username", rank.Username);
        form.AddField("coin", rank.Coin);
        form.AddField("ansnumber", rank.Ansnumber);
        form.AddField("count", rank.Count);
        return form;
    }
    private WWWForm QuestionTest()
    {
        QuestionInfo info = new QuestionInfo();
        info.QuestionID = "hoge";
        info.Count = 250;
        info.Ansnumber = 15;
        WWWForm form = new WWWForm();
        form.AddField("questionID", info.QuestionID);
        form.AddField("ansnumber", info.Ansnumber);
        form.AddField("count", info.Count);
        return form;
    }
    private WWWForm UserTest()
    {
        UserInfo info = new UserInfo();
        info.Userid = "hoge";
        info.Username = "moge";
        info.Email = "xxx.example.com";
        info.Password = "hogehoge";
        WWWForm form = new WWWForm();
        form.AddField("username", info.Userid);
        form.AddField("username", info.Username);
        form.AddField("email", info.Email);
        form.AddField("password", info.Password);
        return form;
    }
}
public class Rank
{
    [SerializeField]
    private string username = null;
    public string Username { 
        get { return username; }
        set { username = value; }
    }

    [SerializeField]
    private int coin = 0;
    public int Coin {
        get { return coin; }
        set { coin = value; }
    }

    [SerializeField]
    private int ansnumber = 0;
    public int Ansnumber { 
        get { return ansnumber; }
        set { ansnumber = value; }
    }
    [SerializeField]
    private string anstime = "";
    public string Anstime
    {
        get { return anstime; }
        set { anstime = value; }
    }

    [SerializeField]
    private int count = 0;
    public int Count { 
        get { return count; }
        set { count= value; }
    }

    public void Dump()
    {
        Debug.Log("ユーザーID："+username);
        Debug.Log("所持コイン："+Coin);
        Debug.Log("正解数："+Ansnumber);
        Debug.Log("回答数："+Count);
        Debug.Log("日付："+anstime);
    }
}
public class UserInfo
{
    [SerializeField]
    private string userid = null;
    public string Userid
    {
        get { return userid; }
        set { userid = value; }
    }

    [SerializeField]
    private string username = null;
    public string Username
    {
        get { return username; }
        set { username = value; }
    }

    [SerializeField]
    private string email = null;
    public string Email
    {
        get { return email; }
        set { email = value; }
    }

    [SerializeField]
    private string password = null;
    public string Password
    {
        get { return password; }
        set { password = value; }
    }
}

public class QuestionInfo
{
    [SerializeField]
    private string questionID = null;
    public string QuestionID
    {
        get { return questionID; }
        set { questionID = value; }
    }

    [SerializeField]
    private int count = 0;
    public int Count
    {
        get { return count; }
        set { count = value; }
    }

    [SerializeField]
    private int ansnumber = 0;
    public int Ansnumber
    {
        get { return ansnumber; }
        set { ansnumber = value; }
    }
}

