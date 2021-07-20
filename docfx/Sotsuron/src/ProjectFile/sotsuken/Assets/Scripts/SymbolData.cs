using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnumDic;

public class SymbolData : MonoBehaviour
{

    [SerializeField] protected Position pos = Position.LEFT;
    [SerializeField] protected Symbol symbol = Symbol.BAR;

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
