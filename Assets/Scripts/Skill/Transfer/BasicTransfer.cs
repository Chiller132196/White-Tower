using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicTransfer : Skill
{
    void Start()
    {
        
    }

    void Update()
    {
        if (isActive)
        {
            mousePoint = GetMousePosition();
            
        }
    }

}
