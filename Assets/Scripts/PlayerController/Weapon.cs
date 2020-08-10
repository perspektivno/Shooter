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

        Vector3[] newVecs;
        Quaternion[] spreadAngleShot;

        public void InitializeWeapon()
        {
            recoilTimeAmount = weaponData.RecoilTime;
            saveMagazine = weaponData.Magazine;
            StopShoot();
        }
        public void Awake()
        {

            newVecs = new Vector3[weaponData.NumberOfBullets];
            spreadAngleShot = new Quaternion[weaponData.NumberOfBullets+1];
        }

        private void Click(Quaternion[] spreadAngle)
        {
            StartCoroutine(GenerateBullets(PoolObjectType.Bullet, spreadAngle));
        }
        private IEnumerator GenerateBullets(PoolObjectType type, Quaternion[] spreadAngle)
        {
            
            
            GameObject[] obs = new GameObject[weaponData.NumberOfBullets];
            for(int i = 0; i < weaponData.NumberOfBullets; i++)
            {
                newVecs[i] = spreadAngle[i] * firePoint.forward;
                obs[i] = PoolManager.instance.GetPoolObject(type);
                obs[i].GetComponent<Bullet>().SetDirection(newVecs[i]);
                obs[i].transform.position = firePoint.position;
                obs[i].gameObject.SetActive(true);
            }
               
            yield return new WaitForSeconds(3f);
            for(int i = 0; i < weaponData.NumberOfBullets; i++)
            {
                PoolManager.instance.CollObject(obs[i], type);
            }
        }
        public void Update()
        {
            if (Input.GetButton("Fire1") && saveMagazine > 0)
            {
                
                timer += Time.deltaTime;
                if (timer >= weaponData.FireRate)
                {
                    timer -= weaponData.FireRate;

                    spreadAngleShot[0] = Quaternion.AngleAxis(Random.Range(-5,5), new Vector3(0, 10, 0));
                    if (weaponData.NumberOfBullets > 1)
                    {
                        for(int i = 1; i < weaponData.NumberOfBullets; i++)
                        {
                            if(i%2 == 0)
                            {
                                spreadAngleShot[i] = Quaternion.AngleAxis((-1) ^ i * Random.Range(i*5, i * 5 + 10), new Vector3(0, 0.1f, 0));
                            }
                            else
                            {
                                spreadAngleShot[i] = Quaternion.AngleAxis((-1) ^ i * Random.Range((i+1) * 5, (i + 1) * 5 + 10), new Vector3(0, 0.1f, 0));
                            }
                            
                        }
                    }
                   
                    FireGun(spreadAngleShot);
                    Click(spreadAngleShot);
                    saveMagazine--;
                    Debug.Log($"magazine={saveMagazine}, id={weaponData.Id}");

                }
            }
            if(Input.GetButtonUp("Fire1") && saveMagazine > 0)
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

            if (unit != null && unit.fraction == Unit.Fraction.Enemy)
            {
                var dmg = weaponData.Damage;
                var health = unit.health;
                health.TakeDamage(dmg);
                
            }
        }
        private void FireGun(Quaternion[] spreadAngle)
        {
            Ray[] rays = new Ray[weaponData.NumberOfBullets];
            for(int i = 0; i < weaponData.NumberOfBullets; i++)
            {
                newVecs[i] = spreadAngle[i] * firePoint.forward;
                rays[i] = new Ray(firePoint.position, newVecs[i]);
               
                    RaycastHit hitInfo;
                    if (Physics.Raycast(rays[i], out hitInfo, 100))
                    {
                        _RayCast(hitInfo);
                    }
                

            }

            
            

        }

        
        

        

    }
}