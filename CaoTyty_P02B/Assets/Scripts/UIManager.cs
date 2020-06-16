using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    //[SerializeField] GameObject gameManager;
    //GameManager gM;

    [SerializeField] GameObject CreditsUI = null;
    [SerializeField] GameObject DarkenBG = null;
    [SerializeField] Text highScoreText = null;

    int displayHighScore;

    private void Awake()
    {
        #region SINGLETON PATTERN
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
    }

    private void Start()
    {
        ////gM = gameManager.GetComponent<GameManager>();
        UpdateHighScore();
    }

    public void UpdateHighScore()
    {
        Debug.Log("update high score");
        displayHighScore = GameManager.instance.highScore;
        highScoreText.text = displayHighScore.ToString();
    }

    public void ResetScore()
    {
        //maybe add are you sure popup
        displayHighScore = 0;
        GameManager.instance.highScore = 0;
        GameManager.instance.SaveData();

        highScoreText.text = displayHighScore.ToString();
    }

    public void OpenCredits()
    {
        CreditsUI.SetActive(true);
        DarkenBG.SetActive(true);
    }

    public void ReturnToMenu()
    {
        CreditsUI.SetActive(false);
        DarkenBG.SetActive(false);
    }

    public void ActivateSelf(bool _onMenu)
    {
        gameObject.SetActive(_onMenu);
    }
}
