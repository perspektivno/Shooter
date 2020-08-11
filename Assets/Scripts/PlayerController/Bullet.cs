using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shooter.PlayerController
{
    public class Bullet : MonoBehaviour
    {
        private Vector3 _dir;
        private Rigidbody rb;
        public void SetDirection(Vector3 dir)
        {
            transform.rotation = Quaternion.Euler(dir + Vector3.up);
            _dir = dir;
            rb.velocity =_dir * 30f;
        }
        public void Awake()
        {
            rb = GetComponent<Rigidbody>();
        }
        private void OnCollisionEnter(Collision collision)
        {
            //PoolManager.instance.CollObject(Weapon.instance.obs[0], PoolObjectType.Bullet);
        }
    }
}