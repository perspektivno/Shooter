using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shooter.PlayerController
{
    [CreateAssetMenu(menuName = "Characters/Weapons", fileName = "New Weapon")]
    public class WeaponData : ScriptableObject
    {//id fireRate damage magazine recoilTime numberOfBulletsPerShot
        [SerializeField] int id;
        public int Id
        {
            get { return id; }
            protected set { }
        }
        [SerializeField] float fireRate;
        public float FireRate
        {
            get { return fireRate; }
            set { }
        }
        [SerializeField] int damage;
        public int Damage
        {
            get { return damage; }
            set { }
        }
        [SerializeField] int magazine;
        public int Magazine
        {
            get { return magazine; }
            set { }
        }
        [SerializeField] float recoilTime;
        public float RecoilTime
        {
            get { return recoilTime; }
            set { }
        }
        [SerializeField] int shootingType;
        public float ShootingType
        {
            get { return shootingType; }
            set { }
        }



    }
}