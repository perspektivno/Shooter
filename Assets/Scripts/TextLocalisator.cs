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
        LocalisationManager.GetString(key);
    }
    private void Start()
    {
        LocalisationManager.onLangChange += SetText;
    }
    private void Destroy()
    {
        LocalisationManager.onLangChange -= SetText;
    }
}
}
