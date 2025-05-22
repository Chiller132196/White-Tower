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
            else
            {
                Destroy(points[0]);
                points.RemoveAt(0);
                targets = AddTarget(_temp);
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
            else
            {
                Destroy(points[0]);
                points.RemoveAt(0);
                targets = AddTarget(_temp);
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
            else
            {
                Destroy(points[0]);
                points.RemoveAt(0);
                targets = AddTarget(_temp);
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

            if (targets.Count < targetNum)
            {
                targets.Add(_temp);
            }
            else
            {
                Destroy(points[0]);
                points.RemoveAt(0);
                targets = AddTarget(_temp);
            }
        }

        points.Add(Instantiate(skillPoint));

        points[points.Count - 1].transform.position = new Vector3(_temp.transform.position.x, _temp.transform.position.y - 0.5f, _temp.transform.position.z);

    }

    public override void ConfirmSkill()
    {
        base.ConfirmSkill();

        foreach (GameObject point in points)
        {
            Destroy(point);
        }

        points.Clear();
    }

    internal override void Start()
    {
        base.Start();

        points = new List<GameObject> { };
    }
}
