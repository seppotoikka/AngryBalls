using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]

public class Baddie : MonoBehaviour {

    public float health;

    private GameManager manager;

    [HideInInspector]
    public Rigidbody2D rb2D;
    [HideInInspector]
    public Collider2D baddieCollider;

    private void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
        baddieCollider = GetComponent<Collider2D>();
    }

    public void SetManager(GameManager manager)
    {
        this.manager = manager;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        float damage = Mathf.Abs(Vector2.Dot(collision.relativeVelocity, collision.contacts[0].point - rb2D.position));
        TakeDamage(damage);
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0)
        {
            manager.BaddieDestroyed(this);
            Destroy(gameObject);
        }
    }
}
