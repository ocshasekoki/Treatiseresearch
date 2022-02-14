using DataBase;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class Login : MonoBehaviour
{
    [SerializeField] InputField userid = null;
    [SerializeField] InputField username = null;
    private static string defurl = "http://localhost/";
    private static string userInfo = "userinfo_";
    private static string login = "login.php";
    private static int minlength = 8;
    private static string pattern = @"[^a-z ^A-Z @\.0-9]";
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
        if (match.Success) return true;
        else return false;
    }
    public void UserInfoGet()
    {
        StartCoroutine(UserLogin(defurl + userInfo + login));
    }

    private IEnumerator UserLogin(string defurl)
    {
        WWWForm form = new WWWForm();
        form.AddField("userid",userid.text);
        form.AddField("password",username.text);
        IEnumerator coroutine = SQLConnect.Post(defurl,form);
        yield return StartCoroutine(coroutine);
        List<UserInfo> list = SQLConnect.UserInfoJsonDeSer(coroutine.Current.ToString());
        PlayerPrefs.SetString("username", list[0].Username);
    }
}
