using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
  
    public Slingshot slingshot;
    
    public GameStates.State gameState;

    [HideInInspector]
    public List<Ball> balls;
    List<Baddie> baddies;
    List<BuildingBlock> buildingBlocks;
    [HideInInspector]
    public Camera mainCamera;

    private void Start()
    {
        balls = new List<Ball>(FindObjectsOfType<Ball>());
        foreach (Ball ball in balls)
        {
            ball.SetManager(this);
        }

        baddies = new List<Baddie>(FindObjectsOfType<Baddie>());
        foreach (Baddie baddie in baddies)
        {
            baddie.SetManager(this);
        }

        buildingBlocks = new List<BuildingBlock>(FindObjectsOfType<BuildingBlock>());
        foreach (BuildingBlock block in buildingBlocks)
        {
            block.SetManager(this);
        }

        gameState = GameStates.State.idle;
        mainCamera = Camera.main;
    }

    public void ProcessInput(Vector2 touchPos, TouchPhase touchPhase)
    {
        GameStates.ProcessGameStateInput(this, mainCamera.ScreenToWorldPoint(touchPos), touchPhase);
    }  

    public void BallDestroyed(Ball ball)
    {
        balls.Remove(ball);

        //check if all balls have been launched
        if (balls.Count == 0)
        {
            //win or lose?
            if (baddies.Count > 0)
            {
                //you lose
                Debug.Log("You lose!");
            }
            else
            {
                //you win
                Debug.Log("You win!");
            }
        }
    }

    public void BaddieDestroyed(Baddie baddie)
    {
        baddies.Remove(baddie);
    }

    public void CreateExplosion(Vector2 explosionPosition, float force, float radius)
    {
        foreach(Baddie baddie in baddies)
        {
            //check if baddie is within explosion radius
            Vector2 baddieClosestPoint = baddie.baddieCollider.bounds.ClosestPoint(explosionPosition);
            Vector2 explosionToBaddieVector = baddieClosestPoint - explosionPosition;
            float distanceFromExplosion = explosionToBaddieVector.magnitude;
            if (distanceFromExplosion < radius)
            {
                float appliedForce = (1 - distanceFromExplosion / radius) * force;
                baddie.rb2D.AddForceAtPosition(explosionToBaddieVector.normalized * appliedForce, explosionPosition);
                baddie.TakeDamage(appliedForce);
            }
        }

        foreach(BuildingBlock block in buildingBlocks)
        {

        }
    }
}
