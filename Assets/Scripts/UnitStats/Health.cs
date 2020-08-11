using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shooter.UnitStats
{
    public class Health : MonoBehaviour
    {
        [SerializeField] public int startingHealth = 5;
        private int currentHealth;

        public HealthBar healthBar;

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