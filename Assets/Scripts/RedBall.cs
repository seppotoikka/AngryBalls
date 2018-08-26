using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedBall : Ball {

    private float timeSinceStopped;

    private void Update()
    {
        if (status == Status.launched && rb2D.IsSleeping())
        {
            timeSinceStopped += Time.deltaTime;
            if (timeSinceStopped > 1)
            {
                manager.BallDestroyed(this);
                Destroy(gameObject);
            }
        }
    }

    public override void Launch(Vector2 launchVector)
    {
        base.Launch(launchVector);

    }
}
