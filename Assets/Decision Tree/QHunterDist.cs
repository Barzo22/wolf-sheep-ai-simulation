using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QHunterDist : QuestionNode
{

    public override void Execute(Boid npc)
    {
  
        Hunter hunter = FindObjectOfType<Hunter>();

        if (hunter != null)
        {
            float dist = Vector3.Distance(npc.transform.position, hunter.transform.position);
            if (dist <= npc._radiusHunter)
            {
                trueNode.Execute(npc);
                return;
            }
        }

        falseNode.Execute(npc);
    }
}
