using UnityEngine;

public class SoundScript : MonoBehaviour
{
    [SerializeField]AudioSource souce;
    [SerializeField] Vector2 spd;
    [SerializeField] Vector2 turnarea;
    [SerializeField] Vector2 defaultarea;
    [SerializeField]private Rigidbody2D r;
    private bool stop;
    [SerializeField]private bool roop;
    private bool turn;
    private void Start()
    {
        stop = false;
        turn = false;
    }
    private void Update()
    {
        if(!stop&&!roop) r.velocity = spd;
        if (roop)
        {
            float x = transform.position.x;
            if (spd.x > 0)
            {
                if (x >= turnarea.x && !turn) turn = true;
                else if (x <= defaultarea.x && turn) turn = false;
            }
            else if(spd.x<0)
            {
                if (x <= turnarea.x && !turn) turn = true;
                else if (x >= defaultarea.x && turn) turn = false;
            }
            if (turn)
            {
                r.velocity = -spd;
            }
            else
            {
                r.velocity = spd;
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        souce.Play();
        stop = true;
        r.isKinematic =true;
        r.velocity = Vector2.zero;
    }

    public void RoopFinish()
    {
        roop = false;
    }
}
