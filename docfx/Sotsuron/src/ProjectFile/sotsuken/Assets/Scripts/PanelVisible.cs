using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelVisible : MonoBehaviour
{
    [SerializeField] GameObject Panel = null;
    bool isOn = false;

    public void PushButton()
    {
        Panel.SetActive(!isOn);
        isOn = !isOn;
    }
}
