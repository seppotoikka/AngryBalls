using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//prevent errors
[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]

public abstract class Ball : MonoBehaviour {

    public enum Status { idle, armed, launched }   
    public Status status;

    [HideInInspector]
    public Collider2D ballCollider;

    [HideInInspector]
    public Rigidbody2D rb2D;

    protected GameManager manager;

    protected virtual void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
        ballCollider = GetComponent<Collider2D>();
    }

    public void SetManager(GameManager manager)
    {
        this.manager = manager;
    }

    public virtual void JumpToSling(Transform sling)
    {
        rb2D.MovePosition(sling.position);
        status = Status.armed;
    }

    public virtual void Launch(Vector2 launchVector)
    {
        Debug.Log("Launch " + launchVector);
        rb2D.isKinematic = false;
        rb2D.AddRelativeForce(launchVector * 1000);
        status = Status.launched;
    }

    
}
