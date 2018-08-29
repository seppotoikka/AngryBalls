using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Information on using attributes see https://docs.unity3d.com/Manual/Attributes.html
[RequireComponent(typeof(Collider2D))]
public class Ball : MonoBehaviour {

	public enum States { idle, armed, launched }
	public States state = States.idle;

	public Collider2D ballCollider;
    public new Rigidbody2D rigidbody2D;

    private SpriteRenderer spriteRenderer;

    // Cache references to components to avoid using slow GetComponent call during gameplay
	void Start () {
		ballCollider = GetComponent<Collider2D>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
	}
	
	// Destroy ball after launch if it has stopped moving or has moved off screen
	void Update () {
		if (state == States.launched && (rigidbody2D.IsSleeping() || !spriteRenderer.isVisible))
        {
            GameManager.Instance.BallDestroyed(this);
            Destroy(gameObject);
        }
	}

    //moves to slingshot and returns true if current state is idle, otherwise retuns false
	public bool JumpToSlingshot(Vector3 position)
	{
		if (state == States.idle) {
			transform.position = position;
			state = States.armed;
			return true;
		}
		return false;
	}

    //change ball rigidbody from kinematic to dynamic, add launch force and change state
	public void Launch(Vector2 launchVector)
	{
        rigidbody2D.isKinematic = false;
        rigidbody2D.AddRelativeForce(launchVector, ForceMode2D.Impulse);
        state = States.launched;
	}
		
}
