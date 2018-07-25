using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobberController : MonoBehaviour {

    public bool isMoving = false;
    public float movingToX;
    public float randomMoveSpeed;
    public float levelModifier;

	// Use this for initialization
	void Start () {
        GetNewTarget();
	}
	
	// Update is called once per frame
	void Update () {
		if (isMoving == true)
        {
            MoveToTarget();
            if(Mathf.Abs(this.transform.position.x - movingToX) < 0.3)
            {
                GetNewTarget();
            }
        }
	}

    public void SetLevelModifier(float modifier)
    {
        levelModifier = modifier;
    }

    public void StartLevel()
    {
        GetNewTarget();
        isMoving = true;
    }

    public void StopMoving()
    {
        isMoving = false;
    }

    void GetNewTarget()
    {
        // set a new randomMoveTime
        randomMoveSpeed = Random.Range(5, 15) * levelModifier;
        // set a new spot to move to
        movingToX = Random.Range(-12, 12);
    }

    void MoveToTarget()
    {
        Vector3 currentPosition = this.transform.position;
        if (currentPosition.x > movingToX) // need to subtract our speed
        {
            currentPosition.x -= randomMoveSpeed * Time.deltaTime;
        } else
        {
            currentPosition.x += randomMoveSpeed * Time.deltaTime;
        }
        this.transform.SetPositionAndRotation(currentPosition, this.transform.rotation);
    }
}
