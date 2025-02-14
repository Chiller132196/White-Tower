using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTest : MonoBehaviour
{
    public float timer = 0f;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        FocusTest();
    }

    void FocusTest()
    {

        timer += Time.deltaTime;

        if (timer > 4f)
        {
            Debug.Log("尝试聚焦");

            BattleCamera.battleCamera.FocusOnMe(gameObject);

            timer = 0f;
        }

    }
}
