using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private CircleCollider2D detectionZone; // Collider para el área de detección.
    [SerializeField] private float moveSpeed = 4f; // Velocidad de movimiento hacia el jugador.
    [SerializeField] private float returnSpeed = 3f; // Velocidad de regreso a la posición inicial.

    private Transform player; // Referencia al jugador.
    private Vector3 initialPosition; // Posición inicial del enemigo.
    private bool isChasing = false; // Indica si el enemigo está persiguiendo al jugador.

    private void Start()
    {
        initialPosition = transform.position;
        player = GameObject.FindGameObjectWithTag("Player").transform; // Busca al jugador por su tag "Player".

    }

    private void Update()
    {
        if (player == null)
        {
            // Si player es null, no hay jugador detectado, así que no realizamos más acciones.
            return;
        }

        // El resto de tu código para calcular la distancia y realizar las acciones en función de la distancia al jugador.
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        float detectionRadius = detectionZone.radius * transform.lossyScale.x;

        if (isChasing)
        {
            // El enemigo persigue al jugador mientras esté dentro del área de detección.
            if (distanceToPlayer < detectionRadius)
            {
                // Mueve al enemigo hacia el jugador con una velocidad específica.
                Vector3 direction = (player.position - transform.position).normalized;
                transform.Translate(direction * moveSpeed * Time.deltaTime);
            }
            else
            {
                // El jugador salió del área de detección, el enemigo debe detenerse.
                isChasing = false;
            }
        }
        else
        {
            // El enemigo no está persiguiendo al jugador, regresa a su posición inicial.
            if (distanceToPlayer < detectionRadius)
            {
                // El jugador entró en el área de detección, el enemigo comienza a perseguir.
                isChasing = true;
            }
            else if (Vector2.Distance(transform.position, initialPosition) > 0.1f)
            {
                // El enemigo regresa a su posición inicial si no está persiguiendo al jugador.
                transform.position = Vector3.MoveTowards(transform.position, initialPosition, returnSpeed * Time.deltaTime);

            }
        }
    }
}