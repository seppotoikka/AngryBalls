using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Ball : MonoBehaviour {

	public enum States { idle, armed, launched }
	public States state = States.idle;

	public Collider2D ballCollider;

	// Use this for initialization
	void Start () {
		ballCollider = GetComponent<Collider2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public bool JumpToSlingshot(Vector3 position)
	{
		if (state == States.idle) {
			GetComponent<Rigidbody2D>().MovePosition (position);
			state = States.armed;
			return true;
		}
		return false;
	}

	public void Launch(Vector2 launchVector)
	{
	}
		
}
