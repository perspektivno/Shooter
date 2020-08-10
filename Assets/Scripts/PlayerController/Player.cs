using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Shooter.UnitStats;

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
            _fraction = Fraction.Alias;
        }

        public override void Respawn()
        {
            RespawnManager.instance.TurnOnRespawnUi();
        }

        // Update is called once per frame

    }
}
