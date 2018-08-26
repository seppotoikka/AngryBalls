using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlingshotBallHolder : MonoBehaviour {

    public Transform backStringBase;
    public Transform frontStringBase;
    public Slingshot slingshot;

    public Vector3 stringBaseCenter;
    private Vector3 restingPosition;

	// Use this for initialization
	void Start () {
        stringBaseCenter = frontStringBase.position + (backStringBase.position - frontStringBase.position) * 0.5f;
        restingPosition = transform.position;
	}

    public void BallLoaded(Ball ball)
    {
        transform.position -= new Vector3(0, ball.ballCollider.bounds.extents.y, 0);
    }
	
	public void UpdatePosition(Ball ball)
    {
        transform.position = ball.transform.position + (ball.transform.position - stringBaseCenter).normalized * ball.ballCollider.bounds.extents.x; 
    }

    public void ResetPosition()
    {
        transform.position = restingPosition;
    }
}
