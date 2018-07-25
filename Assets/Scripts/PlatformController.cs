using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour {

    public float PlatformSpeed = 2.0F;
    public float MaxSpeed = 1.5f;
    public bool canMove;

	// Use this for initialization
	void Start () {
        canMove = true;
	}
	
	// Update is called once per frame
	void Update () {
        CheckForMouseInput();
	}

    public void StopMovement()
    {
        canMove = false;
    }

    public void StartMovement()
    {
        canMove = true;
    }

    private void CheckForMouseInput()
    {
        if (canMove)
        {
            float h = PlatformSpeed * Input.GetAxis("Mouse X");
            if (h > MaxSpeed)
            {
                h = MaxSpeed;
            }
            else if (h < -MaxSpeed)
            {
                h = -MaxSpeed;
            }
            // move the platform based on horizontal mouse movement
            Vector3 currentPos = this.transform.position;
            currentPos.x = currentPos.x - h;
            if (currentPos.x > 13)
            {
                currentPos.x = 13;
            }
            else if (currentPos.x < -13)
            {
                currentPos.x = -13;
            }
            this.transform.SetPositionAndRotation(currentPos, this.transform.rotation);
            
        }
    }
}
