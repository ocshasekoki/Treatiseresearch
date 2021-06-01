using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot_Noguchi : MonoBehaviour
{
    public GameObject[] imgobj; //絵柄のプレハブを格納(計7種）
    GameObject[] tmp_obj = new GameObject[90];//リールの配列（プレハブの種類が入る）
    Transform[] img_pos = new Transform[90];//リールの柄の位置
    Transform pos;  //リールのTransform

    int div0 = 7;//赤＄＄＄が出るしきい値
    int div1 = 15;//＄＄＄が出るしきい値
    int div2 = 20;//BARが出るしきい値
    int div3 = 40;//bが出るしきい値
    int div4 = 55;//0が出るしきい値
    int div5 = 80;//wが出るしきい値
    //%が出るしきい値
    void Awake()//ゲーム開始時に
    {
        GenerateReel();//リール生成関数を呼び出す
    }
    public void GenerateReel()//リール生成関数本体
    {
        pos = GetComponent<Transform>();//リールのTransformはオブジェクト位置とする

        for (int i = 0; i < 90; i++)
        {
            Vector3 pos = new Vector3(0.0f, -1.5f + (1.5f * i), 0.0f);//プレハブ位置の決定
            int tmp;
            int rand = Random.Range(0, 91);//0~91の数字をランダムで生成
            if (0 < rand && rand < div0 //もし数字が0～赤＄＄＄のしきい値以内だったら
                )
            {
                tmp = 0;//赤＄＄＄の絵柄idをtempに代入
            }
            else if (div0 < rand && rand < div1 // もし数字が赤＄＄＄のしきい値～＄＄＄のしきい値以内だったら               
                )
            {
                tmp = 1;//＄＄＄の絵柄idをtempに代入
            }
            else if (div1 < rand && rand < div2 //以下他の絵柄について同様に
               )
            {
                tmp = 2;
            }
            else if (div2 < rand && rand < div3
               )
            {
                tmp = 3;
            }
            else if (div3 < rand && rand < div4
               )
            {
                tmp = 4;
            }
            else if (div4 < rand && rand < div5
               )
            {
                tmp = 5;
            }
            else
            {
                tmp = 6;
            }
            tmp_obj[i] = (GameObject)Instantiate(imgobj[tmp]); //プレハブからtempの絵柄idのGameObjectを生成
            tmp_obj[i].transform.SetParent(transform, false); //リールのオブジェクトを親にする
            img_pos[i] = tmp_obj[i].GetComponent<Transform>();//プレハブのtransformを取得
            img_pos[i].localPosition = pos;//プレハブ位置の代入
        }
    }
    public void DestroyReel()
    {　//リールをデストロイしてリセットする関数
        GameObject[] reels = GameObject.FindGameObjectsWithTag("reel"); //reelタグが付いているオブジェクトをすべて取得
        foreach (GameObject i in reels)//各reelsのオブジェクトに対して以下を行う
        {
            Destroy(i);//reelsの各オブジェクトを破壊する
        }
    }
}
//一応これが参考にして作成した、確率のやつ