using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Shooter.PlayerController
{
    public class Mover : MonoBehaviour
    {
        private CharacterController characterController;
        [SerializeField] private float moveSpeed = 100;
        private void Awake()
        {
            characterController = GetComponent<CharacterController>();
            //animator = GetComponentInChildren<Animator>();
        }
        void Update()
        {
            transform.LookAt(LookAtCursor());
            Movement();
        }
        private void Movement()
        {
            /*if(!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.A)
                && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.D))
            {
                return;
            }*/
            var hor = Input.GetAxis("Horizontal");
            var vert = Input.GetAxis("Vertical");
            var movement = new Vector3(hor, 1, vert);
            characterController.SimpleMove(movement * Time.deltaTime * moveSpeed);
            //animator.SetFloat("forwardSpeed", movement.magnitude);
        }
        private Vector3 LookAtCursor()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            bool hasHit = Physics.Raycast(ray, out hit);
            if (hasHit)
            {
                Vector3 tarPos = new Vector3(hit.point.x, 0, hit.point.z);
                return tarPos;
            }
            return Vector3.zero;
        }
    }
}