using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SaveSystem;

public static class CoinDataHandler
{
    private static CoinData cd;
    public readonly static string CoinSavePath;
    private static FileSave fs;

    static CoinDataHandler()
    {
        CoinSavePath = Application.persistentDataPath + "/coinData.bin";
        fs = new FileSave(FileFormat.Binary);
        cd = fs.ReadFromFile(CoinSavePath, new CoinData());
    }

    public static void SaveCoinData()
    {
        fs.WriteToFile(CoinSavePath, cd);
    }

    public static int GetCoinsByLevel(GameScene level)
    {
        bool success = cd.CoinsByLevel.TryGetValue(level, out int coins);
        return success ? coins : 0;
    }

    public static void UpdateAndSaveCoinsByLevel(GameScene level, int coins)
    {
        UpdateCoinsByLevel(level, coins);
        SaveCoinData();
    }

    public static void UpdateCoinsByLevel(GameScene level, int coins)
    {
        cd.CoinsByLevel[level] = coins;
    }

    public static int GetTotalPlayerCoins()
    {
        int totalCoins = 0;
        foreach (KeyValuePair<GameScene, int> pair in cd.CoinsByLevel)
        {
            totalCoins += pair.Value;
        }
        return totalCoins;
    }
}
 