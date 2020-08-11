using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Shooter.UiController
{
    public class UiManager : MonoBehaviour
    {
        public static UiManager instance;
        public Sprite[] spriteWeapons = new Sprite[3];
        public Image[] weaponSprite = new Image[3];
        public Text[] killer = new Text[3];
        public Text[] killed = new Text[3];
        public GameObject[] EventFeed= new GameObject[3];
        public int numOfRecords;
        // Start is called before the first frame update
        private void Awake()
        {
            instance = this;
        }
        void Start()
        {
            
        }
        public void KillFeed(string _killer, string _killed , Sprite _weaponSprite, int count)
        {
            if(numOfRecords == 0)
            {
                CancelInvoke();
                EventFeed[0].gameObject.SetActive(true);
                killer[0].text = _killer;
                killed[0].text = _killed;
                weaponSprite[0].sprite = _weaponSprite;
                Invoke("ClearFeed", 5);
            }
            if(numOfRecords == 1)
            {
                CancelInvoke();
                EventFeed[1].gameObject.SetActive(true);
                killer[1].text = killer[0].text;
                killed[1].text = killed[0].text;
                weaponSprite[1].sprite = weaponSprite[0].sprite;
                killer[0].text = _killer;
                killed[0].text = _killed;
                weaponSprite[0].sprite = _weaponSprite;
                Invoke("ClearFeed", 7);
            }
            if(numOfRecords == 2)
            {
                CancelInvoke();
                EventFeed[2].gameObject.SetActive(true);
                killer[2].text = killer[1].text;
                killed[2].text = killed[1].text;
                weaponSprite[2].sprite = weaponSprite[1].sprite;
                killer[1].text = killer[0].text;
                killed[1].text = killed[0].text;
                weaponSprite[1].sprite = weaponSprite[0].sprite;
                killer[0].text = _killer;
                killed[0].text = _killed;
                weaponSprite[0].sprite = _weaponSprite;
                Invoke("ClearFeed", 7);
            }
            if(numOfRecords >= 3)
            {
                CancelInvoke();
                killer[0].text = _killer;
                killed[0].text = _killed;
                weaponSprite[0].sprite = _weaponSprite;
                EventFeed[1].gameObject.SetActive(false);
                EventFeed[2].gameObject.SetActive(false);
                numOfRecords = 0;
                Invoke("ClearFeed", 7);
            }
            numOfRecords += count;
            
        }
        private void ClearFeed()
        {
            
            EventFeed[0].gameObject.SetActive(false);
            EventFeed[1].gameObject.SetActive(false);
            EventFeed[2].gameObject.SetActive(false);
            numOfRecords = 0;
        }
        

        // Update is called once per frame
        void Update()
        {

        }
    }
}