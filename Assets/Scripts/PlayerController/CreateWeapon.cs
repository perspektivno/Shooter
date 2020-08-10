using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Shooter.PlayerController;
using System;

namespace Shooter.PlayerController
{
    public class CreateWeapon : MonoBehaviour
    {
        private Transform weaponPoint;
        [SerializeField] private Weapon weaponPrefab0, weaponPrefab1;
        private Dictionary<Slot, Weapon> libraryOfWeapons = new Dictionary<Slot, Weapon>();
        private Weapon currentWeapon;

        private void Awake()
        {
            weaponPoint = transform;
        }

        void Start()
        {
            Create();
        }

        public void Create()
        {
            Weapon weapon0 = Instantiate(weaponPrefab0, weaponPoint);
            weapon0.InitializeWeapon();
            Weapon weapon1 = Instantiate(weaponPrefab1, weaponPoint);
            weapon1.InitializeWeapon();
            libraryOfWeapons.Add(Slot.First, weapon0);
            libraryOfWeapons.Add(Slot.Second, weapon1);
            weapon1.gameObject.SetActive(false);
            Switch(Slot.First);
            
        }
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                Switch(Slot.First);
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                Switch(Slot.Second);
            }
        }
        
        
        public void Switch(Slot slot)
        {
            if (currentWeapon != null) 
            { 
                currentWeapon.gameObject.SetActive(false); 
            }
            Weapon weapon = libraryOfWeapons[slot];
            currentWeapon = weapon;
            currentWeapon.gameObject.SetActive(true);
            Debug.Log(slot);
        }


        public enum Slot
        {
            First, Second
        }
    }
}