using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shooter.UnitStats
{
    public class Health : MonoBehaviour
    {
        [SerializeField] public int startingHealth = 5;
        private int currentHealth;

        private void OnEnable()
        {
            currentHealth = startingHealth;
        }
        public void TakeDamage(int damageAmount)
        {
            currentHealth -= damageAmount;
            if (currentHealth <= 0)
            {
                Die();
            }
        }

        public void SetMaxHealth()
        {
            currentHealth = startingHealth;
        }
        private void Die()
        {
            gameObject.SetActive(false);
        }
    }
}