using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    #region 角色属性
    /// <summary>
    /// 角色力量
    /// </summary>
    public int strength = 1;

    /// <summary>
    /// 角色活力
    /// </summary>
    public int vitality = 1;

    /// <summary>
    /// 角色敏捷
    /// </summary>
    public int agility = 1;

    /// <summary>
    /// 角色灵巧
    /// </summary>
    public int dex = 1;

    /// <summary>
    /// 角色意志
    /// </summary>
    public int will = 1;

    /// <summary>
    /// 角色知识
    /// </summary>
    public int knowledge = 1;

    /// <summary>
    /// 角色智谋
    /// </summary>
    public int wisdom = 1;

    /// <summary>
    /// 角色魔力存量
    /// </summary>
    public int storage = 0;

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
    /// 被动技能
    /// </summary>
    public List<GameObject> passiveSkill;

    /// <summary>
    /// 主动角色技能
    /// </summary>
    public List<GameObject> positiveSkill;

    #region 二级属性
    /// <summary>
    /// 角色血量
    /// </summary>
    private float health;

    /// <summary>
    /// 角色精力
    /// </summary>
    private float energy;

    /// <summary>
    /// 角色魔力
    /// </summary>
    private float mana;

    /// <summary>
    /// 角色机动性
    /// </summary>
    private float motility;
    #endregion

    /// <summary>
    /// 初始化角色二级属性
    /// </summary>
    public virtual void Load()
    {
        health = strength * 0.5f + vitality * 1;

        energy = vitality * 1 + dex * 0.5f;

        if (storage > 0  || extraMana > 0)
        {
            mana = storage * 2 + extraMana;
        }

        motility = agility + dex * 0.25f + extraMotility;
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

    /// <summary>
    /// 激活棋子
    /// </summary>
    public virtual void Active()
    {
        BattleCamera.battleCamera.FocusOnMe(gameObject);
    }

    public virtual List<UnitLoadOnRound> RespondToRound()
    {
        UnitLoadOnRound self = new UnitLoadOnRound();

        self.gameObject = gameObject;
        self.motility = motility;

        return new List<UnitLoadOnRound> { self };
    }

    private void Start()
    {
        Load();
    }

    private void OnEnable()
    {
        BattleManager.OnSwitchRound += RespondToRound;
    }

    private void OnDisable()
    {
        BattleManager.OnSwitchRound -= RespondToRound;
    }

    private void OnDestroy()
    {
        BattleManager.OnSwitchRound -= RespondToRound;
    }
}
