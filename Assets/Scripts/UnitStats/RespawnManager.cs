using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Shooter.PlayerController;

namespace Shooter.UnitStats
{
    public class RespawnManager : MonoBehaviour
    {
        private UnitHolder unitHolder;
        public static RespawnManager instance;
        public GameObject[] dots;
        private Player player = new Player();
        public GameObject uiButton;
        
        private void Awake()
        {
            instance = this;
        }

        private void OnEnable()
        {
            //unitHolder = UnitHolder.instance;
        }
        void Start()
        {
            unitHolder = UnitHolder.instance;
        }
        public void RespawnRequest(Unit unit)
        {
            unit.Respawn();
        }
        public void RespawnPlayer()
        {
            foreach(var unit in unitHolder.listOfPlayers)
            {
                if(unit is Player)
                {
                    player = unit as Player;
                }
            }
            
            player.health.SetMaxHealth();
            player.transform.position = dots[Random.Range(0, dots.Length - 1)].transform.position;
            player.gameObject.SetActive(true);
            uiButton.SetActive(true);

        }
        public void ButtonLock()
        {
            RespawnPlayer();
            uiButton.SetActive(false);
        }
        public void TurnOnRespawnUi()
        {
            uiButton.gameObject.SetActive(true);
        }
        public void DefaultRespawn(Unit unit)
        {
            StartCoroutine(DefaultRespawnDelay(5.0f, unit));
        }
        IEnumerator DefaultRespawnDelay(float time, Unit unit)
        {
       
            yield return new WaitForSeconds(time);
            unit.health.SetMaxHealth();
            unit.gameObject.SetActive(true);
        }
    }
}