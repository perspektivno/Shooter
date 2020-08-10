using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace Shooter.Localisation { 
public class TextLocalisator : MonoBehaviour
{
    public string key;
    private Text text;
    private void Awake()
    {
         text = GetComponent<Text>();
    }
    private void SetText()
    {
            text.text = LocalisationManager.GetString(key);
            //LocalisationManager.onLangChange -= SetText;
        }
    private void Start()
    {
        LocalisationManager.onLangChange += SetText;
    }
    private void OnDestroy()
    {
        LocalisationManager.onLangChange -= SetText;
    }
}
}
