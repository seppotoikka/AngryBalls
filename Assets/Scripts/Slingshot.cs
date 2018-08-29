using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slingshot : MonoBehaviour {

    // Information on using attributes see https://docs.unity3d.com/Manual/Attributes.html
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
        /* Cache references to line renderer components to avoid using expensive 
           GetComponentsInChildren method call many times */
		lineRenderers = GetComponentsInChildren<LineRenderer> ();
        /* Calculate aim point, e.g. the point halfway between 
           the points where rubber band is attached to the slingshot */
		aimPoint = (lineRenderers [0].transform.position + lineRenderers [1].transform.position) / 2;
        // Store initial rubber band end point position for later use
        idleStringsEndPointPosition = stringsEndPoint.position;
        // Set the rubber band line renderers to initial position
        UpdateRubberBands ();
	}

    // Called when the player has selected a ball to be loaded to the slingshot, 
    // Returns true if loading was succesfull (if there wasn't already a ball in the slingshot)
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

        // Update the Line Renderers' line end position
		foreach (LineRenderer lineRenderer in lineRenderers) {
			lineRenderer.SetPosition (1, stringsEndPoint.localPosition - lineRenderer.transform.localPosition);
		}
	}

    // Wrap rubber bands "around the ball" by setting the stringsEndPoint on the ball edge opposite the aimPoint
    // If there is no ball loaded, reset the stringEndPoint
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

    // Update the position of the loaded ball according to the player touch input position
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

    // Called when the player releases mouse button (or ends touching the screen) while aiming
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
