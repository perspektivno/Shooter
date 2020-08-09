using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Shooter.PlayerController;

namespace Shooter.UnitStats
{
    public class UnitHolder : MonoBehaviour
    {
        private List<Unit> listOfPlayers = new List<Unit>();
        public void AddPlayer(Unit player)
        {
            listOfPlayers.Add(player);
        }
        public void RemovePLayer(Unit player)
        {
            listOfPlayers.Remove(player);
        }

        // Start is called before the first frame update
        void Start()
        {

        }
        private void Awake()
        {
            
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}