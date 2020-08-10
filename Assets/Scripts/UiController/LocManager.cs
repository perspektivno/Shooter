using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Shooter.UiController
{
    public class LocManager : MonoBehaviour
    {
        public static LocManager instance;
        public Text respawnText;
        private void Awake()
        {
            instance = this;


        }
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}