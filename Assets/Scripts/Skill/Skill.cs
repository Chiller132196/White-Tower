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
    Passive,
    /// <summary>
    /// 混合型技能
    /// </summary>
    Mixture
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
    Enemy,
    /// <summary>
    /// 技能对友方（包括自己）生效
    /// </summary>
    WithFriend,
    /// <summary>
    /// 技能对除自己外的友方生效
    /// </summary>
    OnlyFriend,
    /// <summary>
    /// 技能仅对自己生效
    /// </summary>
    Self,
    /// <summary>
    /// 技能对所有被选者生效
    /// </summary>
    Mixture
}

public class Skill : MonoBehaviour
{
    /// <summary>
    /// 技能是否被玩家激活
    /// </summary>
    public bool isActiveByPlayer = false;

    /// <summary>
    /// 技能是否被敌方激活
    /// </summary>
    public bool isActiveByMonster = false;

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

    /// <summary>
    /// 是否要求选中目标数量上限后才能释放技能
    /// </summary>
    public bool requireMaxTarget = false;

    public int targetNum = 1;

    /// <summary>
    /// 释放该技能的角色
    /// </summary>
    public GameObject costCharacter;

    /// <summary>
    /// 技能所指向的目标
    /// </summary>
    public List<GameObject> targets;

    public virtual void SkillActiveByEnemy(GameObject _costChara)
    {

    }
    
    /// <summary>
    /// 玩家激活该技能
    /// </summary>
    public virtual void SkillActiveByPlayer()
    {
        Debug.Log("技能"+name+"激活");

        costCharacter = null;

        isActiveByPlayer = true;
    }

    /// <summary>
    /// 玩家激活该技能
    /// </summary>
    /// <param name="_costChara">释放技能的角色</param>
    public virtual void SkillActiveByPlayer(GameObject _costChara)
    {
        Debug.Log("技能" + name + "被玩家激活");

        costCharacter = _costChara;

        isActiveByPlayer = true;

    }

    /// <summary>
    /// 技能具体行动
    /// </summary>
    public virtual void SkillEffect()
    {
        if (skillType == SkillType.Aggresive)
        {

        }
    }

    /// <summary>
    /// 获取到技能目标后，尝试将其收纳
    /// </summary>
    public void AddSkillTarget(GameObject _temp)
    {
        if (targetType == TargetType.Mixture)
        {

        }

        else if (targetType == TargetType.Enemy)
        {

        }

        else if (targetType == TargetType.WithFriend)
        {

        }
    }

    public virtual void ConfirmSkill()
    {

    }

    public virtual void SkillEndByEnemy()
    {

    } 

    /// <summary>
    /// 玩家结束释放该技能
    /// </summary>
    public virtual void SkillEndByPlayer()
    {
        isActiveByPlayer = false;
    }

    internal virtual void Start()
    {
            targets = new List<GameObject>();
    }

    internal virtual void Update()
    {
        //技能被玩家激活的通用逻辑
        if (isActiveByPlayer)
        {
            // 技能仅对自身生效时，选中自己
            if (targetType == TargetType.Self)
            {
                targets = new List<GameObject> { costCharacter };
            }

            // 技能不止选中自己时，开始选择目标
            else
            {
                if (Input.GetMouseButtonUp(0))
                {
                    Debug.Log("接收到点击");

                    RaycastHit hit;
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                    if (Physics.Raycast(ray, out hit))
                    {
                        GameObject temp = hit.collider.gameObject;

                        if (temp.layer == 6)
                            Debug.Log("射线碰撞到物体: " + temp);

                        AddSkillTarget(temp);
                    }

                }

            }

        }
    }
}
