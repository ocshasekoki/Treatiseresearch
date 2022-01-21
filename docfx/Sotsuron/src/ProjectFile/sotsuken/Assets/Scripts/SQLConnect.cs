using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class SQLConnect : MonoBehaviour
{
    public string ServerGetAddress = "http://localhost/index.php";  //selecttest.phpを指定　今回のアドレスはlocalhost
    public string ServerPostAddress = "http://localhost/index3.php";  //selecttest.phpを指定　今回のアドレスはlocalhost

    private void Start()
    {
        StartCoroutine(Receive()); 
    }
    public void SQLReceive()
    {
        Receive();
    }

    public void SQLResponse()
    {
        Access();
    }

    private IEnumerator Access()
    {
        StartCoroutine(Get(ServerGetAddress));
        yield return 0;
    }
    private IEnumerator Receive()
    {
        Rank rank = new Rank();
        rank.UserID = "moge";
        rank.Coin = 200;
        rank.Ansnumber = 15;
        rank.Count = 30;
        StartCoroutine(Post(ServerPostAddress,rank));  // POST
        yield return 0;
    }
    private IEnumerator Post(string url,Rank rank)
    {
        WWWForm form = new WWWForm();
        form.AddField("userID", rank.UserID);
        form.AddField("coin", rank.Coin);
        form.AddField("ansnumber", rank.Ansnumber);
        form.AddField("count", rank.Count);
        UnityWebRequest request = UnityWebRequest.Post(url,form);
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
        }
    }
    private IEnumerator Get(string url)
    {
        UnityWebRequest request = UnityWebRequest.Get(url);
        yield return request.SendWebRequest();
        //3.isNetworkErrorとisHttpErrorでエラー判定
        if (request.isHttpError || request.isNetworkError)
        {
            //4.エラー確認
            Debug.Log(request.error);
        }
        else
        {
            //4.結果確認
            string[] sp = { "rn" };
            Debug.Log(request.downloadHandler.text);
            string[] strs = request.downloadHandler.text.Split(sp,System.StringSplitOptions.None);
            foreach (string s in strs)
            {
                Rank rank = JsonUtility.FromJson<Rank>(s);
                rank.Dump();
            }
            
        }
    }
}
public class Rank
{
    [SerializeField]
    private string userID = null;
    public string UserID { 
        get { return userID; }
        set { userID = value; }
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
    private int count = 0;
    public int Count { 
        get { return count; }
        set { count= value; }
    }

    public void Dump()
    {
        Debug.Log("ユーザーID："+UserID);
        Debug.Log("所持コイン："+Coin);
        Debug.Log("正解数："+Ansnumber);
        Debug.Log("回答数："+Count);
    }
}
