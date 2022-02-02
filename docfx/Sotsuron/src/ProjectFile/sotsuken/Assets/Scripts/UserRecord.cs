using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UserRecord : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        string userid = PlayerPrefs.GetString("username");
        if (userid == "") SceneManager.LoadScene("Record");
        
    }
    public void changeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
