using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AFlocking : ActionNode
{
   public override void Execute(Boid npc)
    {
        npc.Flocking();
    }
}
