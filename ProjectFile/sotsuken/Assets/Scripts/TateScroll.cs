using UnityEngine;

public class TateScroll : MonoBehaviour
{
    //スクロールスピード
    [SerializeField] private float speed = 1;
    [SerializeField] private float upperlimit = 250f;
    [SerializeField] private float lowerlimit = -450f;
    [SerializeField] private float assist = -150f;
    [SerializeField] public  Symbol symbol = Symbol.BAR;

    void FixedUpdate()
    {
        //下方向にスクロール  Vector3.up = new Vector3(0f,1f,0f); Vector3.front = new Vector3(0f,0f,1f);
        transform.localPosition -= Vector3.up * speed;


        if (transform.localPosition.y <= lowerlimit)
        {
            transform.localPosition = new Vector2(0, upperlimit);
        }

        else if(transform.localPosition.y <= assist)
        {

        }
    }
}