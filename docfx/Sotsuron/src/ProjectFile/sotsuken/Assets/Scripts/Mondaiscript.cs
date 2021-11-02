using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
namespace Mondai
{
    public class Mondaiscript : MonoBehaviour
    {
        private const string format = ".json";
        [SerializeField] protected InputField nameIF;
        [SerializeField] protected InputField mondaiIF;
        [SerializeField] protected InputField kaisetsuIF;
        [SerializeField] protected InputField answerIF;
        [SerializeField] protected InputField gogunIF_1;
        [SerializeField] protected InputField gogunIF_2;
        [SerializeField] protected InputField gogunIF_3;
        [SerializeField] protected Dropdown mondaiGenreDD;
        protected MondaiData data = new MondaiData();
        protected string[] gogun = null;

        private void Start()
        {

                string[] ops = Enum.GetNames(typeof(MondaiGenre));
                List<string> ddvalues = new List<string>();
                foreach (string typename in ops)
                {
                    ddvalues.Add(typename);
                }
                mondaiGenreDD.ClearOptions();
                mondaiGenreDD.AddOptions(ddvalues);
        }

        public void SetMondaiData()
        {
            data.MondaiText = mondaiIF.text;
            data.Kaisetsu = kaisetsuIF.text;
            data.Answer = answerIF.text;
            gogun = new string[3];
            gogun[0] = gogunIF_1.text;
            gogun[1] = gogunIF_2.text;
            gogun[2] = gogunIF_3.text;
            data.Gogun = gogun;
            data.Genre = (MondaiGenre)mondaiGenreDD.value;
            
            OutputMondai();
        }
        protected void OutputMondai()
        {
            string json = JsonUtility.ToJson(data);
            Debug.Log(json);
            string path = Application.streamingAssetsPath + "/Mondai/" + data.Genre.ToString() +"/"+ nameIF.text + format;
            File.WriteAllText(path, json);

        }
        public static MondaiData InputMondai(MondaiGenre genre)
        {
            List<string> list = GetMondaiList(genre);
            //リストの長さだけ乱数生成
            int index = UnityEngine.Random.Range(0, list.Count);
            //「乱数で出た数値」番目を取り出す
            string randomname = list[index];
            string path = Application.streamingAssetsPath + "/Mondai/" + genre.ToString() + "/" + randomname;
            string str = File.ReadAllText(path);
            return JsonUtility.FromJson<MondaiData>(str);
            
        }
        public static List<string> GetMondaiList(MondaiGenre genre)
        {
            List<string> mondaiList = new List<string>();

            DirectoryInfo dir = new DirectoryInfo(Application.streamingAssetsPath + "/Mondai/" + genre + "/");
            FileInfo[] info = dir.GetFiles("*.json");

            foreach(FileInfo f in info)
            {
                mondaiList.Add(f.Name);
            }
            return mondaiList;
        }

    public void ViewMondaiList()
    {
        foreach (MondaiGenre g in Enum.GetValues(typeof(MondaiGenre)))
        {
            Debug.Log(g.ToString());
            List<string> list = GetMondaiList(g);
            foreach (string s in list)
            {
                Debug.Log(s);
            }
        }
    }
    }
    public enum MondaiGenre
    {
        FE,AP,KODO
    }
    public class MondaiData
    {
        [SerializeField] private string mondaiText = null;
        [SerializeField] private string answer = null;
        [SerializeField] private string kaisetsu = null;
        [SerializeField] private string[] gogun = null;
        [SerializeField] private MondaiGenre genre;
        public string Answer
        {
            get { return answer; }
            set { answer = value; }
        }

        public string MondaiText
        {
            get { return mondaiText; }
            set { mondaiText = value; }
        }

        public string Kaisetsu
        {
            get { return kaisetsu; }
            set { kaisetsu = value; }
        }

        public string[] Gogun
        {
            get { return gogun; }
            set { gogun = value; }
        }

        public string GetGogun(int index)
        {
            return gogun[index];
        }

        public MondaiGenre Genre
        {
            get { return genre; }
            set { genre = value; }
        }
        public void Dunp()
        {
            Debug.Log("問題：" + mondaiText);
            Debug.Log("回答：" + answer);
            Debug.Log("解説：" + kaisetsu);
            foreach (string s in gogun)
            {
                Debug.Log("選択肢：" + s);
            }
        }
    }
}
