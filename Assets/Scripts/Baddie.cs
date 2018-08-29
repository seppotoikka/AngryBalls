using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class Baddie : MonoBehaviour {

    [Header("Attributes")]
    [Range(1, 10)]
    public float health = 1;

    /* OnCollisionEnter2D is called by Unity Physics 2D whenever another collider 
     * comes into contact with a collider attached to this GameObject.
     * This method is called only in the first physics update after the collision,
     * if the objects continue collision, OnCollisionStay2D will be called instead.
     * A more sophisticated version of collision damage would take this into account 
     * by implementing a treshold to TakeDamage amount and calling TakeDamage also 
     * in OnCollisionStay2D */
    private void OnCollisionEnter2D(Collision2D collision)
    {
        TakeDamage(collision.relativeVelocity.magnitude);
    }

    private void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0)
        {
            GameManager.Instance.BaddieDestroyed(this);
            Destroy(gameObject);
        }
    }
}
