﻿using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class BytesTexureLoad : MonoBehaviour
{
    string test="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";
    private void Start()
    {
        byte[] b = ReadFile("slot.jpg");
        string encodedText= System.Convert.ToBase64String(b);
        Debug.Log(encodedText);
        var texture=ReadTexture(test);
        GetComponent<Renderer>().material.mainTexture = texture;
    }
    void Update()
    {
        /*
        if (((int)Time.realtimeSinceStartup / 3) % 2 == 0)
        {
            var texture = ReadTexture("green.jpg");
            GetComponent<Renderer>().material.mainTexture = texture;
        }
        else
        {
            var texture = ReadTexture("slot.jpg");
            GetComponent<Renderer>().material.mainTexture = texture;
        }
        */
    }

    Texture ReadTexture(string text)
    {
        //byte[] readBinary = ReadFile(path);
        byte[] readBinary = ReadImage(text);
        Texture2D texture = new Texture2D(1, 1);
        texture.LoadImage(readBinary);
        return texture;
    }

    byte[] ReadFile(string path)
    {
        FileStream fileStream = new FileStream(Application.dataPath+ "/"+path, FileMode.Open, FileAccess.Read);
        BinaryReader bin = new BinaryReader(fileStream);
        byte[] values = bin.ReadBytes((int)bin.BaseStream.Length);
        Debug.Log(values[0]);
        bin.Close();

        return values;
    }
    byte[] ReadImage(string s)
    {
        byte[] values = System.Convert.FromBase64String(s);
        return values;
    }
}
