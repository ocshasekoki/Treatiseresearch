using UnityEngine;

public class Mondai : MonoBehaviour
{
    [SerializeField] private string mondaiText = null;
    [SerializeField] private string answer = null;
    [SerializeField] private string kaisetsu = null;
    [SerializeField] private string[] t = null;


    public string GetAnswer()
    {
        return answer;
    }

    public string GetMondaiText()
    {
        return mondaiText;
    }

    public string GetKaisetsu()
    {
        return kaisetsu;
    }

    public string[] GetT()
    {
        return t;
    }

    public string GetT(int index)
    {
        return t[index];
    }

    public void Dunp()
    {
        Debug.Log("問題："+mondaiText);
        Debug.Log("回答：" + answer);
        Debug.Log("解説：" + kaisetsu);
        foreach(string s in t)
        {
            Debug.Log("選択肢：" + s);
        }
    }
}

