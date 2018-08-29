using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slingshot : MonoBehaviour {

	public Transform stringsEndPoint;

	private Ball currentlyLoadedBall;

	private LineRenderer[] lineRenderers;

	private Vector2 aimPoint;

	void Awake(){
		lineRenderers = GetComponentsInChildren<LineRenderer> ();
		aimPoint = (lineRenderers [0].transform.position + lineRenderers [1].transform.position) / 2;
		UpdateRubberBands ();
	}

	void Update(){
		if (currentlyLoadedBall != null) {
			//stringsEndPoint.position = currentlyLoadedBall.transform.position;
			UpdateEndPoint ();
			UpdateRubberBands ();
		}
		
	}

	public bool LoadSlingshot(Ball ball){
		if (currentlyLoadedBall == null && ball.JumpToSlingshot(stringsEndPoint.position)) {
			currentlyLoadedBall = ball;
			return true;
		}
		return false;
	}

	private void UpdateRubberBands(){
		foreach (LineRenderer lineRenderer in lineRenderers) {
			lineRenderer.SetPosition (1, stringsEndPoint.localPosition - lineRenderer.transform.localPosition);
		}
	}

	private void UpdateEndPoint(){
		Vector2 vectorFromAimPoint = (Vector2) currentlyLoadedBall.transform.position - aimPoint;
		stringsEndPoint.transform.position = aimPoint + vectorFromAimPoint + 
			vectorFromAimPoint.normalized * currentlyLoadedBall.ballCollider.bounds.extents.x;
	}
}
