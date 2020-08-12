using UnityEngine;
using Shooter.PlayerController;
using UnityEngine.AI;

namespace Shooter.UnitStats {
    public class UnitMover : MonoBehaviour
    {
        private GameObject player;
        public NavMeshAgent agent;
        private void Awake()
        {
            player = Player.instance.gameObject;
        }

        // Update is called once per frame
        void Update()
        {
            MoveToPlayer();
        }
        
        private void MoveToPlayer()
        {
            GetComponent<NavMeshAgent>().destination = player.transform.position;


        }
        
    }
}