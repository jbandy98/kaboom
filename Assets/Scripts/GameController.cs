using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public List<GameObject> bombs;
    public GameObject bombPrefab;
    public GameObject spawnPoint;
    public RobberController robber;
    public PlatformController player;
    public List<LevelInfo> levelInfo;
    public int currentLevel;
    public LevelInfo currentLevelInfo;
    AudioSource audioSource;
    public AudioClip caughtSound;
    public AudioClip explosionSound;
    public int lives;
    public Text scoreText;
    public Text levelText;
    public Text livesText;
    public int score;

    // bomb spawner mechanics variables
    public float timeElapsed;
    public int bombsRemaining;
    public float bombDelay;
    public bool levelStarted;
    public bool levelLost;
    public bool endingTurn;

	// Use this for initialization
	void Start () {
        audioSource = GetComponent<AudioSource>();
        currentLevel = 1;
        levelInfo = LevelInfo.populateGameData();
        levelStarted = false;
        levelLost = false;
        lives = 3;
        endingTurn = false;
        score = 0;
        UpdateUI();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButton("Fire1") && levelStarted == false && !endingTurn)
        {
            Debug.Log("Level started.");
            StartLevel();
        }

		if (levelStarted && !levelLost)
        {
            BombSpawner();
        }
        if (bombsRemaining <= 0 && levelStarted)
        {
            robber.StopMoving();
            if (bombs.Count == 0)
            {
                FinishLevel();
            }
        }
	}

    public void UpdateUI()
    {
        scoreText.text = score.ToString();
        levelText.text = currentLevel.ToString();
        livesText.text = lives.ToString();
    }

    void BombSpawner()
    {
        // the bomb spawner should utilize the level info to spawn bombs until the bomb count
        // is reached
        timeElapsed += Time.deltaTime;
        if (timeElapsed > bombDelay && bombsRemaining > 0)
        {
            bombsRemaining -= 1;
            SpawnBomb();
            timeElapsed = 0.0f;
        }
    }

    void StartLevel()
    {
        // after pushing the main button, the level should start
        // initialize all variables, and turn on various mechanics -
        // robber should begin moving, bombs should start falling
        if (levelLost)
        {
            currentLevel -= 1;
            lives -= 1;
            if (currentLevel < 1)
            {
                currentLevel = 1;
            }
            levelLost = false;
            if (lives < 0)
            {
                Debug.Log("Game Over - all lives lost!");
            }
        }
        timeElapsed = 0.0f;
        currentLevelInfo = LevelInfo.getLevelInfo(currentLevel, levelInfo);
        bombsRemaining = currentLevelInfo.bombCount;
        bombDelay = currentLevelInfo.bombDelay;
    
        // turn on various mechanics
        levelStarted = true;
        robber.SetLevelModifier(currentLevel);
        UpdateUI();
        robber.StartLevel();

    }

    void FinishLevel()
    {
        Debug.Log("Level finished.");
        levelStarted = false;
        timeElapsed = 0.0f;
        currentLevel += 1;

    }

    void SpawnBomb()
    {
        // a bomb should be spawned at the bomb spawn location
        GameObject bomb = Instantiate(bombPrefab, spawnPoint.transform.position, bombPrefab.transform.rotation);
        bomb.GetComponent<BombController>().SetLevel(currentLevel);
        bombs.Add(bomb);
    }

    public void RemoveBomb(GameObject bomb)
    {
        audioSource.clip = caughtSound;
        audioSource.Play();
        bombs.Remove(bomb);
    }

    public void EndTurn()
    {
        // a bomb has hit the ground. need to explode remaining bombs
        // all the way up, stop the player and robber and bomb spawns, then restart
        // the level
        endingTurn = true;
        // stop the player movement
        player.StopMovement();

        // stop the robber movement
        robber.StopMoving();
        // stop bomb spawns
        levelLost = true;

        // explode bombs remaining, from bottom to top
        foreach (GameObject bomb in bombs)
        {
            BombController bombController = bomb.GetComponent<BombController>();
            bombController.StopMovement();
            bombController.Explode();
            audioSource.clip = explosionSound;
            audioSource.Play();
        }
        Debug.Log("Exploding all bombs");

        // wait for 1 second and give player their controls back
        StartCoroutine(ResumeGame());
    }

    IEnumerator ResumeGame()
    {
        yield return new WaitForSeconds(2);
        endingTurn = false;
        player.StartMovement();
    }

    public void ScoreBomb()
    {
        score += currentLevel;
        scoreText.text = score.ToString();
    }
}
