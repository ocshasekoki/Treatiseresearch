using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DataBase;
using System.Text.RegularExpressions;

public class RecordUser : MonoBehaviour
{
    private static string defurl = "http://localhost/";
    private static string userInfo = "userinfo_";
    private static string output = "output.php";
    private static string exist = "exist.php";
    private static int minlength = 8;

    private static string pattern = @"[^a-z ^A-Z @\.0-9]";
    [SerializeField] InputField useridIF = null;
    [SerializeField] InputField usernameIF = null;
    [SerializeField] InputField emailIF = null;
    [SerializeField] InputField passwordIF = null;
    [SerializeField] Text result = null;

    public void Check(InputField input)
    {
        bool b = CheckSpell(input.text);
        if (b) input.gameObject.transform.Find("Error").GetComponent<Text>().text = "文字列が不正です。";
        else input.gameObject.transform.Find("Error").GetComponent<Text>().text = "";
    }

    private bool CheckSpell(string str)
    {
        if (str.Length < minlength) return true;
        Match match = Regex.Match(str, pattern);
        if (match.Success)return true;
        else return false;
    }


    public void UserInfoGet()
    {
        StartCoroutine(UserAccess(defurl + userInfo + output));
    }

    private IEnumerator UserAccess(string defurl)
    {
        IEnumerator coroutine = SQLConnect.Get(defurl);
        yield return StartCoroutine(coroutine);
        Debug.Log(coroutine.Current.ToString());
        List<UserInfo> list = SQLConnect.UserInfoJsonDeSer(coroutine.Current.ToString());
    }
    public void SendUserProf()
    {
        StartCoroutine(SetDic());
    }
    public IEnumerator SetDic()
    {
        if (CheckSpell(useridIF.text) || CheckSpell(passwordIF.text) || CheckSpell (emailIF.text) || CheckSpell(usernameIF.text)) yield break;
        Dictionary<string, string> dic = new Dictionary<string, string>();
        dic.Add("userid", useridIF.text);
        dic.Add("username", usernameIF.text);
        dic.Add("email", emailIF.text);
        dic.Add("password", passwordIF.text);
        foreach (string s in dic.Keys) Debug.Log(s+":"+dic[s]);
        WWWForm form = SetUserInfo(dic);
        IEnumerator coroutine = SQLConnect.Post(defurl + userInfo + exist, form);
        yield return StartCoroutine(coroutine);
        result.text = coroutine.Current.ToString();
        if (coroutine.Current.ToString()=="新規登録しました。")
        {
            PlayerPrefs.SetString("username", usernameIF.text);
            SceneChanger.ChangeScene("Title");
        }
    }
    private WWWForm SetUserInfo(Dictionary<string,string> dic)
    {
        WWWForm form = new WWWForm();
        foreach (string s in dic.Keys) form.AddField(s, dic[s]);
        return form;
    }
}

