using UnityEngine;

public class Mondai : MonoBehaviour
{
    public Genre genre;
    public string MondaiText;
    public string answer;
    public string kaisetsu;
    public int b;
    public string[] t;
    public bool clear;
}

public enum Genre
{
    SELECT,
    STRING,
    NUMBER
}
