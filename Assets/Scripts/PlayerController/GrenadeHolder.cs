using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Shooter.PlayerController {
    public class GrenadeHolder : MonoBehaviour
    {
        [SerializeField] private Weapon weaponGrenade;
        private Camera cam;

        public Rigidbody bulletPrefabs;
        public GameObject cursor;
        public LayerMask layer;
        public LineRenderer lineVisual;
        public int lineSegment;

        private bool grenadeIsActive = false;
        public void Awake()
        {
            cursor = Instantiate(cursor);
        }
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
        private void LaunchGrenade()
        {

            Ray CamRay = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(CamRay, out hit, 100f, layer))
            {
                Vector3 vo = Grenade(hit.point, weaponGrenade.firePoint.position, 1f);
                Visualize(vo);
                cursor.SetActive(true);
                cursor.transform.position = hit.point + Vector3.up * 2f;

                Vector3 Vo = Grenade(hit.point, weaponGrenade.firePoint.position, 1f);
                transform.rotation = Quaternion.LookRotation(Vo);
                if (Input.GetMouseButtonDown(0))
                {
                    Rigidbody obj = Instantiate(bulletPrefabs, weaponGrenade.firePoint.position, Quaternion.identity);
                    obj.velocity = Vo;
                }
            }
            else
            {
                cursor.SetActive(false);
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
        private void Update()
        {
            if (grenadeIsActive)
            {
                LaunchGrenade();
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                grenadeIsActive = true;
            }
        }
        private Vector3 CalculatePosInTime(Vector3 vo, float time)
        {
            Vector3 Vxz = vo;
            Vxz.y = 0f;
            Vector3 result = weaponGrenade.firePoint.position + vo * time;
            float sY = (-0.5f * Mathf.Abs(Physics.gravity.y) * (time * time)) + (vo.y * time) + weaponGrenade.firePoint.position.y;
            result.y = sY;
            return result;
        }
    }
}