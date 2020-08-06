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
            _dir = dir;
            rb.velocity =_dir * 10f;
        }
        public void Awake()
        {
            rb = GetComponent<Rigidbody>();
        }
    }
}