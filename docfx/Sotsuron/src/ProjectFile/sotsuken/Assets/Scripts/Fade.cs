using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Fade : MonoBehaviour, IPointerClickHandler
{
    private bool isFadeOut;
    private bool turn;
    [SerializeField] bool onTurn;
    private bool turnR = false;
    private bool turnG = false;
    private bool turnB = false;
    private Color color;
    private Color defcolor;

    [SerializeField] Color changeColor;
    private Renderer rend;
    [SerializeField] float fadeSpeed;
    void Start()
    {
        rend = GetComponent<Renderer>();
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
            if (onTurn)
            {
                color.r = ColorChange(color.r, defcolor.r, !turnR);
                color.g = ColorChange(color.g, defcolor.g, !turnG);
                color.b = ColorChange(color.b, defcolor.b, !turnB);
                rend.material.SetColor("_EmissionColor", color);
                if (color == defcolor) isFadeOut = false;
                return;
            }
            isFadeOut = false;
        }

        else
        {
            color.r = ColorChange(color.r, changeColor.r, turnR);
            color.g = ColorChange(color.g, changeColor.g, turnG);
            color.b = ColorChange(color.b, changeColor.b, turnB);
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

    public void OnPointerClick(PointerEventData eventData)
    {
        if (isFadeOut) return;
        defcolor = rend.material.GetColor("_EmissionColor");
        color = rend.material.GetColor("_EmissionColor");
        color.a = changeColor.a;
        defcolor.a = changeColor.a;
        if (defcolor.r < changeColor.r) turnR = true;
        if (defcolor.g < changeColor.g) turnG = true;
        if (defcolor.b < changeColor.b) turnB = true;
        turn = false;
        isFadeOut = true;
    }
}
