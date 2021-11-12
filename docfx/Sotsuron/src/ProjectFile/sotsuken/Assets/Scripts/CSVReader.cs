using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Data;
using Prob;
using EnumDic;

public class CSVReader : MonoBehaviour
{
    [SerializeField] string fileName;
    TextAsset csvFile; // CSVファイル
    List<string[]> csvDatas = new List<string[]>(); // CSVの中身を入れるリスト;

    void Start()
    {
        csvFile = Resources.Load("CSV/"+fileName) as TextAsset; // Resouces下のCSV読み込み
        StringReader reader = new StringReader(csvFile.text);

        while (reader.Peek() != -1) // reader.Peaekが-1になるまで
        {
            string line = reader.ReadLine(); // 一行ずつ読み込み
            csvDatas.Add(line.Split(',')); // , 区切りでリストに追加
        }
        int config = 0;
        int condition = 0;
        List<ProData> list = new List<ProData>();
        foreach (string[] s in csvDatas)
        {
            switch (s[0])
            {
                case "Config":
                    list = new List<ProData>();
                    config = int.Parse(s[1]);
                    break;
                case "Condition":
                    condition = int.Parse(s[1]);
                    break;
                case "fin":
                    Dic dic = new Dic();
                    dic.prodic = list;
                    Prodic.OutputDic("test",dic);
                    break;
                default:
                    list.Add(ReadProdata(config, condition, s));
                    break;
            }
        }
    }
    protected ProData ReadProdata(int conf,int cond,string[] strs)
    {
        ProData data = new ProData();
        data.conf = (Config)conf;
        data.cond = (Condition)cond;
        data.role = (Role)int.Parse(strs[0]);
        data.appearpro = int.Parse(strs[1]);
        data.bonuspro = int.Parse(strs[2]);
        data.bigbonuspro = int.Parse(strs[3]);
        data.freezepro = int.Parse(strs[4]);
        data.chancezonepro = int.Parse(strs[5]);
        data.Dump();
        return data;
    }
}