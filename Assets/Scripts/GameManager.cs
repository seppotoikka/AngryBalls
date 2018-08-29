using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public static GameManager Instance;

	public enum States { idle, slingArmed, slingAiming, ballReleased }
	public States state;

	private List<Ball> balls;
	private Slingshot slingshot;
	private Camera mainCamera;

	void Awake () {
		Instance = this;
		balls = new List<Ball>(FindObjectsOfType<Ball>());
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
			break;

		case States.slingAiming:
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
}
