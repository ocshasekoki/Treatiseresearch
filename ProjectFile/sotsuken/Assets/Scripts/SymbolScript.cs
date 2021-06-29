using UnityEngine;

public class SymbolScript: MonoBehaviour
{
    //スクロールスピード
    [SerializeField] private float speed = 0.01f;
    [SerializeField] private float upperlimit = 250f;
    [SerializeField] private float lowerlimit = -450f;

    private void Update()
    {
        //下方向にスクロール  Vector3.up = new Vector3(0f,1f,0f); Vector3.front = new Vector3(0f,0f,1f);
        transform.localPosition -= Vector3.up * speed *Time.deltaTime;

        if (transform.localPosition.y <= lowerlimit)
        {
            transform.localPosition = new Vector2(0, upperlimit);
        }
    }
    public void RealStop()
    {
        speed = 0;
    }
    public void RealStart(int spd)
    {
        speed = spd;
    }
}
