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
        /*
        if (cameraDragger == null)
        {
            cameraDragger = this;
            DontDestroyOnLoad(gameObject);
        }

        else
        {
            Destroy(gameObject);
        }
        */
    }

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            // Debug.Log("捕捉到点击");

            lastMousePosition = Input.mousePosition;
        }

        if (Input.GetMouseButton(1))
        {
            // Debug.Log("开始拖拽");

            currentMousePosition = Input.mousePosition;

            Vector3 deltaPosition = currentMousePosition - lastMousePosition;

            Vector3 dragDistance = new Vector3(-deltaPosition.x, 0, -deltaPosition.y) * Time.deltaTime * dragSpeed;

            transform.Translate(dragDistance, Space.Self);

            lastMousePosition = currentMousePosition;
        }

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
