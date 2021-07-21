using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Syutudai : MonoBehaviour
{
    [SerializeField]private Mondai m;

    [SerializeField] Text mondaiText;
    [SerializeField] Text answerText;
    [SerializeField] Text kaisetsuText;
    [SerializeField] Text[] gogunText;
    [SerializeField] GameObject ansButton;

    public void SetButton()
    {
        //リスト初期化
        List<int> numbers = new List<int>();
        //ボタンの数だけ数値を用意({0,1,2,3})
        for(int i = 0; i < gogunText.Length; i++)
        {
            //リストに入れる
            numbers.Add(i);
        }
        //リストが１つ以上データがあるとき
        while (numbers.Count > 0)
        {
            //リストの長さだけ乱数生成
            int index = UnityEngine.Random.Range(0, numbers.Count);
            //「乱数で出た数値」番目を取り出す
            int ransu = numbers[index];
            //テキストを設置する
            gogunText[index].text = m.GetT(ransu);
            //入れた数値をリストから削除する
            numbers.RemoveAt(index);
        }
    }
}