using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI[] sortedLevelTmps = null;
    [SerializeField] private int[] sortedTotalCoins = null;

    public void LoadScene(int buildIndex)
    {
        SceneManager.LoadScene(buildIndex);
    }

    public void UpdateLevelTMPs()
    {
        for(int i=1; i<SceneManager.sceneCountInBuildSettings; i++)
        {
            sortedLevelTmps[i-1].text = CoinDataHandler.GetCoinsByLevel((GameScene)i) + "/" + sortedTotalCoins[i-1];
        }
    }

    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
