using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameSession : MonoBehaviour
{
    [SerializeField] int playerLives = 3;
    [SerializeField] TextMeshProUGUI livesText;
    [SerializeField] public TextMeshProUGUI ScoreText;
    [SerializeField] int score = 0;
    private void Awake()
    {
        int numGameSessions = FindObjectsOfType<GameSession>().Length;
        if (numGameSessions > 1)
        {
            Destroy(gameObject); //if there is more than one game session on awake destroy that object so we have only 1.asd
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
    private void Start()
    {
        livesText.text = playerLives.ToString();
        ScoreText.text = score.ToString();
    }
    public IEnumerator ProcessPlayerDeath()
    {
        if(playerLives > 1)
        {
            yield return new WaitForSecondsRealtime(4);
            TakeLife();
        }
        else
        {
            yield return new WaitForSecondsRealtime(4);
            ResetGameSession();
        }
    }

    public void AddToSCore(int pointsToAdd)
    {
        score += pointsToAdd;
        ScoreText.text = score.ToString();
    }
    private void TakeLife()
    {
        playerLives--;
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
        livesText.text = playerLives.ToString();
    }

    private void ResetGameSession()
    {
        FindObjectOfType<ScenePersist>().ResetScenePersist();
        SceneManager.LoadScene(0);
        Destroy(gameObject); // destroy this instance of game session if the game restarts.
    }
}
