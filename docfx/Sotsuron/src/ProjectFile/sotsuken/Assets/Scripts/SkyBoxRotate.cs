using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyBoxRotate : MonoBehaviour
{
    [SerializeField] Material skybox = null;
    [SerializeField] float speed = 0f;
    [SerializeField] float repeatValue =0f;
    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        repeatValue = Mathf.Repeat(skybox.GetFloat("_Rotation") + speed, 360f);
        skybox.SetFloat("_Rotation", repeatValue);
    }
}
