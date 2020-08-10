using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Shooter.UnitStats;

namespace Shooter.PlayerController
{
    public class Grenade : MonoBehaviour
    {
        public float delay = 3f;
        float countdown;
        bool hasExploded = false;
        public GameObject explosionEffect;
        private float explodeRadius = 5.0f;
        void Start()
        {
            countdown = delay;
        }

        // Update is called once per frame
        void Update()
        {
            countdown -= Time.deltaTime;
            if (countdown <= 0f && !hasExploded)
            {
                //Explode();
                hasExploded = true;
            }
        }
        void Explode()
        {
            GameObject eff = Instantiate(explosionEffect, transform.position, transform.rotation);
            eff.SetActive(true);
            var units = UnitHolder.instance.listOfPlayers;
            for(int i =0; i < units.Count; i++)
            {
                if (units[i] != null)
                {
                    if(Vector3.Distance(transform.position, units[i].transform.position) <= explodeRadius)
                    {
                        //Ray ray = new Ray(transform.position, units[i].transform.position);
                        RaycastHit hit;
                        if (Physics.Raycast(transform.position, units[i].transform.position - transform.position, out hit))
                        {
                             
                            _RayCast(hit);
                        }
                    }
                }
            }
            Destroy(gameObject);
        }
        private void _RayCast(RaycastHit hitInfo)
        {
            var unit = hitInfo.collider.GetComponent<Unit>();

            if (unit != null)
            {
                var dmg = 2;
                var health = unit.health;
                health.TakeDamage(dmg);

            }
        }
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.collider.tag ==  "Ground" ||
                collision.collider.tag == "Enemy")
            {
                Explode();
            }

        }
    }
}