using System;
using System.Collections.Generic;

[Serializable]
public class GameData
{
    public int Level;
    public int Score;
    public GameMod GameMod;
    public int Money;
    
    public List<SkinSaveData> Skins;
    public List<BackgroundSaveData> Backgrounds;
    public List<BoosterSaveData> Boosters;
    public List<int> IndexesAvailableLevels;

    public GameData(int level, int score, GameMod gameMod, int money, List<SkinSaveData> skins,
        List<BackgroundSaveData> backgrounds, List<BoosterSaveData> boosters, List<int> indexesAvailableLevels )
    {
        Level = level;
        Score = score;
        GameMod = gameMod;
        Money = money;
        Skins = skins;
        Backgrounds = backgrounds;
        Boosters = boosters;
        IndexesAvailableLevels = indexesAvailableLevels;
    }
}