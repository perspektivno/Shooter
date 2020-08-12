using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Shooter.PlayerController;
using UnityEngine.UI;

namespace Shooter.UnitStats
{
    public class RespawnManager : MonoBehaviour
    {
        public static RespawnManager instance;
        private UnitHolder unitHolder;
        public List<GameObject> dots;
        private Player player = new Player();
        public GameObject uiButton;
            

        private void OnEnable()
        {
            //unitHolder = UnitHolder.instance;
        }
        void Start()
        {
            unitHolder = UnitHolder.instance;
        }
        private void Awake()
        {
            instance = this;
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
            player.transform.position = dots[Random.Range(0, dots.Count - 1)].transform.position;
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
            int num = Random.Range(0, dots.Count);
            unit.health.SetMaxHealth();
            unit.gameObject.SetActive(true);
            unit.gameObject.transform.position = dots[num].transform.position;
        }
    }
}