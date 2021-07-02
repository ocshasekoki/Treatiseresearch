using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnswerScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Answer()
    {
        Syutudai s = GameObject.Find("MondaiSystem").GetComponent<Syutudai>();
        s.Answer(transform.Find("Text").GetComponent<Text>().text);
    }
}
