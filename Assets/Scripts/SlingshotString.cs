using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class SlingshotString : MonoBehaviour {

    public Transform ballEnd;

    private LineRenderer lineRenderer;

	// Use this for initialization
	void Start () {
        lineRenderer = GetComponent<LineRenderer>();
	}
	
	void LateUpdate () {
        lineRenderer.SetPosition(1, ballEnd.position - transform.position);
	}
}
