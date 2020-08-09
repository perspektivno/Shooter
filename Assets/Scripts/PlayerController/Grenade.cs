using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shooter.PlayerController
{
    public class Grenade : MonoBehaviour
    {
        public float delay = 3f;
        float countdown;
        bool hasExploded = false;
        public GameObject explosionEffect;
        //private GameObject eff;
        // Start is called before the first frame update
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
            Destroy(gameObject);
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