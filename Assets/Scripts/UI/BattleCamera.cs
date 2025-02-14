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
        
    }

    /// <summary>
    /// 使摄像机对准某物体
    /// </summary>
    public void FocusOnMe(GameObject _target)
    {
        float cameraDistance = 12f;

        Quaternion cameraRotation = nowUsedCamera.transform.rotation;

        //获取摄像机附着物体的坐标
        Vector3 targetPosition = _target.transform.position;

        Vector3 cameraDirection = cameraRotation * Vector3.back;

        Vector3 offset = cameraDistance * cameraDirection;

        //移动至目标点位
        transform.position = targetPosition + offset;
    }

}
