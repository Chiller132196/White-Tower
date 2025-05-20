using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberJumper : MonoBehaviour
{
    public float dieTime;

    public bool isActive;

    /// <summary>
    /// 最终点
    /// </summary>
    private Vector3 goal;

    /// <summary>
    /// 数字跳动的速度
    /// </summary>
    public float jumpSpeed = 0.1f;

    public void DieAt(float _time)
    {
        dieTime = _time;

        isActive = true;

        goal = transform.position;
        goal.y += 2;
    }

    private void Awake()
    {
        isActive = false;
    }

    void Start()
    {

    }

    void Update()
    {
        if (isActive)
        {
            if (dieTime > 0)
            {
                dieTime -= Time.deltaTime;

                transform.position = Vector3.MoveTowards(transform.position, goal, jumpSpeed);
            }

            else
            {
                Destroy(gameObject);
            }
        }
    }

}
