using UnityEngine;

public class Mondai : MonoBehaviour
{
    [SerializeField] private string mondaiText;
    [SerializeField] private string answer;
    [SerializeField] private string kaisetsu;
    [SerializeField] private int b;
    [SerializeField] private string[] t;
    [SerializeField] private bool clear;

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

    public bool GetClear()
    {
        return clear;
    }

    public void SetClear(bool b)
    {
        clear = b;
    }
}

