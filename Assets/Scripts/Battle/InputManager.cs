using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    /// <summary>
    /// 监听按钮按下
    /// </summary>
    /// <param name="number">对应的技能序号</param>
    public void OnSkillTriggerDown(int _number)
    {
        Debug.Log("激活" + _number+"技能");

        BattleManager.battleManager.LoadSkill(_number);
    }

    /// <summary>
    /// 监听摄像机归位按键
    /// </summary>
    public void OnReFocusTriggerDown()
    {
        BattleCamera.battleCamera.ReFocus();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //设置摄像机归位键位
        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnReFocusTriggerDown();
        }

    }
}
