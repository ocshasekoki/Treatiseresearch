using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TateScroll : MonoBehaviour
{
    //スクロールスピード
    [SerializeField] float speed = 1;
    void FixedUpdate()
    {
        //下方向にスクロール  Vector3.up = new Vector3(0f,1f,0f); Vector3.front = new Vector3(0f,0f,1f);
        transform.localPosition -= Vector3.up * speed;

        if (transform.localPosition.y <= -450f)
        {
            transform.localPosition = new Vector2(0, 150f);
        }
    }
}