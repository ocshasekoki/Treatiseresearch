using UnityEngine;

public class SymbolScript: MonoBehaviour
{
    //スクロールスピード
    private float speed = 0;
    [SerializeField] private float lowerlimit = -450f;
    [SerializeField] private GameObject nextobj = null;
    private void FixedUpdate()
    {
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
