using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleTest : MonoBehaviour
{
    public float timer = 0f;

    void Start()
    {
        GetComponent<BattleManager>().NewRoundBegin();
    }

    void Update()
    {

    }
}
