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

        private Vector3 newVec, newVec2;
        Quaternion spreadAngle, spreadAngle2, spreadAngleMain;



        //for grenade//

        private bool grenadeIsActive = false;
        //for grenade
        public void InitializeWeapon()
        {
            recoilTimeAmount = weaponData.RecoilTime;
            saveMagazine = weaponData.Magazine;
            //this.firePoint = firePoint;
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

                    
                    spreadAngle = Quaternion.AngleAxis(-Random.Range(10, 20), new Vector3(0, 10, 0));
                    spreadAngle2 = Quaternion.AngleAxis(Random.Range(10,20), new Vector3(0, 10, 0));
                    FireGun(spreadAngle, spreadAngle2);
                    Click(spreadAngle, spreadAngle2);
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

        private void _RayCast(RaycastHit hitInfo)
        {
            Debug.DrawRay(firePoint.position, firePoint.forward, Color.white, 5f);
            var unit = hitInfo.collider.GetComponent<Unit>();
            //var health = hitInfo.collider.GetComponent<Health>();

            if (unit != null && unit.fraction == Unit.Fraction.Enemy)
            {
                var dmg = weaponData.Damage;
                var health = unit.health;
                health.TakeDamage(dmg);
                   // unit.health.TakeDamage(weaponData.Damage);
                
            }
        }
        private void FireGun(Quaternion spreadAngle, Quaternion spreadAngle2)
        {

            newVec = spreadAngle * firePoint.forward;
            newVec2 = spreadAngle2 * firePoint.forward;
            Ray ray = new Ray(firePoint.position, firePoint.forward);
            Ray ray1 = new Ray(firePoint.position, newVec);
            Ray ray2 = new Ray(firePoint.position, newVec2);
            RaycastHit hitInfo;
            
            if (Physics.Raycast(ray, out hitInfo, 100))
            {

                _RayCast(hitInfo);
            }
            if (Physics.Raycast(ray1, out hitInfo, 100))
            {
                _RayCast(hitInfo);

            }
            if (Physics.Raycast(ray2, out hitInfo, 100))
            {
                _RayCast(hitInfo);
            }
        }

        
        public void Start()
        {
        }
        

        

    }
}