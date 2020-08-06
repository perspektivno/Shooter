using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Shooter.UnitStats;
using System.Linq;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Shooter.PlayerController
{
    public class Weapon : MonoBehaviour
    {
        public WeaponData weaponData;
        public Transform firePoint;
        private float timer;
        private float recoilTimeAmount;
        private int saveMagazine;
        public LineRenderer lineVisual;
        public int lineSegment;
        private Vector3 newVec, newVec2;

        Quaternion spreadAngle = Quaternion.AngleAxis(-15, new Vector3(0, 10, 0));
        Quaternion spreadAngle2 = Quaternion.AngleAxis(15, new Vector3(0, 10, 0));

        //for grenade//
        public Rigidbody bulletPrefabs;
        public GameObject cursor;
        public LayerMask layer;
        private Camera cam;
        private bool grenadeIsActive = false;
        //for grenade
        public void InitializeWeapon(Transform firePoint)
        {
            recoilTimeAmount = weaponData.RecoilTime;
            saveMagazine = weaponData.Magazine;
            this.firePoint = firePoint;
            StopShoot();
        }
        
        public void Update()
        {
            if(grenadeIsActive)
            {
                LaunchGrenade();
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                grenadeIsActive = true;
            } 
            if (Input.GetButton("Fire2") && saveMagazine > 0)
            {
                
                timer += Time.deltaTime;
                if (timer >= weaponData.FireRate)
                {
                    timer -= weaponData.FireRate;
                    FireGun();
                    Click();
                    saveMagazine--;
                    Debug.Log($"magazine={saveMagazine}, id={weaponData.Id}");

                }
            }
            if(Input.GetButtonUp("Fire2") && saveMagazine > 0)
            {
                StopShoot();
            }
            if (saveMagazine == 0)
            {
                recoilTimeAmount -= Time.deltaTime;
                if(recoilTimeAmount < 0)
                {
                    saveMagazine = weaponData.Magazine;
                    recoilTimeAmount = weaponData.RecoilTime;
                }
            }
        }
        private void StopShoot()
        {
            timer = weaponData.FireRate;
        }
        private void FireGun()
        {

            newVec = spreadAngle * firePoint.forward;
            newVec2 = spreadAngle2 * firePoint.forward;
            Ray ray = new Ray(firePoint.position, firePoint.forward);
            Ray ray1 = new Ray(firePoint.position, newVec);
            Ray ray2 = new Ray(firePoint.position, newVec2);
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo, 100))
            {

                Debug.DrawRay(firePoint.position, firePoint.forward, Color.white, 5f);
                var health = hitInfo.collider.GetComponent<Health>();
                if (health != null)
                {
                    health.TakeDamage(weaponData.Damage);
                }
            }
            if (Physics.Raycast(ray1, out hitInfo, 100))
            {

                Debug.DrawRay(firePoint.position, newVec, Color.white, 5f);
                var health = hitInfo.collider.GetComponent<Health>();
                if (health != null)
                {
                    health.TakeDamage(weaponData.Damage);
                }
            }
            if (Physics.Raycast(ray2, out hitInfo, 100))
            {

                Debug.DrawRay(firePoint.position, newVec2, Color.white, 5f);
                var health = hitInfo.collider.GetComponent<Health>();
                if (health != null)
                {
                    health.TakeDamage(weaponData.Damage);
                }
            }
        }

        private void LaunchGrenade()
        {
            
            Ray CamRay = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(CamRay, out hit, 100f, layer))
            {
                Vector3 vo = Grenade(hit.point, firePoint.position, 1f);
                Visualize(vo);
                cursor.SetActive(true);
                cursor.transform.position = hit.point + Vector3.up * 2f;

                Vector3 Vo = Grenade(hit.point, firePoint.position, 1f);
                transform.rotation = Quaternion.LookRotation(Vo);
                if(Input.GetMouseButtonDown(0))
                {
                    Rigidbody obj = Instantiate(bulletPrefabs, firePoint.position, Quaternion.identity);
                    obj.velocity = Vo;
                    //OnCollisionEnter();
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
        private void OnCollisionEnter(Collision collision)
        {
            Destroy(collision.gameObject);
        }

        /////////////////////////////////////////////////
        private void Click()
        {
            StartCoroutine(GenerateRoutine(PoolObjectType.Bullet));
        }
        private IEnumerator GenerateRoutine(PoolObjectType type)
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
        public void Start()
        {
            cam = Camera.main;
            lineVisual.positionCount = lineSegment;
        }
        public void Awake()
        {
            cursor = Instantiate(cursor);
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
            for(int i = 0; i < lineSegment; i++)
            {
                Vector3 pos = CalculatePosInTime(vo, i / (float)lineSegment );
                lineVisual.SetPosition(i, pos);
            }
        }

        

    }
}