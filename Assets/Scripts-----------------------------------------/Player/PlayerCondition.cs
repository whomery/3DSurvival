using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamagaIbe
{
    void TakePhySicalDamage(int damage);
}
public class PlayerCondition : MonoBehaviour , IDamagaIbe
{

    public UiCondition uiCondition;

    Condition health { get { return uiCondition.health; } }
    Condition hunger { get { return uiCondition.hunger; } }
    Condition stamina { get { return uiCondition.stamina; } }

    public float noHungerGealthDecay;

    public event Action onTakeDamage;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        hunger.Subtract(hunger.passiveValue * Time.deltaTime);
        stamina.Add(stamina.passiveValue * Time.deltaTime); 

        if(hunger.curValue == 0f)
        {
            health.Subtract(noHungerGealthDecay * Time.deltaTime);
        }
        if(health.curValue == 0f)
        {
            Die();
        }

        

        
    }
    public void Heal(float amout)
    {
        health.Add(amout);
    }
    public void Eat (float amout)
    {
        hunger.Add(amout);
    }

    public void Die()
    {
        Debug.Log("¾ß");
    }

    void IDamagaIbe.TakePhySicalDamage(int damage)
    {
       health.Subtract(damage);
        onTakeDamage?.Invoke();
    }
}
