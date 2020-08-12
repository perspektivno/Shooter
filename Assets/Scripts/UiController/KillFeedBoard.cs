using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Shooter.UiController
{
    public class KillRecord
    {
        public Sprite weaponSprite;
        public string killer;
        public string killed;
    }
    public class KillFeedBoard : MonoBehaviour
    {
        public static KillFeedBoard instance;
        public RecordPanel[] recordPanels;
        public Dictionary<int, KillRecord> dictOfKillRecords = new Dictionary<int, KillRecord>();
        private void Awake()
        {
            instance = this;
        }
        public void AddKillToDict(KillRecord killRecord)
        {
            dictOfKillRecords.Add(dictOfKillRecords.Count, killRecord);
            AddRecordToKillFeedBoard(killRecord, recordPanels);
        }
        public void AddRecordToKillFeedBoard(KillRecord killRecord, RecordPanel[] recordBoard)
        {
            if(recordBoard.Length >= dictOfKillRecords.Count)
            {
                recordBoard[dictOfKillRecords.Count - 1].gameObject.SetActive(true);
            }
            int i = recordBoard.Length - 1;
            while(i > 0)
            {
                recordBoard[i].SetPanelInfo(recordBoard[i - 1].GetPanelInfo());
                i--;
            }
            recordBoard[0].SetPanelInfo(killRecord);
        }
        
        
    }
}