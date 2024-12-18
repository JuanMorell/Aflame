using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveGame : MonoBehaviour
{
    private const string extension = ".json";

    public void SaveFileAsJSon(string saveFile, JObject state)
    {
        string path = GetPathFromSaveFile(saveFile);
        print("Saving to " + path);
        using (var textWriter = File.CreateText(path))
        {
            using (var writer = new JsonTextWriter(textWriter))
            {
                writer.Formatting = Formatting.Indented;
                state.WriteTo(writer);
            }
        }
    }

    private string GetPathFromSaveFile(string saveFile)
    {
        return Path.Combine(Application.persistentDataPath, saveFile + extension);
    }
}
