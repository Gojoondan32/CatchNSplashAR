using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuckHealth : MonoBehaviour
{
    [SerializeField] private float maxHealth = 5f;
    [SerializeField] private float currentHealth = 0f;
    [SerializeField] private HealthBar healthBar;

    private void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetSliderMaxHealth(maxHealth);
    }

    public void DamageCalculator(float damageIn)
    {
        currentHealth -= damageIn;
        healthBar.SetSliderHealth(currentHealth);
    }

    //Return the current health of the duck
    public float Health { get { return currentHealth; } }

    //Return when the current health is less than 0
    public bool IsDead { get { return currentHealth <= 0; } }
}
