using System;
using UnityEngine;
using System.IO;

public class GameSaveManager
{
    private string _filePath => Path.Combine(Application.persistentDataPath, "save.json");

    public void Save(GameData data)
    {
        var json = JsonUtility.ToJson(data);
        File.WriteAllText(_filePath, json);
        Debug.Log($" Save : level {data.Level} , Score {data.Score} , GameMod {data.GameMod} , Money {data.Money}");
        Debug.Log(_filePath);
    }

    public GameData Load()
    {
        if (File.Exists(_filePath))
        {
            var json = File.ReadAllText(_filePath);
            var data = JsonUtility.FromJson<GameData>(json);
            Debug.Log($" Save : level {data.Level} , Score {data.Score} , GameMod {data.GameMod} , Money {data.Money}");
           // Debug.Log("Game loaded successfully!");
            return data;
        }

        Debug.LogWarning("Save file not found!");
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

[Serializable]
public class GameData
{
    [field:SerializeField] public int Level { get; private set; }
    [field:SerializeField] public int Score { get; private set; }
    [field:SerializeField] public GameMod GameMod { get; private set; }
    [field:SerializeField] public int Money { get; private set; }
  // public List<Skin> BoughtSkins { get; private set; }
  // public Skin ActiveSkin { get; private set; }
  // public List<Booster> BoughtBoosters { get; private set; }
  // public List<Background> BoughtBackgrounds { get; private set; }
    public GameData(int level, int score, GameMod gameMod, int money/*, List<Skin> boughtSkins, Skin activeSkin,
        List<Booster> boughtBoosters, List<Background> boughtBackgrounds*/)
    {
        Level = level;
        Score = score;
        GameMod = gameMod;
        Money = money;
       // BoughtSkins = boughtSkins;
       // ActiveSkin = activeSkin;
       // BoughtBoosters = boughtBoosters;
       // BoughtBackgrounds = boughtBackgrounds;
    }
}