using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleCamera : MonoBehaviour
{
    /// <summary>
    /// 战斗中使用的摄像机
    /// </summary>
    public Camera nowUsedCamera;

    public static BattleCamera battleCamera;

    /// <summary>
    /// 摄像机被拖动的速度
    /// </summary>
    public float dragSpeed = 10.0f;

    /// <summary>
    /// 摄像机的缩放速度
    /// </summary>
    public float zoomSpeed = 15f;

    private float mouseScrollValue = 0f;

    public float maxFOV = 45f;

    public float minFOV = 25f;

    private Vector3 lastMousePosition;

    private Vector3 currentMousePosition;

    internal GameObject nowFoucusObject;

    /// <summary>
    /// 相机移动速度
    /// </summary>
    public float cameraSpeed = 5.0f;

    private void Awake()
    {
        if (battleCamera == null)
        {
            battleCamera = this;

            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
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

        mouseScrollValue = Input.GetAxis("Mouse ScrollWheel");

        if (mouseScrollValue != 0f)
        {
            float newFOV = nowUsedCamera.fieldOfView - mouseScrollValue * zoomSpeed;

            newFOV = Mathf.Clamp(newFOV, minFOV, maxFOV);

            nowUsedCamera.fieldOfView = newFOV;
        }
    }

    /// <summary>
    /// 使摄像机对准某物体
    /// </summary>
    public void FocusOnMe(GameObject _target)
    {
        nowFoucusObject = _target;

        float cameraDistance = 13f;

        Quaternion cameraRotation = nowUsedCamera.transform.rotation;

        //获取摄像机附着物体的坐标
        Vector3 targetPosition = _target.transform.position;

        Vector3 cameraDirection = cameraRotation * Vector3.back;

        Vector3 offset = cameraDistance * cameraDirection;

        //移动至目标点位
        transform.position = targetPosition + offset;
    }

    public void TempFocusOnMe(GameObject _target)
    {
        float cameraDistance = 13f;

        Quaternion cameraRotation = nowUsedCamera.transform.rotation;

        //获取摄像机附着物体的坐标
        Vector3 targetPosition = _target.transform.position;

        Vector3 cameraDirection = cameraRotation * Vector3.back;

        Vector3 offset = cameraDistance * cameraDirection;

        //移动至目标点位
        transform.position = targetPosition + offset;
    }

    public void ReFocus()
    {
        if (!nowFoucusObject)
        {
            Debug.LogWarning("当前无聚焦物体");

            return;
        }

        FocusOnMe(nowFoucusObject);
    }
}
