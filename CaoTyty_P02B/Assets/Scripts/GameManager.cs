using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    //[SerializeField] GameObject UIManager;
    //UIManager uiManager;

    public int currentScore;
    [SerializeField] string scoreKeyName = "SavedScore";

    public int highScore = 0;
    bool onMenu = true;

    private void Awake()
    {
        #region SINGLETON
        if (instance == null)
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
        else if (instance != null)
        {
            Destroy(gameObject);
        }
        #endregion
        LoadData();
    }

    private void Start()
    {
        //uiManager = UIManager.GetComponent<UIManager>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && onMenu == false)
        {
            currentScore += 5;
            Debug.Log("Score: " + currentScore);
        }
        if (Input.GetKeyDown(KeyCode.Escape) && onMenu == false) //change to open menu instead of exit
        {
            //QuitGame();
        }
    }

    public void SaveData()
    {
        CalculateHighScore();
        Debug.Log("Saving...");
        PlayerPrefs.SetInt(scoreKeyName, highScore);
        //CalculateHighScore();
    }

    public void LoadData()
    {
        Debug.Log("Loading...");
        highScore = PlayerPrefs.GetInt(scoreKeyName);
        UIManager.instance.UpdateHighScore();
        Debug.Log(highScore);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        //reset current score
    }

    public void CalculateHighScore()
    {
        if (currentScore > highScore)
        {
            highScore = currentScore;
        }
    }

    public void StartGame(int _levelNumber)
    {
        //loads next level index
        SceneManager.LoadScene(_levelNumber + 1);
        onMenu = false;
        UIManager.instance.gameObject.SetActive(false);
        currentScore = 0;
        //calculate high score after return to menu(start doesn't return)
    }

    public void ActivateMenu()
    {
        Debug.Log("active menu");
        onMenu = true;
        UIManager.instance.ActivateSelf(onMenu);
        LoadData();
        UIManager.instance.UpdateHighScore();
    }

    public void QuitGame()
    {
        SaveData();
        Application.Quit();
    }
}
