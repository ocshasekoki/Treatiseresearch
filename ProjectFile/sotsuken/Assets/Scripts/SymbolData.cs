using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SymbolData : MonoBehaviour
{

    [SerializeField] private Position pos = Position.LEFT;
    [SerializeField] private Symbol symbol = Symbol.BAR;

    public void Dump()
    {
        Debug.Log(pos.ToString());
        Debug.Log(symbol.ToString());
    }
    public Position GetPos()
    {
        return pos;
    }
    public Symbol GetSymbol()
    {
        return symbol;
    }
}
