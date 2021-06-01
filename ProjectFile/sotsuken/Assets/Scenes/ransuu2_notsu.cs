using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ransuu2_notsu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
class RandomNum
{
    public int[] randNum(int digits)
    {
        int[] numArry = new int[digits];
        Random r = new Random(int)DataTime.Now.Ticks);
        for (int I =0; i < digits; I++)
        {
            //0以上10未満のランダム整数を返す
            numArry[i] = r.Next(10);

        }

        //配列ごと返却
        return numArry;
    }
}