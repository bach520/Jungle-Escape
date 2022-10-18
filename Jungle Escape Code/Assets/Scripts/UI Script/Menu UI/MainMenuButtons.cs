using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MainMenuButtons : MonoBehaviour
{
    #region variables
    [SerializeField]
    GameObject mainMenuUI;
    [SerializeField]
    GameObject levelSelectUI;
    [SerializeField]
    GameObject scoreBoard;
    #endregion

    void Start()
    {
        mainMenuUI.SetActive(true);
        levelSelectUI.SetActive(false);
        scoreBoard.SetActive(false);
    }

    #region standard buttons
    public void BackClickedLevel()
    {
        levelSelectUI.SetActive(false);
        mainMenuUI.SetActive(true);
    }

    public void BackClickedBoard()
    {
        scoreBoard.SetActive(false);
        mainMenuUI.SetActive(true);
    }

    public void NewGameClicked()
    {
        SceneManager.LoadScene("Opening cutscene dialogue", LoadSceneMode.Single);
    }

    public void LevelSelectClicked()
    {
        mainMenuUI.SetActive(false);
        levelSelectUI.SetActive(true);
        scoreBoard.SetActive(false);
    }

    public void ScoreBoardSelected()
    {
        mainMenuUI.SetActive(false);
        levelSelectUI.SetActive(false);
        scoreBoard.SetActive(true);
    }
    #endregion

    #region level select
    public void LevelOneClicked()
    {
        SceneManager.LoadScene("Level_1", LoadSceneMode.Single);
    }
    public void LevelTwoClicked()
    {
        SceneManager.LoadScene("Level_2", LoadSceneMode.Single);
    }
    public void LevelThreeClicked()
    {
        SceneManager.LoadScene("Boss Level", LoadSceneMode.Single);
    }
    public void ExitGame()
    {
        Application.Quit();
    }
    #endregion
}
