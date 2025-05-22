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

    #endregion

    #region 修正属性
    /// <summary>
    /// 是否会魔法
    /// </summary>
    public bool magical = false;

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
    /// 不计入上限部分的血量
    /// </summary>
    public int tempHealth;

    /// <summary>
    /// 不计入上限部分的能量
    /// </summary>
    public int tempEnergy;

    /// <summary>
    /// 不计入上限部分的魔力
    /// </summary>
    public int tempMana;

    /// <summary>
    /// 角色的额外机动性
    /// </summary>
    public int extraMotility;
    #endregion

    #region 角色与外部衔接属性
    /// <summary>
    /// 角色外观
    /// </summary>
    public object outlook;

    /// <summary>
    /// 角色顶部GUI
    /// </summary>
    public GameObject stateBar;

    /// <summary>
    /// 被动技能
    /// </summary>
    public List<GameObject> passiveSkill;

    /// <summary>
    /// 主动角色技能
    /// </summary>
    public List<GameObject> positiveSkill;

    /// <summary>
    /// 当前装载的技能
    /// </summary>
    public GameObject nowLoadSkill;
    #endregion

    #region 二级属性
    /// <summary>
    /// 血量
    /// </summary>
    public int health;

    /// <summary>
    /// 角色血量
    /// </summary>
    public int Health
    {
        get => health;
        set
        {
            if (value < health)
            {
                int delta = health - value;
                if (tempHealth > 0)
                {
                    int tempDelta = Mathf.Min(delta, tempHealth);
                    tempHealth -= tempDelta;
                    delta -= tempDelta;
                }
                health -= delta;
            }

            if (value >= maxHealth)
            {
                health = maxHealth;
            }
        }
    }

    /// <summary>
    /// 角色最大血量
    /// </summary>
    private int maxHealth;

    /// <summary>
    /// 精力
    /// </summary>
    public int energy;

    /// <summary>
    /// 角色精力
    /// </summary>
    private int Energy
    {
        get => energy;
        set
        {
            if (value < energy)
            {
                int delta = energy - value;
                if (tempEnergy > 0)
                {
                    int tempDelta = Mathf.Min(delta, tempEnergy);
                    tempEnergy -= tempDelta;
                    delta -= tempDelta;
                }
                energy -= delta;
            }

            if (value >= maxEnergy)
            {
                energy = maxEnergy;
            }
        }
    }

    private int maxEnergy;

    /// <summary>
    /// 魔力
    /// </summary>
    public int mana;

    /// <summary>
    /// 角色魔力
    /// </summary>
    private int Mana
    {
        get => mana;
        set
        {
            if (value < mana)
            {
                int delta = mana - value;
                if (tempMana > 0)
                {
                    int tempDelta = Mathf.Min(delta, tempMana);
                    tempMana -= tempDelta;
                    delta -= tempDelta;
                }
                mana -= delta;
            }

            if (value >= maxMana)
            {
                mana = maxMana;
            }
        }
    }

    private int maxMana;

    /// <summary>
    /// 角色机动性
    /// </summary>
    private float motility;

    /// <summary>
    /// 角色免伤
    /// </summary>
    private int defence;
    #endregion


    #region 角色数值相关逻辑

    /// <summary>
    /// 初始化角色二级属性
    /// </summary>
    public virtual void Load()
    {
        //生命值初始化
        maxHealth = (int)System.Math.Round(strength * 0.5f + vitality * 1) + extraHealth;
        Health = maxHealth;

        // 计算能量
        maxEnergy = (int)System.Math.Round(vitality * 0.2f + dex * 0.05f) + extraMana;
        Energy = maxEnergy;

        if (magical)
        {
            maxMana = (int)System.Math.Round(vitality * 1 + dex * 0.5f) + extraMana;
            Mana = maxMana;
        }

        // 计算机动性
        motility = agility + dex * 0.25f + extraMotility;

        // 实例化技能
        for(int i = 0; i < passiveSkill.Count; i++)
        {
            passiveSkill[i] = Instantiate(passiveSkill[i]);
        }

        for (int i = 0; i < positiveSkill.Count; i++)
        {
            positiveSkill[i] = Instantiate(positiveSkill[i]);
        }

        CheckStateBar();
    }

    /// <summary>
    /// 直接对血量进行修正
    /// </summary>
    /// <param name="amount">修正值</param>
    public virtual void ChangeHealth(int _amount)
    {
        Health = Health + _amount;

        CheckState();
        CheckStateBar();
    }

    private void ChangeMana(int _amount)
    {
        Mana = Mana + _amount;

        CheckState();
        CheckStateBar();
    }

    private void ChangeEnergy(int _amount)
    {
        Energy = Energy + _amount;

        CheckState();
        CheckStateBar();
    }

    public int CostEnergy(int _amount)
    {
        if ( (Energy+tempEnergy) < _amount)
        {
            return 0;
        }

        ChangeEnergy(-1 * _amount);

        return 1;
    }

    public int CostMana(int _amount)
    {
        if ( (Mana+tempMana) < _amount)
        {
            return 0;
        }

        ChangeMana(-1 * _amount);

        return 1;
    }

    /// <summary>
    /// 受到伤害
    /// </summary>
    /// <param name="_amount">伤害量</param>
    /// <param name="_adder">施加者</param>
    public void GetDamage(int _amount, GameObject _adder)
    {
        // 避免伤害为负
        int damage = -1 * Mathf.Max(0, _amount - defence);

        ParticleManager.particleManager.ShowNumberRespond(gameObject, damage);

        ChangeHealth(damage);
    }

    /// <summary>
    /// 被治疗
    /// </summary>
    /// <param name="_amount">治疗量</param>
    /// <param name="_adder">施加者</param>
    public void GetHeal(int _amount, GameObject _adder)
    {
        ParticleManager.particleManager.ShowNumberRespond(gameObject, _amount);

        ChangeHealth(_amount);
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
    /// 调整数值条
    /// </summary>
    public virtual void CheckStateBar()
    {
        stateBar.GetComponent<StateBar>().ChangeHealth( (float)Health / maxHealth );
        stateBar.GetComponent<StateBar>().ChangeEnergy( (float)Energy / maxEnergy);
        if (magical)
        {
            stateBar.GetComponent<StateBar>().ChangeEnergy( (float)Mana / maxMana);
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

        Debug.Log(name + "开始行动");

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

    /// <summary>
    /// 释放对应序号的技能
    /// </summary>
    /// <param name="_number">技能序号</param>
    public virtual void LoadSkill(int _number)
    {
        Debug.Log("尝试释放"+ positiveSkill[_number].name);

        nowLoadSkill = positiveSkill[_number];

        positiveSkill[_number].GetComponent<Skill>().SkillActiveByPlayer(gameObject);
    }

    public virtual void ConfirmSkill()
    {
        if (!nowLoadSkill)
        {
            Debug.LogError("当前无正在释放的技能！");
            return;
        }

        nowLoadSkill.GetComponent<Skill>().ConfirmSkill();
    }

    public virtual void NeutralAction()
    {
        //中立单位逻辑需独立重写
        EndAction();
    }

    public virtual void EnemyAction()
    {
        //敌怪逻辑需独立重写
        EndAction();
    }

    /// <summary>
    /// 结束自身回合，如果有对应效果可在子类重写
    /// </summary>
    public virtual void EndAction()
    {
        BattleManager.battleManager.OnActionEnd();
    }

    /// <summary>
    /// 回合结束后恢复一定资源
    /// </summary>
    public virtual void Rest()
    {
        Energy += (int)System.Math.Round(vitality * 0.05 + dex * 0.03);
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
