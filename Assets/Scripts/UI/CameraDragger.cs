using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraDragger : MonoBehaviour
{
    /// <summary>
    /// 摄像机移动速度
    /// </summary>
    public float dragSpeed = 10.0f;

    public bool isAwake = true;

    // public static CameraDragger cameraDragger;

    public Vector3 lastMousePosition;

    public Vector3 currentMousePosition;

    private void Awake()
    {

    }

    void Start()
    {

    }

    void Update()
    {

    }

    public void Wake()
    {
        isAwake = true;
    }

    public void Sleep()
    {
        isAwake = false;
    }
}
