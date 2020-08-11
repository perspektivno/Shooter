using Shooter.PlayerController;
using UnityEngine;
using Shooter.UnitStats;

namespace Shooter.PlayerController
{
    public class Unit : MonoBehaviour
    {
        [SerializeField]
        protected Fraction _fraction;

        private Health _health;

        public Fraction fraction
        {
            get
            {
                return _fraction;
            }
        }
        
        public Health health
        {
            get
            {
                return _health;
            }
            
        }

        public enum Fraction
        {
            Enemy, Alias
        }
        protected virtual void Awake()
        {
            FindObjectOfType<UnitHolder>().AddPlayer(this);
            _health = GetComponent<Health>();


        }
        public virtual void Respawn()
        {
            RespawnManager.instance.DefaultRespawn(this);
           
        }
        private void OnDisable()
        {
            RespawnManager.instance.RespawnRequest(this);
        }

    }
}