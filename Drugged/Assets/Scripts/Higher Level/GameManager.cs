using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Transform SpawnTransform = null;
    private Vector3? lastCheckpointPos;

    [SerializeField] private GameObject PlayerPrefab = null;
    [SerializeField] private FistController LeftJoystick = null;
    [SerializeField] private FistController RightJoystick = null;

    [SerializeField] private GameObject essentialSingletonsContainer = null;

    private GameObject currentPlayer;

    public delegate void RestartingLevelHandler();
    public static RestartingLevelHandler RestartingLevel;

    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += SpawnPlayerIfNeeded;
        Checkpoint.CheckPointReached += OnCheckPointReached;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= SpawnPlayerIfNeeded;
        Checkpoint.CheckPointReached -= OnCheckPointReached;
    }

    public void SpawnPlayer()
    {
        Vector3 spawnPos = (lastCheckpointPos == null) ? SpawnTransform.position : (Vector3) lastCheckpointPos;
        currentPlayer = Instantiate(PlayerPrefab, spawnPos, PlayerPrefab.transform.rotation);
        CameraScript.Instance.PlayerTransform = currentPlayer.transform;
        Player player = currentPlayer.GetComponent<Player>();
        player.LFistMovements.JoystickAssigned = LeftJoystick;
        player.RFistMovements.JoystickAssigned = RightJoystick;
    }

    public void RestartFromLastCheckpoint()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void RestartLevel()
    {
        lastCheckpointPos = null;
        RestartingLevel.Invoke();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene((int) GameScene.MainMenu);
        Destroy(essentialSingletonsContainer);
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
    }

    public void LoadNextLevel()
    {
        Destroy(essentialSingletonsContainer);
        int nextScene = SceneManager.GetActiveScene().buildIndex + 1;
        bool doesNextLevelExist = Enum.IsDefined(typeof(GameScene), nextScene);
        nextScene = doesNextLevelExist ? nextScene : (int) GameScene.MainMenu;
        SceneManager.LoadScene(nextScene);
    }

    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }

    private void OnCheckPointReached(Vector3 position)
    {
        lastCheckpointPos = position;
    }

    private void SpawnPlayerIfNeeded(Scene scene, LoadSceneMode mode)
    {
        if(scene.buildIndex != (int) GameScene.MainMenu)
        {
            SpawnPlayer();
        }
    }
}
