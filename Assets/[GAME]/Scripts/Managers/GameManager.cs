using ElephantSDK;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region Singleton
    public static GameManager instance = null;

    private void Awake()
    {
        if (instance == null) instance = this;

        //UpdatePlayerPerfs();
    }
    #endregion

    public enum GameSituation
    {
        notStarted, isStarted, onLevelEnd, isEnded
    }

    public GameSituation gameSituation;

    [HideInInspector] public int fakeLevelNum = 1; // ekranda gözüken (yani fake olmasınn nedeni aslında aynı levellerin dönmesi ama yeni elvelmiş gibi gösterilmesi)
    [HideInInspector] public int levelNum = 1; // gerçek level sayımız index e göre


    private void Start()
    {
        gameSituation = GameSituation.notStarted;
        UpdatePlayerPerfs();
        Time.timeScale = 1f;
    }

    void UpdatePlayerPerfs() // initial uı adjust
    {
        fakeLevelNum = PlayerPrefs.GetInt("FakeLevel",1);
        levelNum = PlayerPrefs.GetInt("Level", 1);
    }

    public void SendLevelStartedEvent()
    {
        Elephant.LevelStarted(fakeLevelNum);
    }

    public void EndGame(int endType) // 2 success // 1 fail // call this after level ended 
    {
        gameSituation = GameSituation.isEnded;

        switch (endType)
        {
            case 2:
                UIManager.instance.OpenClosePanels(2);
                Elephant.LevelCompleted(fakeLevelNum); // düzenlenecek
                break;
            case 1:
                UIManager.instance.OpenClosePanels(1);
                Elephant.LevelFailed(fakeLevelNum); // düzenlenecek
                break;
        }
    }

    public void NextLevel() // button method
    {
        fakeLevelNum++;
        levelNum++;

        RecordLevel();
        RecordFakeLevel();

        if (levelNum == SceneManager.sceneCountInBuildSettings)
        {
            levelNum = 1;
            RecordLevel();
        }

        print("loading scene");

        SceneManager.LoadScene(levelNum);
    }

    public void RetryLevel() // button method
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ManageGameStatus(GameSituation gameState)
    {
        gameSituation = gameState;
    }

    void RecordLevel()
    {
        PlayerPrefs.SetInt("Level", levelNum);
    }

    void RecordFakeLevel()
    {
        PlayerPrefs.SetInt("FakeLevel", fakeLevelNum);
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
    }
}
