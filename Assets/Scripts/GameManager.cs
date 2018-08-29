using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    /*  An extremely simple implementation of the Singleton design pattern 
        This means we ensure a class has only one instance, and provide a 
        global point of access to it, setting the reference in Awake. 
        
        For another implementation see 
        https://unity3d.com/learn/tutorials/projects/2d-roguelike-tutorial/writing-game-manager

        For an in-depth explanation of the design pattern, see
        http://gameprogrammingpatterns.com/singleton.html 
    */
    public static GameManager Instance;

	public enum States { idle, slingArmed, slingAiming, ballReleased }
	public States state;

	private List<Ball> balls;
    private List<Baddie> baddies;
	private Slingshot slingshot;
	private Camera mainCamera;

	void Awake () {
        if (Instance != null && Instance != this)
        {
            Debug.LogError("There is already an instance of GameManager!");
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
		
        // Create a list of all balls & baddies in the scene to keep track of them
		balls = new List<Ball>(FindObjectsOfType<Ball>());
        baddies = new List<Baddie>(FindObjectsOfType<Baddie>());

        /* Cache reference to the slingshot
           In general it is a good practice whenever possible to use 
           FindObjects -calls only once and cache the results to improve performance */
        slingshot = FindObjectOfType<Slingshot> ();

        // Camera.main uses FindObjectsWithTag and is not cached internally, so it is very slow
        // We will cache the reference to avoid using the call during gameplay
		mainCamera = Camera.main;
	}

    // Called from InputController each frame if there is player input
	public void ProcessInput (Vector2 touchPosition, TouchPhase touchPhase) 
	{
        // Convert player input from screen coordinates to game world coordinates
		Vector2 positionInWorldSpace = mainCamera.ScreenToWorldPoint (touchPosition);

        // Choose a method to process the input depending on current game state
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
                // If player clicked on an idle ball, load it into the slingshot
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
                // If player clicked on the ball currently loaded in the slingshot, start aiming
                if (ball.state == Ball.States.armed && ball.ballCollider.bounds.Contains(positionInWorldSpace))
                {
                    state = States.slingAiming;
                }
            }
        }
    }

    private void ProcessAimingPhaseInput(Vector2 positionInWorldSpace, TouchPhase touchPhase)
    {
        // While the player is holding down the button/finger, update slingshot ball position
        if (touchPhase == TouchPhase.Moved)
        {
            slingshot.Aim(positionInWorldSpace);
        }
        else
        {
            // Shoot the ball when player releases button
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

        /*  If there are no more balls or baddies left, process game ending, 
            else return idle state and wait for player to choose next ball */
        if (balls.Count == 0 || baddies.Count == 0)
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
