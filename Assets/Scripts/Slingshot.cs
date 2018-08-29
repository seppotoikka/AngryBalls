using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slingshot : MonoBehaviour {

    [Header("Values")]
    [Range(2, 4)]
    public float maxStringLength = 2;
    [Range(1, 10)]
    public float slingshotForce = 1;
    [Space]
	public Transform stringsEndPoint;

	private Ball currentlyLoadedBall;

	private LineRenderer[] lineRenderers;

	private Vector2 aimPoint;
    private Vector2 idleStringsEndPointPosition;

	void Awake(){
        /*cache references to line renderer components to avoid using expensive 
         * GetComponentsInChildren method call many times*/
		lineRenderers = GetComponentsInChildren<LineRenderer> ();
        /*calculate aim point, e.g. the point halfway between 
         * the points where rubber band is attached to the slingshot*/
		aimPoint = (lineRenderers [0].transform.position + lineRenderers [1].transform.position) / 2;
        //store initial rubber band end point position for later use
        idleStringsEndPointPosition = stringsEndPoint.position;
        //set the rubber band line renderers to initial position
        UpdateRubberBands ();
	}

    //called when the player has selected a ball to be loaded to the slingshot, 
    //returns true if loading was succesfull (if there wasn't already a ball in the slingshot)
	public bool LoadSlingshot(Ball ball){
		if (currentlyLoadedBall == null && ball.JumpToSlingshot(stringsEndPoint.position)) {
			currentlyLoadedBall = ball;
            UpdateRubberBands();
			return true;
		}
		return false;
	}

	private void UpdateRubberBands(){
        UpdateEndPoint();

		foreach (LineRenderer lineRenderer in lineRenderers) {
			lineRenderer.SetPosition (1, stringsEndPoint.localPosition - lineRenderer.transform.localPosition);
		}
	}

	private void UpdateEndPoint(){
        if (currentlyLoadedBall == null)
        {
            stringsEndPoint.transform.position = idleStringsEndPointPosition;
        }
        else
        {
            Vector2 vectorFromAimPoint = (Vector2)currentlyLoadedBall.transform.position - aimPoint;
            stringsEndPoint.transform.position = aimPoint + vectorFromAimPoint +
                vectorFromAimPoint.normalized * currentlyLoadedBall.ballCollider.bounds.extents.x;
        }
	}

    //update the position of the loaded ball according to the player touch input position
    public void Aim(Vector2 touchPosition)
    {
        if (currentlyLoadedBall != null)
        {
            Vector2 aimVector = touchPosition - aimPoint;

            if (aimVector.magnitude > maxStringLength)
            {
                aimVector = aimVector.normalized * maxStringLength;
            }
            currentlyLoadedBall.rigidbody2D.position = aimPoint + aimVector;

            UpdateRubberBands();
        }
    }

    //called when the player releases mouse button (or ends touching the screen) while aiming
    public void Shoot()
    {
        if (currentlyLoadedBall != null)
        {
            Vector2 launchVector = aimPoint - currentlyLoadedBall.rigidbody2D.position;

            currentlyLoadedBall.Launch(launchVector * slingshotForce);
            
            currentlyLoadedBall = null;

            UpdateRubberBands();
        }
    }
}
