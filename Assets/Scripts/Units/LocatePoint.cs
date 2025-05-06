using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Location : MonoBehaviour
{
    public GameObject character;

    void Start()
    {
        if (character)
        {
            RefreshCharacterLocation();
        }
    }

    void Update()
    {
        
    }

    /// <summary>
    /// 获取要绑定的角色
    /// </summary>
    /// <param name="gameObject">绑定角色</param>
    public void GetLocateObject(GameObject gameObject)
    {
        character = gameObject;

        RefreshCharacterLocation();
    }

    public void RefreshCharacterLocation()
    {
        if (!character)
        {
            Debug.Log(name+"无绑定角色");

            return;
        }

        character.transform.position = new Vector3(transform.position.x, transform.position.y + 5.0f, transform.position.z);
    }
}
