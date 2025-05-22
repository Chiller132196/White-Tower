using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class ParticleManager : MonoBehaviour
{
    #region 对象池（暂不使用）
    private struct ParticleWithDieTime
    {
        GameObject particle;

        float dieTime;

        public ParticleWithDieTime(GameObject _particle, float _time)
        {
            particle = _particle;

            dieTime = _time;
        }

        public void Check()
        {
            if (dieTime > 0)
            {
                dieTime -= Time.deltaTime;
            }
        }

        public void Clear()
        {
            Destroy(particle);
        }
    }

    /// <summary>
    /// 盛放粒子的对象池
    /// </summary>
    private List<ParticleWithDieTime> particlePool;


    /// <summary>
    /// 直接清除粒子池中所有对象
    /// </summary>
    public void ClearPool()
    {
        foreach (ParticleWithDieTime particle in particlePool)
        {
            particle.Clear();
        }

        particlePool.Clear();
    }

    #endregion

    /// <summary>
    /// 数字效果
    /// </summary>
    public GameObject numberParticle;

    public static ParticleManager particleManager;

    /// <summary>
    /// 跳数字效果
    /// </summary>
    /// <param name="_target">目标物体</param>
    /// <param name="_number">数值</param>
    public void ShowNumberRespond(GameObject _target, int _number)
    {
        ShowNumberRespond(_target.transform.position, _number);
    }

    /// <summary>
    /// 跳数字效果
    /// </summary>
    /// <param name="_position">坐标</param>
    /// <param name="_number">数值</param>
    public void ShowNumberRespond(Vector3 _position, int _number)
    {
        GameObject number = Instantiate(numberParticle);

        if (_number > 0)
        {
            number.GetComponent<TextMeshPro>().color = new Color(0, 255, 0);

            number.GetComponent<TextMeshPro>().text = "+";
        }

        else if (_number <= 0)
        {
            number.GetComponent<TextMeshPro>().color = new Color(255, 0, 0);

            number.GetComponent<TextMeshPro>().text = "";
        }

        number.GetComponent<TextMeshPro>().text += _number;

        number.transform.position = _position;

        number.GetComponent<NumberJumper>().DieAt(3.0f);
    }

    public void TestNumber()
    {
        ShowNumberRespond(new Vector3(0,0,0), 10);
    }

    void Awake()
    {
        if (particleManager == null)
        {
            particleManager = this;

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
}
