using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempSkill : Skill
{
    public List<GameObject> points;

    public override void AddSkillTarget(GameObject _temp)
    {
        // 有该对象时，不再纳入
        if (targets.Contains(_temp))
        {
            return;
        }

        if (targetType == TargetType.Mixture)
        {
            if (targets.Count < targetNum)
            {
                targets.Add(_temp);
            }
        }

        // 此模式下，非敌人不纳入
        else if (targetType == TargetType.Enemy)
        {
            if (_temp.GetComponent<Character>().characterType != 2)
            {
                Debug.Log("Not right type");
                return;
            }

            if (targets.Count < targetNum)
            {
                targets.Add(_temp);
            }
        }

        // 此模式下，非友方不纳入
        else if (targetType == TargetType.WithFriend)
        {
            if (_temp.GetComponent<Character>().characterType != 1)
            {
                return;
            }

            if (targets.Count < targetNum)
            {
                targets.Add(_temp);
            }
        }

        // 此模式下，非除自己外的友方不纳入
        else if (targetType == TargetType.OnlyFriend)
        {
            if (_temp.Equals(costCharacter))
            {
                return;
            }
            else if (_temp.GetComponent<Character>().characterType != 1)
            {
                return;
            }
        }

        Destroy(points[0]);

        points.Add(Instantiate(skillPoint));

        points[points.Count - 1].transform.position = _temp.transform.position;

        targets = AddTarget(_temp);

    }
}
