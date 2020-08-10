using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Shooter.PlayerController;

namespace Shooter.UnitStats
{
    public class Spawner : MonoBehaviour
    {
        public Unit enemyGo, aliasGo, playerGo;
        public static Spawner instance;
        // Start is called before the first frame update
        void Start()
        {

        }
        private void Awake()
        {
            instance = this;
        }
        public void CreateBoard()
        {
            Unit newUnit = Instantiate(enemyGo, transform.position, Quaternion.identity);
            playerGo.gameObject.SetActive(true);
            //Unit newPlayer = Instantiate(playerGo, transform.position, Quaternion.identity);
            newUnit.transform.position = new Vector3(10, 1, 10);
            //newPlayer.transform.position = new Vector3(0, 1, 0);
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}