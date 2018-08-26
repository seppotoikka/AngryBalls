using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStates {

    public enum State { idle, slingArmed, slingAiming, ballReleased, gameOver }

    public static void ProcessGameStateInput(GameManager manager, Vector2 touchPos, TouchPhase touchPhase)
    {
        switch (manager.gameState)
        {
            case State.idle:
                ProcessIdlePhase(manager, touchPos, touchPhase);
                break;

            case State.slingArmed:
                ProcessArmedPhase(manager, touchPos, touchPhase);
                break;

            case State.slingAiming:
                ProcessAimingPhase(manager, touchPos, touchPhase);
                break;

            default:
                break;
        }
    }

    static void ProcessIdlePhase(GameManager manager, Vector2 touchPos, TouchPhase touchPhase)
    {
        //check if the touch selects a ball
        if (touchPhase == TouchPhase.Began)
        {
            foreach (Ball ball in manager.balls)
            {
                Debug.Log(touchPos + ";" + ball.transform.position + ";" + ball.ballCollider.bounds.Contains(touchPos));
                //if an idle ball is selected, tell it to jump to slingshot and change the game state
                if (ball.status == Ball.Status.idle && 
                    ball.ballCollider.bounds.Contains(touchPos))
                {
                    //if ball is succesfully placed in slingshot, transition to slingArmed state
                    if (manager.slingshot.LoadSlingshot(ball))
                    {
                        manager.gameState = State.slingArmed;
                    }
                    return;
                }
            }
        }
    }

    static void ProcessArmedPhase(GameManager manager, Vector3 touchPos, TouchPhase touchPhase)
    {
        if (touchPhase == TouchPhase.Began)
        {
            foreach (Ball ball in manager.balls)
            {
                if (ball.status == Ball.Status.armed && 
                    ball.ballCollider.bounds.Contains(touchPos))
                {
                    manager.gameState = State.slingAiming;
                    manager.slingshot.UpdateSlingshotPosition(touchPos);
                }
            }
        }               
    }

    static void ProcessAimingPhase(GameManager manager, Vector2 touchPos, TouchPhase touchPhase)
    {
        if (touchPhase == TouchPhase.Moved)
        {
            manager.slingshot.UpdateSlingshotPosition(touchPos);
        }
        else
        {
            if (touchPhase == TouchPhase.Ended)
            {
                if (manager.slingshot.ReleaseBall(touchPos))
                {
                    manager.gameState = State.ballReleased;
                }
                else
                {
                    manager.gameState = State.slingArmed;
                }
            }
        }
    }
}
