using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QFoodDist : QuestionNode
{
    public float radiusArrive = 15f;

    public override void Execute(Boid npc)
    {
        Food nearestFood = Food.FindNearestFood(npc.transform.position, radiusArrive);

        if (nearestFood != null)
        {
            trueNode.Execute(npc); 
        }
        else
        {
            falseNode.Execute(npc);
        }
    }
}

