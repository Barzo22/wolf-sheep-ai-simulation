using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AEvade : ActionNode
{
    public float predictionTime = 1.5f;

    public override void Execute(Boid npc)
    {
        Hunter hunter = GameObject.FindObjectOfType<Hunter>();

        if (hunter != null)
        {
            Vector3 futurePos = hunter.transform.position + hunter.Velocity * predictionTime;
            futurePos.y = npc.transform.position.y; 

    
            Vector3 fleeDir = npc.transform.position - futurePos;
            fleeDir.y = 0; 
            fleeDir.Normalize();

            Vector3 desired = fleeDir * npc._maxVelocity;

            Vector3 steering = desired - npc.Velocity;
            steering = Vector3.ClampMagnitude(steering, npc._maxForce);

            npc.AddForce(steering);
        }
    }
}

