using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Shooter.UiController
{
    
    public class RecordPanel : MonoBehaviour
    {
        public Image weaponSprite;
        public Text killer;
        public Text killed;
        public void SetPanelInfo(KillRecord _killRecord)
        {
            killer.text= _killRecord.killer;
            killed.text = _killRecord.killed;
            weaponSprite.sprite = _killRecord.weaponSprite;
        }
        public KillRecord GetPanelInfo()
        {
            return new KillRecord { killer = killer.text, killed = killed.text, weaponSprite = weaponSprite.sprite };
        }
    }
}