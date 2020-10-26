using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Level01Controller : MonoBehaviour
{
    [SerializeField] Text _currentScoreTextView;
    [SerializeField] GameObject _popupMenu;
    [SerializeField] GameObject enemyCube;
    [SerializeField] Transform player;
    public Slider healthSlider;
    public PlayerHealth playerHealth;
    [SerializeField] float enemySpawnCooldown = 10f;
    [SerializeField] float maxEnemyDistance = 100f;
    //[SerializeField] AudioClip enemySpawnClip;

    int _currentScore;
    float spawnCooldownLeft;

    private void Start()
    {
        spawnCooldownLeft = 2f;
    }

    private void Awake()
    {
        Resume();
        //SpawnEnemy();
    }
    private void Update()
    {
        //update timer for cooldown
        spawnCooldownLeft -= Time.deltaTime;

        //spawn another enemy if the cooldown hits 0
        if(spawnCooldownLeft <= 0)
        {
            SpawnEnemy();
            spawnCooldownLeft = enemySpawnCooldown;
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

    public void SpawnEnemy()
    {
        float enDist = maxEnemyDistance / 10;
        Vector3 spawnPoint = new Vector3(Random.Range(-enDist, enDist) * 10, 0f, Random.Range(-enDist,enDist) * 10);
        //GameObject newEnemy = Instantiate(enemyCube, spawnPoint, enemyCube.transform.rotation);
        Instantiate(enemyCube, player.transform.position + spawnPoint, enemyCube.transform.rotation);
    }
}
