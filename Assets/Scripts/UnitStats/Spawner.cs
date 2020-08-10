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
        public void CreateBoard(int count)
        {
            playerGo.gameObject.SetActive(true);
            for (int i = 1; i<= count; i++)
            {
                Unit newUnit = Instantiate(enemyGo, transform.position, Quaternion.identity);
                newUnit.transform.position = RespawnManager.instance.dots[i].transform.position;
            }

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}