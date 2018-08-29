using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Ball : MonoBehaviour {

	public enum States { idle, armed, launched }
	public States state = States.idle;

	public Collider2D ballCollider;
    public new Rigidbody2D rigidbody2D;

    private SpriteRenderer spriteRenderer;
	// Use this for initialization
	void Start () {
		ballCollider = GetComponent<Collider2D>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		if (state == States.launched && (rigidbody2D.IsSleeping() || !spriteRenderer.isVisible))
        {
            GameManager.Instance.BallDestroyed(this);
            Destroy(gameObject);
        }
	}

	public bool JumpToSlingshot(Vector3 position)
	{
		if (state == States.idle) {
			transform.position = position;
			state = States.armed;
			return true;
		}
		return false;
	}

	public void Launch(Vector2 launchVector)
	{
        rigidbody2D.isKinematic = false;
        rigidbody2D.AddRelativeForce(launchVector, ForceMode2D.Impulse);
        state = States.launched;
	}
		
}
