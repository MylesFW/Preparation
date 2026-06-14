using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using Newtonsoft.Json;

public class JsonSerializer
{
    public string Serialize<T>(T data)
    {
        return JsonConvert.SerializeObject(data, formatting: Formatting.Indented);
    }
    public GameData Deserialize(string data) 
    {
        GameData loadData = JsonConvert.DeserializeObject<GameData>(data);
        return loadData;  
    }
}
