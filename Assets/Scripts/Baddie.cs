using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class Baddie : MonoBehaviour {

    [Header("Attributes")]
    [Range(1, 10)]
    public float health = 1;

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
