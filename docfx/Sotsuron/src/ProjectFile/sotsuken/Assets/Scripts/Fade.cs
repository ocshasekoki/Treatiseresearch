using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fade : MonoBehaviour
{
    public bool isFadeOut;
    public bool turn;
    private Color color;
    private Color defcolor;
    [SerializeField] Color changeColor;
    [SerializeField] Renderer rend;
    [SerializeField] float fadeSpeed;
    // Start is called before the first frame update
    void Start()
    {
        defcolor = rend.material.GetColor("_EmissionColor");
        color = rend.material.GetColor("_EmissionColor");
        color.a = changeColor.a;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isFadeOut)
        {
            StartFadeOut();
        }
    }
    private void StartFadeOut()
    {
        if (turn)
        {
            color.r = ColorChange(color.r, defcolor.r,true);
            color.g = ColorChange(color.g, defcolor.g,true);
            color.b = ColorChange(color.b, defcolor.b,true);
            if (color == defcolor) isFadeOut = false;
            rend.material.SetColor("_EmissionColor", color);
            return;
        }
        else
        {
            color.r = ColorChange(color.r, changeColor.r, false);
            color.g = ColorChange(color.g, changeColor.g, false);
            color.b = ColorChange(color.b, changeColor.b, false);
            Debug.Log("R:"+color.r +":" + changeColor.r);
            Debug.Log("G:"+color.g +":" + changeColor.g);
            Debug.Log("B:" + color.b +":" + changeColor.b);
            if (color ==changeColor) turn = true;
            rend.material.SetColor("_EmissionColor",color);
            return;
        }

    }
    private float ColorChange(float color,float chcolor,bool istrun)
    {
        if (!istrun)
        {
            if (chcolor < color)
            {
                return color - fadeSpeed;
            }
            return chcolor;
        }

        if (chcolor > color) 
        {
            return color + fadeSpeed; 
        }
        return chcolor;
    }

    public void FadeButton()
    {
        Debug.Log("ボタンが押された");
        turn = false;
        isFadeOut = true;
    }
}
