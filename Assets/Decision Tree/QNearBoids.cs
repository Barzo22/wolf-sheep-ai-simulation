using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QBoidDist : QuestionNode
{
    public override void Execute(Boid npc)
    {
        Boid[] allBoids = FindObjectsOfType<Boid>();
        bool othersNear = false;

        foreach (Boid b in allBoids)
        {
            if (b != npc)
            {
                float dist = Vector3.Distance(npc.transform.position, b.transform.position);
                if (dist <= npc._radiusDetect)
                {
                    othersNear = true;
                    break;
                }
            }
        }
        if (othersNear)
        {
            trueNode.Execute(npc); 
        }
        else
        {
            falseNode.Execute(npc); 
        }
    }
}

