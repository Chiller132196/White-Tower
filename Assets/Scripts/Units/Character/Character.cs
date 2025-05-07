using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CharactersPrefer
{
    /// <summary>
    /// 更激进的进攻欲望
    /// </summary>
    Aggressive,
    /// <summary>
    /// 温和的进攻欲望
    /// </summary>
    Mild,
    /// <summary>
    /// 不计战损的进攻
    /// </summary>
    Crazy,
    /// <summary>
    /// 守护型角色
    /// </summary>
    Defender,
    /// <summary>
    /// 以生存为首要目标的角色
    /// </summary>
    Survival
}

public class Character : MonoBehaviour
{
    #region 角色属性
    /// <summary>
    /// 角色类型，0中立1玩家2敌人
    /// </summary>
    public int characterType = 0;

    /// <summary>
    /// 在敌方阵容时，角色的打法（目前仅参考，无影响）
    /// </summary>
    public CharactersPrefer prefer = CharactersPrefer.Mild;

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


    #region 角色数值相关逻辑

    /// <summary>
    /// 初始化角色二级属性
    /// </summary>
    public virtual void Load()
    {
        //生命值初始化
        health = strength * 0.5f + vitality * 1;

        energy = vitality * 1 + dex * 0.5f;

        if (storage > 0 || extraMana > 0)
        {
            mana = storage * 2 + extraMana;
        }

        motility = agility + dex * 0.25f + extraMotility;

        // Debug.Log(name+" motility:"+motility);

        // 实例化技能
        for(int i = 0; i < passiveSkill.Count; i++)
        {
            passiveSkill[i] = Instantiate(passiveSkill[i]);
        }

        for (int i = 0; i < positiveSkill.Count; i++)
        {
            positiveSkill[i] = Instantiate(positiveSkill[i]);
        }
    }

    public virtual void ChangeHealth(int amount)
    {
        health = health + amount;

        CheckState();
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
    /// 检测自身数值状态
    /// </summary>
    public virtual void CheckState()
    {
        if (health == 0)
        {
            Dead();
        }
    }

    /// <summary>
    /// 角色死亡逻辑
    /// </summary>
    internal virtual void Dead()
    {

    }

    #endregion

    #region 角色战斗相关逻辑
    /// <summary>
    /// 激活棋子
    /// </summary>
    public virtual void Active()
    {
        BattleManager.battleManager.SetActionType(characterType);

        if (characterType == 1)
        {
            BattleCamera.battleCamera.FocusOnMe(gameObject);
        }

        //中立单位时运行对应逻辑
        if (characterType == 0)
        {
            NeutralAction();
        }

        //敌对单位时运行对应逻辑
        else if (characterType == 2)
        {
            EnemyAction();
        }
    }

    public virtual void LoadSkill(int _number)
    {
        Debug.Log("尝试释放"+ positiveSkill[_number].name);

        positiveSkill[_number].GetComponent<Skill>().SkillActiveByPlayer(gameObject);
    }

    public virtual void NeutralAction()
    {
        BattleManager.battleManager.OnActionEnd();
    }

    public virtual void EnemyAction()
    {
        BattleManager.battleManager.OnActionEnd();
    }

    /// <summary>
    /// 新回合开始时响应事件
    /// </summary>
    /// <returns></returns>
    public virtual List<UnitLoadOnRound> RespondToRound()
    {
        UnitLoadOnRound self = new UnitLoadOnRound();

        self.gameObject = gameObject;
        self.motility = motility;

        return new List<UnitLoadOnRound> { self };
    }

    #endregion

    internal virtual void Start()
    {
        Load();
    }

    public virtual void OnEnable()
    {
        BattleManager.OnSwitchRound += RespondToRound;
    }

    public virtual void OnDisable()
    {
        BattleManager.OnSwitchRound -= RespondToRound;
    }

    public virtual void OnDestroy()
    {
        BattleManager.OnSwitchRound -= RespondToRound;
    }
}
