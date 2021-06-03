using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TateScroll : MonoBehaviour
{
    //スクロールスピード
    [SerializeField] float speed = 1;
    void Update()
    {
        //下方向にスクロール
        transform.localPosition += Vector3.up*speed;
        Debug.Log(transform.localPosition);
        Debug.Log(transform.position);
        //Yが-11まで来れば、21.33まで移動する
        if (transform.localPosition.y >= 250f)
        {
            transform.localPosition = new Vector2(0, -450f);
        }
    }
}