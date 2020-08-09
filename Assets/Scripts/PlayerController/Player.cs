using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Shooter.PlayerController
{
    public class Player : Unit
    {
        // Start is called before the first frame update
        void Start()
        {

        }
        protected override void Awake()
        {
            base.Awake();
            GetComponent<Mover>();
            GetComponent<CreateWeapon>().Create();
            _fraction = Fraction.Alias;
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                GetComponent<CreateWeapon>().Switch(CreateWeapon.Slot.First);
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                GetComponent<CreateWeapon>().Switch(CreateWeapon.Slot.Second);
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                GetComponent<CreateWeapon>().Switch(CreateWeapon.Slot.Third);
            }
        }
    }
}
