using UnityEngine;
using System.IO;
using YG;

public class GameSaveManager
{
    private string _filePath => Path.Combine(Application.persistentDataPath, "save.json");

    public void Save(GameData data)
    {
        var json = JsonUtility.ToJson(data);
        File.WriteAllText(_filePath, json);
    }

    public GameData Load()
    {
        if (File.Exists(_filePath))
        {
            var json = File.ReadAllText(_filePath);
            var data = JsonUtility.FromJson<GameData>(json);
            return data;
        }

        return null;
    }
    

    public void ResetData()
    {
        if (File.Exists(_filePath))
        {
            File.Delete(_filePath);
        }
    }

    public bool SaveExists()
    {
        return File.Exists(_filePath);
    }
}