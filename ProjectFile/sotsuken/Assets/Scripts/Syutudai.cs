using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Syutudai : MonoBehaviour
{
    private string subjectName;
    private string tangenName;
    private Mondai m;
    [SerializeField] Text mondaiText;
    [SerializeField] Text answerText;
    [SerializeField] Text kaisetsuText;
    [SerializeField] Text clearText;
    private GameObject mondai;
    [SerializeField] GameObject InputFieldArea;
    [SerializeField] GameObject ansButton;
    private InputField inputField;
    GameObject[] mondaiList;
    private int count;
     private void Start()
    {
        clearText.text = "";
        kaisetsuText.text = "";
        mondai = (GameObject)Resources.Load("Prefabs/" + subjectName + "/" + tangenName);
        mondai = Instantiate(mondai);

        mondaiList = new GameObject[mondai.transform.childCount];
        for (int i = 0; i < mondai.transform.childCount; i++)
        {
            mondaiList[i] = mondai.transform.GetChild(i).gameObject;
        }
        m = mondaiList[count].GetComponent<Mondai>();
    }

    void SetButton(Text[] text,string[] gogun)
    {
        //リスト初期化
        List<int> numbers = new List<int>();
        //ボタンの数だけ数値を用意({0,1,2,3})
        for(int i = 0; i < text.Length; i++)
        {
            //リストに入れる
            numbers.Add(i);
        }
        //カウント初期化
        int btncount = 0;
        //リストが１つ以上データがあるとき
        while (numbers.Count > 0)
        {
            //リストの長さだけ乱数生成
            int index = UnityEngine.Random.Range(0, numbers.Count);
            //「乱数で出た数値」番目を取り出す
            int ransu = numbers[index];
            //テキストを設置する
            text[index].text = gogun[ransu];
            btncount++;
            //入れた数値をリストから削除する
            numbers.RemoveAt(index);
        }
    }

    IEnumerator SetText(string text)
    {
        clearText.text = text;
        yield return new WaitForSeconds(1f);
        clearText.text = "";
    }

    public void Answer(string ans)
    {
       if(ans == m.GetAnswer())
        {
            Debug.Log("正解");
            StartCoroutine(SetText("せいかい！"));
        }
        else
        {
            Debug.Log("不正解");
            StartCoroutine(SetText("ぶぶー！"));
        }
    }
}
