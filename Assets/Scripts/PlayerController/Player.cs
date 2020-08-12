using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Shooter.UnitStats;

namespace Shooter.PlayerController
{
    public class Player : Unit
    {
        public static Player instance;
        protected override void Awake()
        {
            instance = this;
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
