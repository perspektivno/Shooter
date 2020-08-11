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
        public void OnEnable()
        {
            //StartCoroutine(BulletGoBackInThePool());
        }
        private void OnCollisionEnter(Collision collision)
        {
            //Debug.Log(collision.collider.name);
            PoolManager.instance.CollObject(gameObject, PoolObjectType.Bullet);
        }
        private IEnumerator BulletGoBackInThePool()
        {
            yield return new WaitForSeconds(3.0f);
            PoolManager.instance.CollObject(gameObject, PoolObjectType.Bullet);
        }
    }
}