using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] AudioClip _startingSong;
    [SerializeField] Text _highScoreTextView;

    // Start is called before the first frame update
    void Start()
    {


        //load high score display
        int highScore = PlayerPrefs.GetInt("HighScore");
        _highScoreTextView.text = highScore.ToString();

        //play starting song when menu starts
        if(_startingSong != null)
        {
            AudioManager.Instance.PlaySong(_startingSong);
        }
    }

    public void ResetData()
    {
        PlayerPrefs.SetInt("HighScore", 0);
        _highScoreTextView.text = "0";
    }

    public void QuitApplication()
    {
        Application.Quit();
    }
}
