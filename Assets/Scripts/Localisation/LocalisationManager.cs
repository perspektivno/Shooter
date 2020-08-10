using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json.Linq;

namespace Shooter.Localisation
{
    public enum Lang
    {
        en,
        ru
    }
    public class LocalisationManager
    {
        
        public static Dictionary<string, string> dictOfKeysAndValues = new Dictionary<string, string>();
        public static event System.Action onLangChange;
        private static  Lang localLang;
        public static Lang lang
        {
            get
            {
                return localLang;
            }
            set
            {
                switch(value)
                {
                    case Lang.ru:
                        ChangeCurrentLang("Localisation/ru");
                        break;
                    case Lang.en:
                        ChangeCurrentLang("Localisation/en");
                        break;
                }
                localLang = value;
                onLangChange?.Invoke();
            }
        }
        public static void ChangeCurrentLang(string _lang)
        {
            dictOfKeysAndValues.Clear();
            var textAssets = Resources.Load<TextAsset>(_lang);
            if (textAssets != null)
            {
                JObject jobject = JObject.Parse(textAssets.text);
                dictOfKeysAndValues = jobject.ToObject<Dictionary<string, string>>();
            }
        }
        public static string GetString(string key)
        {
            if (dictOfKeysAndValues.Count > 0)
            {
                return dictOfKeysAndValues[key];
            }
            else
                return key;
        }
        
    }




    
}
