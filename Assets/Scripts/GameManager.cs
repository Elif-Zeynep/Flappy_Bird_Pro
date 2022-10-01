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

    private int score;

    public void Awake()
    {
        Application.targetFrameRate = 60;
        Pause();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Play()
    {
        score = 0;
        scoreText.text = score.ToString();
        playButton.SetActive(false);
        gameOverText.SetActive(false);

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
        Time.timeScale = 0f;
        player.enabled = false;
    }

    public void GameOver()
    {
        //Debug.Log("Game Over");
        gameOverText.SetActive(true);
        playButton.SetActive(true);
        Pause();
    }

    public void IncreaseScoreByOne()
    {
        //Debug.Log("Score");
        score++;
        scoreText.text = score.ToString();
    }
}
