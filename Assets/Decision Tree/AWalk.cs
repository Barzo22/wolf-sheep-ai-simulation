using UnityEngine;

public class AWalk : ActionNode
{
    private Vector3 currentDir = Vector3.zero;
    private float timer = 0f;

    public override void Execute(Boid npc)
    {
         currentDir = new Vector3( Random.Range(-1f, 1f),0f, Random.Range(-1f, 1f)).normalized;

         npc.AddForce(currentDir * npc._maxForce);
    }
}
