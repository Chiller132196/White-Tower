using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fly : Skill
{
    void CheckTerra(Vector3 _mousePoint)
    {

    }

    void Start()
    {
        
    }

    void Update()
    {
        if (isActive)
        {
            mousePoint = GetMousePosition();

            skillPoint.transform.position = mousePoint;
        }
    }
}
