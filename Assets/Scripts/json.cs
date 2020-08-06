using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json.Linq;

public class json : MonoBehaviour
{
    void Json()
    {
        var path = "";
        var textAsset = Resources.Load<TextAsset>(path);
        if(textAsset!=null)
        {
            Dictionary<string, string> words = new Dictionary<string, string>();
            JObject jobject = JObject.Parse(textAsset.text);
            words = jobject.ToObject<Dictionary<string, string>>();
        }
    }
    //static string GetWord(string key, string lang)
    // {
      //  var string1 = 1;
      //return string1;
    //}
    //NewB
    
}
