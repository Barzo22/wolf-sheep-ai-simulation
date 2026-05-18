    using UnityEngine;

    public class Food : MonoBehaviour
    {
        [SerializeField] private float eatTime = 2f;
        [SerializeField] private float eatDistance = 1.5f; 
        private bool isBeingEaten = false;
        private float timer;
        Boid boid;

        private void Update()
        {
            if (!isBeingEaten)
            {
                Boid[] boids = FindObjectsOfType<Boid>();
                foreach (Boid boid in boids)
                {
                    float dist = Vector3.Distance(transform.position, boid.transform.position);
                    if (dist <= eatDistance)
                    {
                        isBeingEaten = true;
                        timer = eatTime;
                    
                        break;
                    }
                }
            }
            else
            {
                timer -= Time.deltaTime;
            
                if (timer <= 0f)
                {         
                    Destroy(gameObject);
                }
            }
        }

        public static Food FindNearestFood(Vector3 pos, float _radiusArrive)
        {
            Food[] allFood = FindObjectsOfType<Food>();
            Food nearest = null;
            float minDist = float.MaxValue;

            foreach (Food f in allFood)
            {
                float dist = (f.transform.position - pos).sqrMagnitude;

                if (dist > _radiusArrive * _radiusArrive) continue;

                if (dist < minDist)
                {
                    minDist = dist;
                    nearest = f;
                }
            }

            return nearest;
        }
    }

