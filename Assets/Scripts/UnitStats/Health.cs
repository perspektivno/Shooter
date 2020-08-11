using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Shooter.PlayerController;

namespace Shooter.UnitStats
{
    public class Health : MonoBehaviour
    {
        public static Health instance;
        [SerializeField] public int startingHealth = 5;
        public int currentHealth;

        public HealthBar healthBar;

        private void Awake()
        {
            instance = this;
        }
        private void OnEnable()
        {
            currentHealth = startingHealth;
        }
        public void TakeDamage(int damageAmount)
        {
            currentHealth -= damageAmount;
            healthBar.SetHealth(currentHealth);
            if (currentHealth <= 0)
            {
                Die();
            }
        }

        public void SetMaxHealth()
        {
            currentHealth = startingHealth;
            healthBar.SetMaxHealth(startingHealth);
        }
        private void Die()
        {
            gameObject.SetActive(false);
        }
    }
}