using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingBlock : MonoBehaviour {

    public bool isDestructible;
    public float health;

    public Rigidbody2D rb2D;
    public Collider2D blockCollider2D;

    private GameManager manager;

    public void SetManager(GameManager manager)
    {
        this.manager = manager;
    }

    private void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
        blockCollider2D = GetComponent<Collider2D>();
    }



}
