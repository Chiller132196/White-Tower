using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    #region 角色属性
    /// <summary>
    /// 角色力量
    /// </summary>
    public int strength;

    /// <summary>
    /// 角色活力
    /// </summary>
    public int vitality;

    /// <summary>
    /// 角色敏捷
    /// </summary>
    public int agility;

    /// <summary>
    /// 角色灵巧
    /// </summary>
    public int dex;

    /// <summary>
    /// 角色意志
    /// </summary>
    public int will;

    /// <summary>
    /// 角色知识
    /// </summary>
    public int knowledge;

    /// <summary>
    /// 角色智谋
    /// </summary>
    public int wisdom;

    /// <summary>
    /// 角色魔力存量
    /// </summary>
    public int storage;

    #endregion

    #region 修正属性
    /// <summary>
    /// 角色独立于属性计算的额外血量
    /// </summary>
    public int extraHealth;

    /// <summary>
    /// 角色的额外伤害修正值
    /// </summary>
    public int extraDmg;

    /// <summary>
    /// 角色独立于属性计算的额外魔力
    /// </summary>
    public int extraMana;

    /// <summary>
    /// 角色的额外机动性
    /// </summary>
    public int extraMotility;
    #endregion

    /// <summary>
    /// 角色外观
    /// </summary>
    public object out_look;

    /// <summary>
    /// 角色技能
    /// </summary>
    public List<GameObject> skill;

    #region 二级属性
    /// <summary>
    /// 角色血量
    /// </summary>
    private double health;

    /// <summary>
    /// 角色精力
    /// </summary>
    private double energy;

    /// <summary>
    /// 角色魔力
    /// </summary>
    private double mana;

    /// <summary>
    /// 角色机动性
    /// </summary>
    private double motility;
    #endregion

    /// <summary>
    /// 初始化角色二级属性
    /// </summary>
    public virtual void Load()
    {
        health = strength * 0.5 + vitality * 1;

        energy = vitality * 1 + dex * 0.5;

        if (storage > 0  || extraMana > 0)
        {
            mana = storage * 2 + extraMana;
        }

        motility = agility + dex * 0.25 + extraMotility;
    }

    public void ChangeHealth(int amount)
    {
        health = health + amount;
    }

    public void ChangeMana(int amount)
    {
        mana = mana + amount;
    }

    public void ChangeEnergy(int amount)
    {
        energy = energy + amount;
    }
}
