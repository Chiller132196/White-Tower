using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ActionType
{
    neutral = 0,
    player = 1,
    enemy = 2
}

public class BattleManager : MonoBehaviour
{
    public static BattleManager battleManager;

    public int gameRound = 0;

    public ActionType actionType;

    /// <summary>
    /// 定义回合开始的事件，新回合开始时广播给所有棋子
    /// </summary>
    /// <returns></returns>
    public delegate List<UnitLoadOnRound> SwitchRound();

    /// <summary>
    /// 参与响应的实例方法
    /// </summary>
    public static event SwitchRound OnSwitchRound;

    /// <summary>
    /// 本回合参与行动的棋子
    /// </summary>
    public List<UnitLoadOnRound> chessThisRound;

    /// <summary>
    /// 棋子序列
    /// </summary>
    public List<GameObject> chessQueue;

    private void Awake()
    {
        if ( battleManager == null)
        {
            battleManager = this;

            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    #region 回合管理

    /// <summary>
    /// 开始新回合
    /// </summary>
    internal void NewRoundBegin()
    {
        gameRound += 1;

        chessQueue = new List<GameObject>();

        chessThisRound = NewRoundCheck();

        chessThisRound = NewRoundSort(chessThisRound);

        //调试用，检测排序后列表是否正确
        foreach (UnitLoadOnRound unit in chessThisRound)
        {
            // Debug.Log(unit.gameObject.name + "机动:" + unit.motility);

            chessQueue.Add(unit.gameObject);
        }

        NextAction();
    }

    /// <summary>
    /// 通过委托，接收当前回合的单位
    /// </summary>
    /// <returns>单位列表</returns>
    public static List<UnitLoadOnRound> NewRoundCheck()
    {
        List<UnitLoadOnRound> responders = new List<UnitLoadOnRound>();

        if (OnSwitchRound != null)
        {
            foreach (SwitchRound handler in OnSwitchRound.GetInvocationList())
            {
                responders.AddRange(handler());
            }
        }

        return responders;
    }

    /// <summary>
    /// 棋子行动排序算法
    /// </summary>
    private List<UnitLoadOnRound> NewRoundSort(List<UnitLoadOnRound> unitThisRound)
    {
        unitThisRound.Sort((p1, p2) => p2.motility.CompareTo(p1.motility));

        return unitThisRound;
    }

    /// <summary>
    /// 下一动，激活对应棋子
    /// </summary>
    public void NextAction()
    {
        chessQueue[0].GetComponent<Character>().Active();
    }

    public void OnActionEnd()
    {
        chessQueue.RemoveAt(0);

        if ( chessQueue.Count == 0)
        {
            NewRoundBegin();
        }

        NextAction();
    }

    public void SetActionType(int _type)
    {
        actionType = (ActionType)_type;
    }

    #endregion

    #region 技能交互

    /// <summary>
    /// 尝试调动某位置的技能
    /// </summary>
    /// <param name="_number">技能序号</param>
    public void LoadSkill(int _number)
    {
        if (actionType != ActionType.player)
        {
            Debug.Log("当前非玩家行动");
            return;
        }

            Debug.Log("尝试使单位激活该技能");

            chessQueue[0].GetComponent<Character>().LoadSkill(_number);
    }

    public void ConfirmSkill()
    {
        chessQueue[0].GetComponent<Character>().ConfirmSkill();
    }

    public void EndCharacterAction()
    {
        chessQueue[0].GetComponent<Character>().EndAction();
    }

    /// <summary>
    /// 敌人获取玩家角色以决定技能释放
    /// </summary>
    public List<GameObject> GetComputerSkillTargets()
    {
        List<GameObject> playerUnits = new List<GameObject>();

        return playerUnits;
    }

    #endregion
}
