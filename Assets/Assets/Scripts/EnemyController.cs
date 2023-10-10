using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private CircleCollider2D detectionZone; // Collider para el �rea de detecci�n.
    [SerializeField] private float moveSpeed = 4f; // Velocidad de movimiento hacia el jugador.
    [SerializeField] private float returnSpeed = 3f; // Velocidad de regreso a la posici�n inicial.

    private Transform player; // Referencia al jugador.
    private Vector3 initialPosition; // Posici�n inicial del enemigo.
    private bool isChasing = false; // Indica si el enemigo est� persiguiendo al jugador.

    private void Start()
    {
        initialPosition = transform.position;
        player = GameObject.FindGameObjectWithTag("Player").transform; // Busca al jugador por su tag "Player".

    }

    private void Update()
    {
        if (player == null)
        {
            // Si player es null, no hay jugador detectado, as� que no realizamos m�s acciones.
            return;
        }

        // El resto de tu c�digo para calcular la distancia y realizar las acciones en funci�n de la distancia al jugador.
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        float detectionRadius = detectionZone.radius * transform.lossyScale.x;

        if (isChasing)
        {
            // El enemigo persigue al jugador mientras est� dentro del �rea de detecci�n.
            if (distanceToPlayer < detectionRadius)
            {
                // Mueve al enemigo hacia el jugador con una velocidad espec�fica.
                Vector3 direction = (player.position - transform.position).normalized;
                transform.Translate(direction * moveSpeed * Time.deltaTime);
            }
            else
            {
                // El jugador sali� del �rea de detecci�n, el enemigo debe detenerse.
                isChasing = false;
            }
        }
        else
        {
            // El enemigo no est� persiguiendo al jugador, regresa a su posici�n inicial.
            if (distanceToPlayer < detectionRadius)
            {
                // El jugador entr� en el �rea de detecci�n, el enemigo comienza a perseguir.
                isChasing = true;
            }
            else if (Vector2.Distance(transform.position, initialPosition) > 0.1f)
            {
                // El enemigo regresa a su posici�n inicial si no est� persiguiendo al jugador.
                transform.position = Vector3.MoveTowards(transform.position, initialPosition, returnSpeed * Time.deltaTime);

            }
        }
    }
}