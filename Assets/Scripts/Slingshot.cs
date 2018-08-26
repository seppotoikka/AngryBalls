using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slingshot : MonoBehaviour {

    public Transform backString;
    public Transform frontString;
    public SlingshotBallHolder ballHolder;
    [Space]
    public float maxLength;
    public float maxForce;

    Ball currentBall = null;

    private Vector2 slingshotBasePoint;

    private void Awake()
    {
        slingshotBasePoint = (frontString.position + backString.position) / 2;
    }

    public bool IsArmed
    {
        get
        {
            return (currentBall != null);
        }
    }

    public bool LoadSlingshot(Ball ball)
    {
        if (currentBall == null)
        {
            currentBall = ball;
            ball.JumpToSling(ballHolder.transform);
            ballHolder.BallLoaded(ball);
            return true;
        }

        return false;
    }

    public void UpdateSlingshotPosition(Vector2 touchPos)
    {
        if (currentBall != null)
        {
            transform.localPosition = GetSlingshotVector(touchPos);
            currentBall.rb2D.MovePosition(transform.position);
            ballHolder.UpdatePosition(currentBall);
        }
    }

    public bool ReleaseBall(Vector2 touchPos)
    {
        if (currentBall != null)
        {
            //launch ball
            Vector2 slingshotVector = GetSlingshotVector(currentBall.transform.position);
            float forceMultiplier = slingshotVector.magnitude / maxLength * maxForce;
            currentBall.Launch(slingshotVector * -1 * forceMultiplier);
            Debug.Log("Touch pos: " + touchPos + "; SlingshotPos: " + slingshotBasePoint + "Vector: " + GetSlingshotVector(touchPos));
            //return slingshot to resting position
            transform.localPosition = Vector3.zero;
            ballHolder.ResetPosition();
            return true;
        }
        else
        {
            return false;
        }
    }

    private Vector2 GetSlingshotVector(Vector2 touchPos)
    {
        Vector2 vectorFromSlingshotBase = touchPos - slingshotBasePoint;
        float distanceMagnitude = vectorFromSlingshotBase.magnitude;
        float clampedDistanceMagnitude = Mathf.Clamp(distanceMagnitude, 0, maxLength);

        return vectorFromSlingshotBase.normalized * clampedDistanceMagnitude;
    }
}
