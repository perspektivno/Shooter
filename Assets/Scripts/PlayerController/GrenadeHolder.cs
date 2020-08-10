using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Shooter.PlayerController {
    public class GrenadeHolder : MonoBehaviour
    {
        private Camera cam;
        public LineRenderer lineVisual;
        public int lineSegment;
        private bool aimingIsActive = false;
        public LayerMask layer;
        public SpriteRenderer cursor;
        public Rigidbody grenadeRigidBody;
        private Vector3 _vo;
        
        void Start()
        {

            lineVisual.positionCount = lineSegment;
            cam = Camera.main;
            
        }
        
        void Visualize(Vector3 vo)
        {
            for (int i = 0; i < lineSegment; i++)
            {
                Vector3 pos = CalculatePosInTime(vo, i / (float)lineSegment);
                lineVisual.SetPosition(i, pos);
            }
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.G))
            {
                TurnOnAiming();
            }

            if (aimingIsActive)
            {
                DrawGrenadeAim();
            }
            
            if (Input.GetKeyUp(KeyCode.G))
            {
                TurnOffAiming();
                ThrowGrenade();
            }
        }

        private void TurnOffAiming()
        {
            lineVisual.enabled = false;
            cursor.enabled = false;
            aimingIsActive = false;
        }

        private void TurnOnAiming()
        {
            lineVisual.enabled = true;
            cursor.enabled = true;
            aimingIsActive = true;
        }

        private void ThrowGrenade()
        {
            Rigidbody obj = Instantiate(grenadeRigidBody, transform.position, Quaternion.identity);
            obj.velocity = _vo;
        }

        private void DrawGrenadeAim()
        {
            Ray CamRay = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(CamRay, out hit, 100f, layer))
            {
                Vector3 vo = Grenade(hit.point, transform.position, 1f);
                Visualize(vo);

                if (hit.collider.CompareTag("Ground"))
                {
                    cursor.transform.position = hit.point + (Vector3.up * 0.1f);
                    cursor.transform.rotation = Quaternion.Euler(90f, 0f, 0f);
                }
                else
                {
                    cursor.transform.position = hit.point - (Vector3.forward * 0.1f);
                    cursor.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                }

                Vector3 Vo = Grenade(hit.point, transform.position, 1f);
                transform.rotation = Quaternion.LookRotation(Vo);
                _vo = Vo;
            }
        }

        private Vector3 Grenade(Vector3 target, Vector3 origin, float time)
        {
            Vector3 distance = target - origin;
            Vector3 distanceXZ = distance;
            distanceXZ.y = 0f;
            float Sy = distance.y;
            float Sxz = distanceXZ.magnitude;
            float Vxz = Sxz / time;
            float Vy = Sy / time + 0.5f * Mathf.Abs(Physics.gravity.y) * time;
            Vector3 result = distanceXZ.normalized;
            result *= Vxz;
            result.y = Vy;
            return result;
        }
        
        private Vector3 CalculatePosInTime(Vector3 vo, float time)
        {
            Vector3 Vxz = vo;
            Vxz.y = 0f;
            Vector3 result = transform.position + vo * time;
            float sY = (-0.5f * Mathf.Abs(Physics.gravity.y) * (time * time)) + (vo.y * time) + transform.position.y;
            result.y = sY;
            return result;
        }
    }
}