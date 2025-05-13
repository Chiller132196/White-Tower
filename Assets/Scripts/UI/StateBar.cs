using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StateBar : MonoBehaviour
{
    public GameObject healthBar;

    public GameObject energyBar;

    public GameObject manaBarParent;

    public GameObject manaBar;

    public float nowHealth;

    public float nowEnergy;

    public float nowMana;

    public void ChangeHealth(float _point)
    {
        healthBar.GetComponent<Image>().fillAmount = _point;
    }

    public void ChangeEnergy(float _point)
    {
        energyBar.GetComponent<Image>().fillAmount = _point;
    }

    public void ChangeMana(float _point)
    {
        manaBar.GetComponent<Image>().fillAmount = _point;
    }

    public void ShowMana()
    {
        manaBarParent.SetActive(true);
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
