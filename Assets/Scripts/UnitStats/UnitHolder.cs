using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Shooter.PlayerController;

namespace Shooter.UnitStats
{
    public class UnitHolder : MonoBehaviour
    {
        public static UnitHolder instance;
        public List<Unit> listOfPlayers = new List<Unit>();
        public int numberOfEnemy;
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
            Spawner.instance.CreateBoard(numberOfEnemy);
        }
        private void Awake()
        {
            instance = this;
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}