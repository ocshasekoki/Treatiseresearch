using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Progress : MonoBehaviour
{
    string subjectName;
    string tangenName;
    int progress;
    GameObject mondai;
    int count;
    [SerializeField] Button[] buttons;
    // Start is called before the first frame update
    void Start()
    {
        subjectName = PlayerPrefs.GetString("Subject");
        foreach (Button b in buttons)
        {
            tangenName = b.gameObject.name;
            progress = PlayerPrefs.GetInt(subjectName + tangenName);
            mondai = (GameObject)Resources.Load("Prefabs/" + subjectName + "/" + tangenName);
            mondai = Instantiate(mondai);
            Debug.Log(tangenName);
            Debug.Log(progress);
            count = mondai.transform.childCount;
            b.gameObject.transform.Find("Slider").gameObject.GetComponent<Slider>().value = (float)progress / count-1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
