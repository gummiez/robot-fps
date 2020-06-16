using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameGUIManager : MonoBehaviour
{
    [SerializeField] GameObject GameMenu = null;
    [SerializeField] GameObject DimBG = null;
    [SerializeField] Text guiText = null;

    int score;
    public bool MenuOpen = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OpenGameMenu(MenuOpen);
        }

        UpdateScore();
        MenuTimeLock();
    }

    void OpenGameMenu(bool isMenuOpen)
    {
        //sets active to opposite (allows for escape to open and close menu)
        MenuOpen = !isMenuOpen;
        GameMenu.SetActive(MenuOpen);
        DimBG.SetActive(MenuOpen);
    }

    public void ResumeGame()
    {
        OpenGameMenu(true);
    }

    void MenuTimeLock()
    {
        if (MenuOpen == true)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Time.timeScale = 0;
        }
        else if(MenuOpen == false)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            Time.timeScale = 1;
        }
    }

    void UpdateScore()
    {
        score = GameManager.instance.currentScore;
        guiText.text = score.ToString();
    }

    public void QuitToMenu()
    {
        MenuOpen = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        GameManager.instance.ActivateMenu();
        GameManager.instance.SaveData();
        //GameManager.instance.LoadData();
    }

}
