using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombController : MonoBehaviour {

    public float level;
    public float baseSpeed;
    public float speedChangePerLevel;
    public AudioClip caughtSound;
    public bool isMoving;

	// Use this for initialization
	void Start () {
        isMoving = true;
	}
	
	// Update is called once per frame
	void Update () {
        if (isMoving)
        {
            MoveDown();
        }
	}

    void MoveDown()
    {
        // bomb needs to move down at a consistent speed
        Vector3 currentPosition = this.transform.position;
        currentPosition.y = currentPosition.y - ((baseSpeed + (speedChangePerLevel * level * 2)) * Time.deltaTime);
        this.transform.SetPositionAndRotation(currentPosition, this.transform.rotation);
    }

    public void SetLevel(int level)
    {
        this.level = level;
    }

    public void StopMovement()
    {
        isMoving = false;
    }

    public void Explode()
    {
        StartCoroutine(EndBomb());
    }

    IEnumerator EndBomb()
    {
        yield return new WaitForSeconds(1);
        GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().RemoveBomb(this.gameObject);
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            // the bomb was caught, destroy it safely and score the player
            Debug.Log("Bomb Caught");

            // Score the player
            GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().ScoreBomb();

            // Destroy this game object
            GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().RemoveBomb(this.gameObject);
            Destroy(gameObject);
        }
        else if (other.tag == "Ground")
        {
            // the bomb made it to the ground, this turn is over
            Debug.Log("Bomb exploded...");
            // tell the game controller that a bomb has hit the ground, it can then control
            // blowing up each bomb all the way up, stopping the player and robber and bomb spawns
            GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().EndTurn();
        }
    }
}
