using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 该回合参与行动的棋子
/// </summary>
public class UnitLoadOnRound : System.IComparable<UnitLoadOnRound>
{
    public GameObject gameObject;

    public float motility;

    public int CompareTo(UnitLoadOnRound other)
    {
        return motility.CompareTo(other.motility);
    }
}
