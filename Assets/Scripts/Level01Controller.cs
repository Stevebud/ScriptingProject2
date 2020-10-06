using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Level01Controller : MonoBehaviour
{
    [SerializeField] Text _currentScoreTextView;
    [SerializeField] GameObject _popupMenu;
    public Slider healthSlider;
    public PlayerHealth playerHealth;

    int _currentScore;

    private void Awake()
    {
        Resume();
    }
    private void Update()
    {
        //Increase Score
        //TODO replace with real implementation later
        if (Input.GetKeyDown(KeyCode.Q))
        {
            IncreaseScore(5);
        }
        //Exit Level
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(_popupMenu.activeSelf)
            {
                Resume();
            }
            else
            {
                MenuPopup();
            }
            
        }
        //Reload Level
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            ReloadScene();
        }
    }

    public void MenuPopup()
    {
        //enable popup menu and unlock cursor
        _popupMenu.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0f;
    }

    public void Resume()
    {
        //disable popup menu and lock cursor
        _popupMenu.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1f;
    }

    public void ExitLevel()
    {
        //compare score to high score
        int highScore = PlayerPrefs.GetInt("HighScore");
        if(_currentScore > highScore)
        {
            //save current score as high score
            PlayerPrefs.SetInt("HighScore", _currentScore);
            Debug.Log("New high score: " + _currentScore);
        }
        //load main menu
        SceneManager.LoadScene("MainMenu");
    }

    public void IncreaseScore(int scoreIncrease)
    {
        //increase score
        _currentScore += scoreIncrease;
        //update score display so we can see the new score
        _currentScoreTextView.text = "Score: " + _currentScore.ToString();
    }
    public void UpdateHealthSlider()
    {
        //set slider value equal to health
        healthSlider.value = playerHealth.health;
    }

    public void ReloadScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex);
    }
}
