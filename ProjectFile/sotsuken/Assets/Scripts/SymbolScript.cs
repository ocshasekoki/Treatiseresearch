using UnityEngine;

public class SymbolScript: MonoBehaviour
{
    //スクロールスピード
    private float speed = 0;
    [SerializeField] private float upperlimit = 250f;
    [SerializeField] private float lowerlimit = -450f;
    [SerializeField] private GameObject nextobj = null;
    private void FixedUpdate()
    {
        //下方向にスクロール  Vector3.up = new Vector3(0f,1f,0f); Vector3.front = new Vector3(0f,0f,1f);
        transform.localPosition -= Vector3.up * speed;
    }
    private void Update()
    {
        if (transform.localPosition.y <= lowerlimit)
        {
            transform.localPosition = new Vector3(0f, nextobj.transform.localPosition.y + 100f, 0f);
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
