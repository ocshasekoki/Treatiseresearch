using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using Calen;
using System;
using System.Text.RegularExpressions;

public class RecordUser : MonoBehaviour
{
    private static string defurl = "http://localhost/";
    private static string userQues = "userques_";
    private static string userInfo = "userinfo_";
    private static string input = "input.php";
    private static string output = "output.php";
    private static string exist = "exist.php";
    private static int minlength = 8;

    private static string pattern = @"[^a-z @\.0-9]";
    [SerializeField] InputField useridIF;
    [SerializeField] InputField usernameIF;
    [SerializeField] InputField emailIF;
    [SerializeField] InputField passwordIF;

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

    public void UserInfoSet()
    {
        StartCoroutine(Post(defurl + userInfo + input,UserTest()));
    }
    public void UserInfoGet()
    {
        StartCoroutine(UserAccess(defurl + userInfo + output));
    }

    private IEnumerator UserAccess(string defurl)
    {
        IEnumerator coroutine = Get(defurl);
        yield return StartCoroutine(coroutine);
        Debug.Log(coroutine.Current.ToString());
        List<UserInfo> list = UserInfoJsonDeSer(coroutine.Current.ToString());
    }

    private static IEnumerator Post(string defurl, WWWForm form)
    {
        UnityWebRequest request = UnityWebRequest.Post(defurl, form);
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

    private List<UserInfo> UserInfoJsonDeSer(string str)
    {
        List<UserInfo> list = new List<UserInfo>();
        string[] sp = { "_jnl" };
        string[] strs = str.Split(sp, StringSplitOptions.None);
        foreach (string s in strs)
        {
            UserInfo info = JsonUtility.FromJson<UserInfo>(s);
            list.Add(info);
        }
        return list;
    }

    private WWWForm UserTest()
    {
        UserInfo info = new UserInfo();
        info.Userid = "hoge";
        info.Username = "moge";
        info.Email = "xxx.example.com";
        info.Password = "hogehoge";
        WWWForm form = new WWWForm();
        form.AddField("userid", info.Userid);
        form.AddField("username", info.Username);
        form.AddField("email", info.Email);
        form.AddField("password", info.Password);
        return form;
    }
    public void SetDic()
    {
        if (CheckSpell(useridIF.text) || CheckSpell(passwordIF.text) || CheckSpell (emailIF.text) || CheckSpell(usernameIF.text)) return;
        Dictionary<string, string> dic = new Dictionary<string, string>();
        dic.Add("userid", useridIF.text);
        dic.Add("username", usernameIF.text);
        dic.Add("email", emailIF.text);
        dic.Add("password", passwordIF.text);
        foreach (string s in dic.Keys) Debug.Log(s+":"+dic[s]);
        WWWForm form = SetUserInfo(dic);
        StartCoroutine(Post(defurl+userInfo+exist, form));
    }
    private WWWForm SetUserInfo(Dictionary<string,string> dic)
    {
        WWWForm form = new WWWForm();
        foreach (string s in dic.Keys) form.AddField(s, dic[s]);
        return form;
    }
}

