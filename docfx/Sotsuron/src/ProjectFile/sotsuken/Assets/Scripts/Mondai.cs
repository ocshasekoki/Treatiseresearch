using UnityEngine;

public class Mondai : MonoBehaviour
{
    [SerializeField] private string mondaiText;
    [SerializeField] private string answer;
    [SerializeField] private string kaisetsu;
    [SerializeField] private string[] t;


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

