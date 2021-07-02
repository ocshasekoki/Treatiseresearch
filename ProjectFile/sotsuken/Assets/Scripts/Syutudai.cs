using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Syutudai : MonoBehaviour
{
    string subjectName;
    string tangenName;
    private Mondai m;
    [SerializeField]GameObject[] bottons;
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
        subjectName = PlayerPrefs.GetString("Subject");
        tangenName = PlayerPrefs.GetString("Tangen");
        mondai = (GameObject)Resources.Load("Prefabs/" + subjectName + "/" + tangenName);
        mondai = Instantiate(mondai);
        count = PlayerPrefs.GetInt(subjectName + tangenName);
        inputField = InputFieldArea.GetComponent<InputField>();
        InputFieldArea.SetActive(false);
        ansButton.SetActive(false);
        foreach (GameObject b in bottons)
        {
            b.SetActive(false);
            Debug.Log(b.name);
        }
        InputFieldArea.SetActive(false);
        mondaiList = new GameObject[mondai.transform.childCount];
        for (int i = 0; i < mondai.transform.childCount; i++)
        {
            mondaiList[i] = mondai.transform.GetChild(i).gameObject;
        }
        m = mondaiList[count].GetComponent<Mondai>();
        Set();
    }
    public void Set()
    {
        mondaiText.text = m.MondaiText;
        switch (m.genre)
        {
            case Genre.SELECT:
                SetButton();
                break;
            case Genre.NUMBER:
                SetInputField();
                inputField.contentType = InputField.ContentType.DecimalNumber;
                break;
            case Genre.STRING:
                SetInputField();
                inputField.contentType = InputField.ContentType.Standard;
                break;
        }

    }
    /**/
    public void BackQuestion()
    {
        answerText.text = "";
        if (count - 1 >= 0)
        {
            count--;
            m = mondaiList[count].GetComponent<Mondai>();
            Set();
        }
        else
        {
            Debug.Log("まえのもんだいはありません");
        }
    }
    public void nextQuestion()
    {
        answerText.text = "";
        if (count + 1 < mondaiList.Length){
            count++;
            PlayerPrefs.SetInt(subjectName + tangenName, count);
            m = mondaiList[count].GetComponent<Mondai>();
            Set();
        }
        else
        {
            Debug.Log("つぎのもんだいはありません");
            clearText.text = "くりあー！";
        }
    }
    public void SetKaisetsu()
    {
        kaisetsuText.text = m.kaisetsu;
    }
    /**
     * 
     *   ボタンで答える場合、
     *   ボタンをランダムに配置する 
     * 
     */
    void SetButton()
    {
        //ボタン可視化
        //ボタンの量だけ繰り返す
        foreach (GameObject b in bottons)
        {
            //ボタン可視化
            b.SetActive(true);
        }
        //文字列用のモノを消す
        ansButton.SetActive(false);
        InputFieldArea.SetActive(false);
        //リスト初期化
        List<int> numbers = new List<int>();
        //ボタンの数だけ数値を用意({0,1,2,3})
        for(int i = 0; i < m.b; i++)
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
            bottons[btncount].gameObject.transform.Find("Text").GetComponent<Text>().text = m.t[ransu];
            btncount++;
            //入れた数値をリストから削除する
            numbers.RemoveAt(index);
        }
    }
    /*
     
         入力形式で答える場合
         
         */

    void SetInputField()
    {
       　foreach(GameObject b in bottons)
         {
            b.SetActive(false);
         }
         ansButton.SetActive(true);
         InputFieldArea.SetActive (true);
    }
    IEnumerator SetText(string text)
    {
        clearText.text = text;
        yield return new WaitForSeconds(1f);
        clearText.text = "";
    }
    /*
     
        ボタンの場合の答え合わせ 

    */
    public void Answer(string ans)
    {
       if(ans == m.answer)
        {
            Debug.Log("正解");
            StartCoroutine(SetText("せいかい！"));
            nextQuestion();
        }
        else
        {
            Debug.Log("不正解");
            if (m.genre == Genre.SELECT)
            {
                SetButton();
            }
            StartCoroutine(SetText("ぶぶー！"));
        }
    }
    /*
     
         入力での答え合わせ
         
         */
    public void AnsButton()
    {
        Answer(inputField.text);
    }
}
