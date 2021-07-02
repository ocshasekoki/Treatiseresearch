using UnityEngine;

public class Mondai : MonoBehaviour
{
    [SerializeField] private Genre genre;
    [SerializeField] private string MondaiText;
    [SerializeField] private string answer;
    [SerializeField] private string kaisetsu;
    [SerializeField] private int b;
    [SerializeField] private string[] t;
    [SerializeField] private bool clear;
}

public enum Genre
{
    SELECT,
    STRING,
    NUMBER
}
