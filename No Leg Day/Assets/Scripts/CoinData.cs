using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CoinData
{
    public Dictionary<GameScene, int> CoinsByLevel = new Dictionary<GameScene, int>();
}
