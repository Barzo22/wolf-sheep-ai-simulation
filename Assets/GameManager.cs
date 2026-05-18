using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public List<Boid> boids = new List<Boid>();
    [Range(0f, 1f)] public float weightSeparation;
    [Range(0f, 1f)] public float weightAligment;
    [Range(0f, 1f)] public float weightCohesion;

    private void Awake()
{
    if (Instance == null)
    {
        Instance = this;
        boids = new List<Boid>();
    }

}
}
