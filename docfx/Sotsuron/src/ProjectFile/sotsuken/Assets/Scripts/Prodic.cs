using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Data;
using EnumDic;

namespace Prob
{
    public class Prodic : MonoBehaviour
    {
        protected const string format = ".json";
        /// <summary>
        /// ボーナス確率を取ってくる関数
        /// </summary>
        /// <param name="dic">対応するディクショナリ</param>
        /// <param name="c">設定</param>
        /// <param name="r">役</param>
        /// <param name="cd">状態</param>
        /// <returns>確率</returns>
        public static ProData GetOc(Dic dic, Config c, Role r, Condition cd)
        {
            return dic.prodic.Find(x => x.conf == c && x.role == r && x.cond == cd);
        }
        /// <summary>
        /// 現在の設定とコンディションから一致した物を
        /// Keyを小役、ValueをProdata（確率）としてDictionaryとして抽出するスクリプト
        /// </summary>
        /// <param name="dic">全通り確率が格納されたリスト</param>
        /// <param name="c">現在の設定状態</param>
        /// <param name="cd">現在のコンディション状態</param>
        /// <returns>Keyを小役、ValueをProdata（確率）としてDictionary</returns>
        public static Dictionary<Role, ProData> GetPro(Dic dic, Config c, Condition cd)
        {
            Dictionary<Role, ProData> diction = new Dictionary<Role, ProData>();
            foreach (ProData p in dic.prodic)
            {
                if (p.cond == cd && p.conf == c)
                {
                    diction.Add(p.role, p);
                }
            }
            return diction;
        }

        /// <summary>
        /// 確率書き込み（ツール用）
        /// </summary>
        /// <param name="dic">書き込むデータ</param>
        /// <param name="name">ファイル名</param>
        public static void OutputDic(string name, Dic dic)
        {
            string json = JsonUtility.ToJson(dic);
            string path = Application.streamingAssetsPath + "/" + name + format;
            File.WriteAllText(path, json);
        }
        /// <summary>
        /// prodic(確率)Jsonファイルからロードしたデータを
        /// Dicクラスに変換する関数
        /// </summary>
        /// <returns>すべての確率のリスト</returns>
        public static Dic LoadDic()
        {
            string path = Application.streamingAssetsPath + "/prodic" + format;
            string str = File.ReadAllText(path);
            return JsonUtility.FromJson<Dic>(str);
        }
    }
}

