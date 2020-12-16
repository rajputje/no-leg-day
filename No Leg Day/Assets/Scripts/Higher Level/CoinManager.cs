using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class CoinManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI[] totalPlayerCoinTmps = null;
    [SerializeField] private TextMeshProUGUI[] collectedCoinTmps = null;
    [SerializeField] private TextMeshProUGUI[] bestScoreTmps = null;
    public GameObject[] Coins;
    
    private bool[] previousCoinsActiveState;
    private int? lastSceneIndex = null;

    private int lastSavedCoinBalance = 0;
    private int coinBalance = 0;

    private bool IsSaveToFileNeeded => CoinDataHandler.GetCoinsByLevel((GameScene)lastSceneIndex) < coinBalance;

    public int CoinBalance 
    {
        get 
        {
            return coinBalance;
        }
        set
        {
            coinBalance = value;
            UpdateCollectedCoinTmp();
        } 
    }
    
    private static CoinManager instance;

    public static CoinManager Instance { get { return instance; } }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            //if there is a new instance, get the new coins and destroy the new instance
            Debug.Log("New Instance of Coin Manager found");
            instance.Coins = Coins;
            DestroyImmediate(gameObject);
        }

        if (Coins.Length < 1)
        {
            Debug.LogWarning("Make sure you assigned all coin references to Coin Manager");
        }
    }

    private void Start()
    {
        UpdateTotalCoinTmps();
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnLevelLoaded;
        Checkpoint.CheckPointReached += OnCheckPointReached;
        GameManager.RestartingLevel += OnRestartingLevel;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnLevelLoaded;
        Checkpoint.CheckPointReached -= OnCheckPointReached;
        GameManager.RestartingLevel -= OnRestartingLevel;
    }

    private void OnCheckPointReached(Vector3 position)
    {
        SaveCollectedCoinData();
    }

    private void OnRestartingLevel()
    {
        DeleteCollectedCoinData();
    }

    private void OnLevelLoaded(Scene scene, LoadSceneMode mode)
    {
        if (lastSceneIndex == scene.buildIndex && previousCoinsActiveState != null)
        {
            LoadCollectedCoinData();
        }
        else if(lastSceneIndex != scene.buildIndex)
        {
            lastSceneIndex = scene.buildIndex;
            if (lastSceneIndex != (int) GameScene.MainMenu)
                SaveCollectedCoinData();
            Debug.Log("New scene was loaded: " + scene.buildIndex);
        }
    }

    private void DeleteCollectedCoinData()
    {
        Debug.LogWarning("Delete Coin Data isn't completely implemented. Check comments for info.");
        coinBalance = 0;
        previousCoinsActiveState = null;
    }

    private void SaveCollectedCoinData()
    {
        lastSavedCoinBalance = coinBalance;
        previousCoinsActiveState = new bool[Coins.Length];
        for(int i=0; i<Coins.Length; i++)
        {
            previousCoinsActiveState[i] = Coins[i].activeSelf;
        }
    }

    private void LoadCollectedCoinData()
    {
        CoinBalance = lastSavedCoinBalance;
        if (previousCoinsActiveState.Length != Coins.Length)
        {
            Debug.LogWarning("Previous coin count doesn't match with the current count");
        }
        else
        {
            for (int i = 0; i < Coins.Length; i++)
            {
                Coins[i].SetActive(previousCoinsActiveState[i]);
            }
        }
    }

    public void SaveToFileIfNeeded()
    {
        if(IsSaveToFileNeeded)
        {
            CoinDataHandler.UpdateAndSaveCoinsByLevel((GameScene)lastSceneIndex, coinBalance);
        }
        UpdateTmpsWithCoinData();
    }

    private void UpdateTmpCollection(string updatedString, TextMeshProUGUI[] tmps)
    {
        foreach(TextMeshProUGUI tmp in tmps)
        {
            tmp.text = updatedString;
        }
    }

    private void UpdateCollectedCoinTmp()
    {
        UpdateTmpCollection(CoinBalance + "/" + Coins.Length, collectedCoinTmps);
    }

    private void UpdateTotalCoinTmps()
    {
        UpdateTmpCollection(CoinDataHandler.GetTotalPlayerCoins() + "", totalPlayerCoinTmps);
    }

    private void UpdateBestScore()
    {
        UpdateTmpCollection(CoinDataHandler.GetCoinsByLevel((GameScene) lastSceneIndex) + "/" + Coins.Length, bestScoreTmps);
    }

    private void UpdateTmpsWithCoinData()
    {
        UpdateBestScore();
        UpdateTotalCoinTmps();
        UpdateCollectedCoinTmp();
    }
}
