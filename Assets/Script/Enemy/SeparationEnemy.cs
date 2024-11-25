using UnityEngine;
using UnityEngine.Serialization;

public class SeparationEnemy : MonoBehaviour
{
    [FormerlySerializedAs("_distanceBetweenEnemy")] [SerializeField] private int distanceBetweenEnemy = 100;
    [SerializeField] private float repuls = 0.5f;

    // Update is called once per frame
    void Update()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Ennemie");

        foreach (GameObject enemy in enemies)
        {
            if (enemy != gameObject) // Ignore lui-même
            {
                Vector3 direction = transform.position - enemy.transform.position;
                float distance = direction.magnitude;

                if (distance < distanceBetweenEnemy) // Si trop proche
                {
                    // Calcul d'une force de répulsion
                    Vector3 repulsionForce = direction.normalized * ((distanceBetweenEnemy - distance) * repuls);
                    transform.position += repulsionForce * Time.deltaTime;
                }
            }
        }
    }
}
