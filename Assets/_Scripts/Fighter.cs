using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter : MonoBehaviour
{
    [Header("Основные параметры")]
    public bool isAlive = true;
    public int currentHealth;
    public int maxHealth;    

    void Awake()
    {
        //currentHealth = maxHealth;        
    }
    
    protected virtual void TakeDamage(int dmg)
    {        
        currentHealth -= dmg; 
        
        //Death
        if (currentHealth <= 0)
            {
                currentHealth = 0;
                isAlive = false;
                Death();
            }       
    }

    protected virtual void Heal(int healingAmount)
    {
        if (currentHealth == maxHealth)
            return;

        currentHealth += healingAmount;
        if (currentHealth > maxHealth)
            currentHealth = maxHealth;
    }

    protected virtual void Death()
    {
        isAlive = false;
        Debug.Log(transform.name + " died.");
    }
}