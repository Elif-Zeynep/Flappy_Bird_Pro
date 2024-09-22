using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Player player;

    public Text scoreText;
    public GameObject playButton;
    public GameObject gameOverText;

    private int _score, _bestScore;
    private bool _isPaused = false;

    [SerializeField]
    private Text _pauseText, _resumeText, _bestScoreText;

    public void Awake()
    {
        Application.targetFrameRate = 60;
        StopGame();
    }

    // Start is called before the first frame update
    void Start()
    {
        _bestScore = PlayerPrefs.GetInt("HighScore", 0);
        _bestScoreText.text = "Best: " + _bestScore;
        _pauseText.gameObject.SetActive(false);
        _bestScoreText.gameObject.SetActive(false);
        _resumeText.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (_isPaused)
            {
                ResumeGame();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Play()
    {
        _score = 0;
        scoreText.text = _score.ToString();
        playButton.SetActive(false);
        gameOverText.SetActive(false);
        _pauseText.gameObject.SetActive(false);
        _bestScoreText.gameObject.SetActive(false);
        _resumeText.gameObject.SetActive(false);
        StartCoroutine(PlayPauseText());

        Time.timeScale = 1f;
        player.enabled = true;

        PipeMovement[] pipes = FindObjectsOfType<PipeMovement>();
        for ( int i = 0; i < pipes.Length; i++)
        {
            Destroy( pipes[i].gameObject );
        }

        CloudMovement[] clds = FindObjectsOfType<CloudMovement>();
        for (int i = 0; i < clds.Length; i++)
        {
            Destroy(clds[i].gameObject);
        }
    }

    public void Pause()
    {
        _isPaused = true;
        Time.timeScale = 0f;
        //player.enabled = false;
        _resumeText.gameObject.SetActive(true);
    }

    public void StopGame()
    {
        Time.timeScale = 0f;
        player.enabled = false;
    }

    public void GameOver()
    {
        UpdateBestScore();
        _bestScoreText.gameObject.SetActive(true);
        gameOverText.SetActive(true);
        playButton.SetActive(true);
        StopGame();
    }

    public void IncreaseScoreByOne()
    {
        _score++;
        scoreText.text = _score.ToString();
    }

    public void ResumeGame()
    {
        _resumeText.gameObject.SetActive(false);
        _isPaused = false;
        Time.timeScale = 1;
    }

    public void MakePaused()
    {
        _isPaused = false;
        _resumeText.gameObject.SetActive(true);
    }

    public void UpdateBestScore()
    {
        if (_score > _bestScore)
        {
            _bestScore = _score;
            PlayerPrefs.SetInt("HighScore", _bestScore);
        }
    }

    IEnumerator PlayPauseText()
    {
        _pauseText.gameObject.SetActive(true);
        yield return new WaitForSeconds(4f);
        _pauseText.gameObject.SetActive(false);
    }
}
