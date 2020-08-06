using Shooter.PlayerController;
using UnityEngine;

namespace Shooter.PlayerController
{
    public class Player : MonoBehaviour
    {
        private void Awake()
        {
            GetComponent<Mover>();
            GetComponent<CreateWeapon>().Create();
        }
        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.Alpha1))
            {
                 GetComponent<CreateWeapon>().Switch(CreateWeapon.Slot.First);
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                GetComponent<CreateWeapon>().Switch(CreateWeapon.Slot.Second);
            }
        }
    }
}