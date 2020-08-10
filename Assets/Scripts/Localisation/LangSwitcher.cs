using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shooter.Localisation
{
    public class LangSwitcher : MonoBehaviour
    {
        void Start()
        {
            SetDefaultLang();
            
        }
        public void SwitchLang()
        {
            var currentLang = LocalisationManager.lang;
            switch(currentLang)
            {
                case Lang.ru:
                    LocalisationManager.lang = Lang.en;
                    break;
                case Lang.en:
                    LocalisationManager.lang = Lang.ru;
                    break;
            }
        }
        private void SetDefaultLang()
        {
            LocalisationManager.lang = Lang.en;
        }
    }
}