using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 技能的种类
/// </summary>
public enum SkillType
{
    /// <summary>
    /// 攻击技能
    /// </summary>
    Aggresive,
    /// <summary>
    /// 移动技能
    /// </summary>
    Transfer,
    /// <summary>
    /// 增益技能
    /// </summary>
    Buff,
    /// <summary>
    /// 交涉技能
    /// </summary>
    Communicate,
    /// <summary>
    /// 物品技能
    /// </summary>
    Item,
    /// <summary>
    /// 被动技能
    /// </summary>
    Passive
}

/// <summary>
/// 技能指向种类
/// </summary>
public enum PointType
{
    /// <summary>
    /// 指向单一目标
    /// </summary>
    PointSingle,
    /// <summary>
    /// 对指向目标扩散
    /// </summary>
    PointWithRadius,
    /// <summary>
    /// 指向多名角色
    /// </summary>
    PointMultiple
}

/// <summary>
/// 技能的目标
/// </summary>
public enum TargetType
{
    /// <summary>
    /// 技能仅对敌人生效
    /// </summary>
    enemy,
    /// <summary>
    /// 技能对友方（包括自己）生效
    /// </summary>
    withFriend,
    /// <summary>
    /// 技能对除自己外的友方生效
    /// </summary>
    onlyFriend,
    /// <summary>
    /// 技能仅对自己生效
    /// </summary>
    self,
    /// <summary>
    /// 技能对所有被选者生效
    /// </summary>
    mixture
}

public class Skill : MonoBehaviour
{
    /// <summary>
    /// 技能是否激活
    /// </summary>
    public bool isActive = false;

    /// <summary>
    /// 技能指针
    /// </summary>
    public GameObject skillPoint;

    /// <summary>
    /// 玩家的鼠标位置
    /// </summary>
    public Vector3 mousePoint;

    public SkillType skillType;

    public PointType pointType;

    public TargetType targetType;

    public int targetNum = 1;

    /// <summary>
    /// 释放该技能的角色
    /// </summary>
    public GameObject costCharacter;

    /// <summary>
    /// 技能所指向的目标
    /// </summary>
    public List<GameObject> targets;

    public virtual void SkillActiveByEnemy()
    {

    }
    
    /// <summary>
    /// 玩家激活该技能
    /// </summary>
    public virtual void SkillActiveByPlayer()
    {
        Debug.Log("技能"+name+"激活");

        isActive = true;
    }

    /// <summary>
    /// 玩家激活该技能
    /// </summary>
    /// <param name="_costChara">释放技能的角色</param>
    public virtual void SkillActiveByPlayer(GameObject _costChara)
    {
        Debug.Log("技能" + name + "激活");

        isActive = true;

        costCharacter = _costChara;
    }

    /// <summary>
    /// 技能具体行动
    /// </summary>
    public virtual void Movement()
    {
        
    }

    /// <summary>
    /// 检测鼠标位置
    /// </summary>
    public Vector3 GetMousePosition()
    {
        mousePoint = Input.mousePosition;

        return mousePoint;
    }

    
    public virtual void SkillEndByEnemy()
    {

    } 

    /// <summary>
    /// 玩家结束释放该技能
    /// </summary>
    public virtual void SkillEndByPlayer()
    {
        isActive = false;
    }

    internal virtual void Start()
    {
        targets = new List<GameObject>();
    }

    internal virtual void Update()
    {
        //技能激活时，通过射线检测获取释放对象
        if (isActive)
        {
            Debug.Log("开始寻找目标");

            if (Input.GetMouseButtonUp(0))
            {
                Debug.Log("开接收到点击");

                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out hit))
                {
                    Debug.Log("射线碰撞到物体: " + hit.collider.gameObject.name);
                }
            }
        }

    }
}
