using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Shooter.UnitStats;
using Shooter.UiController;
using UnityEngine.UI;

namespace Shooter.PlayerController
{
    public class Grenade : MonoBehaviour
    {
        public string grenadeHolder;
        public float delay = 3f;
        public Sprite grenadeImage;
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
            Health hpUnit = null;
            if (unit != null)
            {
                if (unit.GetComponent<Health>() != null)
                {
                    hpUnit = unit.GetComponent<Health>();
                }
                var dmg = 2;
                var health = unit.health;
                health.TakeDamage(dmg);
                if (hpUnit.currentHealth <= 0)
                {
                    // UiManager.instance.KillFeed(grenadeHolder, hpUnit.gameObject.name.ToString(), grenadeImage);
                    KillFeedBoard.instance.AddKillToDict(new KillRecord
                    {
                        weaponSprite = grenadeImage,
                        killer = grenadeHolder,
                        killed = hpUnit.gameObject.name.ToString()
                    });
                }
            }
        }
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.collider.tag ==  "Ground" ||
                collision.collider.tag == "Enemy" ||
                collision.collider.tag == "Ally")
                
            {
                Debug.Log("E!@;");
                Explode();
            }

        }
    }
}