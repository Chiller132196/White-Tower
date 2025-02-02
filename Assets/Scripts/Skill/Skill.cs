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
public enum RangeType
{
    /// <summary>
    /// 指向性
    /// </summary>
    Point,
    /// <summary>
    /// 非指向性
    /// </summary>
    Shape
}

/// <summary>
/// 技能范围的种类
/// </summary>
public enum RangeShape
{
    /// <summary>
    /// 正圆技能范围
    /// </summary>
    circle,
    /// <summary>
    /// 椭圆技能范围
    /// </summary>
    oval,
    /// <summary>
    /// 矩形技能范围
    /// </summary>
    rectangle,
    /// <summary>
    /// 其他形状的技能范围
    /// </summary>
    other
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
    /// 技能对范围内所有人生效
    /// </summary>
    mixture
}

public class Skill : MonoBehaviour
{
    public SkillType skillType;

    public RangeShape rangeType;

    public  RangeShape rangeShape;

    /// <summary>
    /// 范围长度
    /// </summary>
    public double rangeLength = 0;

    /// <summary>
    /// 范围宽度，正圆忽略
    /// </summary>
    public double rangeWidth = 0;

    public TargetType targetType;

    public int targetNum = 0;

    public List<GameObject> targets;

    /// <summary>
    /// 技能具体行动
    /// </summary>
    public virtual void Movement()
    {

    }
}
