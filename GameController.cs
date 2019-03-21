using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameObject hazard;
    public Vector3 spawnValues;
    public int hazardCount;
    public float spawnwait;
    public float startwait;
    public float wavewait;

    public Text ScoreText;
    public Text GameOverText;
    private int score;
    private bool gameOver;

    private void Start()
    {
        StartCoroutine (SpawnWaves());
        score = 0;
        UpdateScore();
        StartCoroutine(SpawnWaves());
        gameOver = false;
        GameOverText.text = "";
    }

    private void Update()
    {
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }

        if (Input.GetKey("r"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    IEnumerator SpawnWaves ()
    {
        yield return new WaitForSeconds(startwait);
        while (true)
        {
            for (int i = 0; i < hazardCount; i++)
            {
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(hazard, spawnPosition, spawnRotation);
                yield return new WaitForSeconds(spawnwait);
            }
            yield return new WaitForSeconds(wavewait);

            if (gameOver)
            {
                break;
            }
        }
    }

    public void AddScore(int newScoreValue)
    {
        score += newScoreValue;
        UpdateScore();
    }

    void UpdateScore()
    {
        ScoreText.text = "Score: " + score;
    }

    public void GameOver()
    {
        GameOverText.text = "Game Over! \n Press R to Restart";
        gameOver = true;
    }
}
