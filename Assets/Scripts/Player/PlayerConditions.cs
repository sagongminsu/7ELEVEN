using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public interface IDamageable
{
    void TakePhysicalDamage(int damage);
}

[System.Serializable]
public class Condition
{
    [HideInInspector]
    public float curValue;
    public float maxValue;

    public float startValue;
    public float regenRate;
    public float decayRate;
    public Image uiBar;


    public void Add(float amount)
    {
        curValue = Mathf.Min(curValue + amount, maxValue);
    }

    public void Subtract(float amount)
    {
        curValue = Mathf.Max(curValue - amount, 0.0f);
    }

    public float GetPercentage()
    {
        return curValue / maxValue;
    }

}
public class PlayerConditions : MonoBehaviour, IDamageable
{
    public Condition health;
    public Condition hunger;
    public Condition stamina;

    public GameObject dieUI;

    public float noHungerHealthDecay;

    public UnityEvent onTakeDamage;

    private PlayerController playerController;
    // Start is called before the first frame update
    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
    }

    void Start()
    {
        health.curValue = health.startValue;
        hunger.curValue = hunger.startValue;
        stamina.curValue = stamina.startValue;
    }

    // Update is called once per frame
    void Update()
    {
        hunger.Subtract(hunger.decayRate * Time.deltaTime);

        stamina.Add(stamina.regenRate * Time.deltaTime);

        if (hunger.curValue == 0.0f)
        {
            health.Subtract(noHungerHealthDecay * Time.deltaTime);
        }

        if (health.curValue == 0.0f)
        {
            Die();
        }

        health.uiBar.fillAmount = health.GetPercentage();
        hunger.uiBar.fillAmount = hunger.GetPercentage();
        stamina.uiBar.fillAmount = stamina.GetPercentage();
    }

    public void Heal(float amount)
    {
        health.Add(amount);
    }

    public void Eat(float amount)
    {
        hunger.Add(amount);
    }

    public bool UseStamina(float amount)
    {
        if (stamina.curValue - amount < 0)
        {
            return false;
        }

        stamina.Subtract(amount);
        return true;
    }

    public void Die()
    {
        //Debug.Log("�÷��̾� ���");
    }

    public void TakePhysicalDamage(int damage)
    {
        if (health.curValue <= 0)
        {
            Time.timeScale = 0;
            dieUI.SetActive(true);
            playerController.ToggleCursor(true);

        }
        health.Subtract(damage);
        onTakeDamage?.Invoke();        
    }
}
