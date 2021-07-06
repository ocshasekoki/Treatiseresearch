using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void changeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    public void SetSubjectName(string name)
    {
        PlayerPrefs.SetString("Subject", name);
    }
    public void SetTangenName(string name)
    {
        PlayerPrefs.SetString("Tangen", name);
    }
}
