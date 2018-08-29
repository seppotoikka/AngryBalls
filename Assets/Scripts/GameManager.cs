using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public static GameManager Instance;

	public enum States { idle, slingArmed, slingAiming, ballReleased }
	public States state;

	private List<Ball> balls;
    private List<Baddie> baddies;
	private Slingshot slingshot;
	private Camera mainCamera;

	void Awake () {
		Instance = this;
		balls = new List<Ball>(FindObjectsOfType<Ball>());
        baddies = new List<Baddie>(FindObjectsOfType<Baddie>());
		slingshot = FindObjectOfType<Slingshot> ();
		mainCamera = Camera.main;
	}

	public void ProcessInput (Vector2 touchPosition, TouchPhase touchPhase) 
	{
		Vector2 positionInWorldSpace = mainCamera.ScreenToWorldPoint (touchPosition);

		switch (state) {

		case States.idle:
			ProcessIdlePhaseInput (positionInWorldSpace, touchPhase);
			break;

		case States.slingArmed:
            ProcessArmedPhaseInput(positionInWorldSpace, touchPhase);
			break;

		case States.slingAiming:
            ProcessAimingPhaseInput(positionInWorldSpace, touchPhase);               
			break;

		default:
			break;
		}
	}

    private void ProcessIdlePhaseInput(Vector2 positionInWorldSpace, TouchPhase touchPhase)
	{
		if (touchPhase == TouchPhase.Began) 
		{
			foreach (Ball ball in balls) 
			{
				if (ball.state == Ball.States.idle && ball.ballCollider.bounds.Contains (positionInWorldSpace)) 
				{
					if (slingshot.LoadSlingshot (ball)) 
					{
						state = States.slingArmed;
					}
				}
			}
		}
	}

    private void ProcessArmedPhaseInput(Vector2 positionInWorldSpace, TouchPhase touchPhase)
    {
        if (touchPhase == TouchPhase.Began)
        {
            foreach (Ball ball in balls)
            {
                if (ball.state == Ball.States.armed && ball.ballCollider.bounds.Contains(positionInWorldSpace))
                {
                    state = States.slingAiming;
                }
            }
        }
    }

    private void ProcessAimingPhaseInput(Vector2 positionInWorldSpace, TouchPhase touchPhase)
    {
        if (touchPhase == TouchPhase.Moved)
        {
            slingshot.Aim(positionInWorldSpace);
        }
        else
        {
            if (touchPhase == TouchPhase.Ended)
            {
                slingshot.Shoot();
                state = States.ballReleased;
            }
        }
    }

    public void BallDestroyed(Ball ball)
    {
        balls.Remove(ball);

        if (balls.Count == 0)
        {
            if (baddies.Count == 0)
            {
                Debug.Log("You win!");
            }
            else
            {
                Debug.Log("You lose!");
            }
        }
        else
        {
            state = States.idle;
        }
    }

    public void BaddieDestroyed(Baddie baddie)
    {
        baddies.Remove(baddie);
    }
}
