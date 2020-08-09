using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shooter.PlayerController
{
    public class GrenadeHolder : Weapon
    {
        private Camera cam;

        public Rigidbody bulletPrefabs;
        public GameObject cursor;
        public LayerMask layer;

        public LineRenderer lineVisual;
        public int lineSegment;
        // Start is called before the first frame update
        void Start()
        {

            lineVisual.positionCount = lineSegment;
            cam = Camera.main;
        }

        private Vector3 CalculatePosInTime(Vector3 vo, float time)
        {
            Vector3 Vxz = vo;
            Vxz.y = 0f;
            Vector3 result = firePoint.position + vo * time;
            float sY = (-0.5f * Mathf.Abs(Physics.gravity.y) * (time * time)) + (vo.y * time) + firePoint.position.y;
            result.y = sY;
            return result;
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
                Vector3 vo = Grenade(hit.point, firePoint.position, 1f);
                Visualize(vo);
                cursor.SetActive(true);
                cursor.transform.position = hit.point + Vector3.up * 2f;

                Vector3 Vo = Grenade(hit.point, firePoint.position, 1f);
                transform.rotation = Quaternion.LookRotation(Vo);
                if (Input.GetMouseButtonDown(0) && saveMagazine > 0)
                {
                    Rigidbody obj = Instantiate(bulletPrefabs, firePoint.position, Quaternion.identity);
                    obj.velocity = Vo;
                    saveMagazine--;
                    //OnCollisionEnter();
                }
            }
            else
            {
                cursor.SetActive(false);
            }
        }
        public void Awake()
        {
            cursor = Instantiate(cursor);
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
        //private void OnCollisionEnter(Collision collision)
        //{
        //  Destroy(collision.gameObject);
        //}

        /////////////////////////////////////////////////
        private void Click(Quaternion spreadAngle, Quaternion spreadAngle2)
        {
            StartCoroutine(GenerateRoutine(PoolObjectType.Bullet, spreadAngle, spreadAngle2));
        }
        private IEnumerator GenerateRoutine(PoolObjectType type, Quaternion spreadAngle, Quaternion spreadAngle2)
        {
            newVec = spreadAngle * firePoint.forward;
            newVec2 = spreadAngle2 * firePoint.forward;
            GameObject ob = PoolManager.instance.GetPoolObject(type);
            GameObject ob1 = PoolManager.instance.GetPoolObject(type);
            GameObject ob2 = PoolManager.instance.GetPoolObject(type);

            ob.GetComponent<Bullet>().SetDirection(firePoint.forward);
            ob.transform.position = firePoint.position;
            Debug.Log(firePoint.position);
            ob.gameObject.SetActive(true);

            ob1.GetComponent<Bullet>().SetDirection(newVec);
            ob1.transform.position = firePoint.position;
            ob1.gameObject.SetActive(true);
            Debug.Log(newVec);


            ob2.GetComponent<Bullet>().SetDirection(newVec2);
            ob2.transform.position = firePoint.position;
            ob2.gameObject.SetActive(true);
            Debug.Log(newVec2);

            yield return new WaitForSeconds(4f);
            PoolManager.instance.CollObject(ob, type);
            PoolManager.instance.CollObject(ob1, type);
            PoolManager.instance.CollObject(ob2, type);
        }
    }
}