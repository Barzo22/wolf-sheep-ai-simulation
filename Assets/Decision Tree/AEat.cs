using UnityEngine;

public class AEat : ActionNode
{
    public override void Execute(Boid npc)
    {
        Food nearestFood = Food.FindNearestFood(npc.transform.position, npc._radiusFood);

        if (nearestFood != null)
        {
            Vector3 steering = npc.Arrive(nearestFood.transform.position);

            npc.AddForce(steering);

            Debug.DrawLine(npc.transform.position, nearestFood.transform.position, Color.yellow);
        }
    }
}

