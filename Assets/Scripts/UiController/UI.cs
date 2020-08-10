using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Shooter.UnitStats;

namespace Shooter.UiController
{
    public class UI : MonoBehaviour
    {
        public Text soundText, playText, locText;
        public Text respawnText;
        private bool checkSound;
        private bool checkLoc;
        public static UI instance;
        public void Awake()
        {
            instance = this;
        }
        public void Localisation()
        {
            checkLoc = !checkLoc;
            if (checkLoc)
            {
                locText.text = "РУ";
                if (!checkSound)
                {
                    soundText.text = "ЗВУК: ВКЛ";
                }
                else
                {
                    soundText.text = "ЗВУК: ВЫКЛ";
                }
                playText.text = "ИГРАТЬ";
                //LocManager.instance.respawnText.text = "Возродиться";

            }
            else
            {
                locText.text = "EN";
                if (!checkSound)
                {
                    soundText.text = "SOUND: ON";
                }
                else
                {
                    soundText.text = "SOUND: OFF";
                }
                playText.text = "PLAY";
                // LocManager.instance.respawnText.text = "Respawn";
            }
        }
        public void Sound()
        {
            checkSound = !checkSound;
            if (!checkSound)
            {
                AudioListener.volume = 1;
                if (!checkLoc)
                {
                    soundText.text = "SOUND: ON";
                }
                else
                {
                    soundText.text = "ЗВУК: ВКЛ";
                }
            }
            else
            {
                AudioListener.volume = 0;
                if (!checkLoc)
                {
                    soundText.text = "SOUND: OFF";
                }
                else
                {
                    soundText.text = "ЗВУК: ВЫКЛ";
                }
            }

        }

    }
}