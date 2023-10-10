using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolMovementController : MonoBehaviour
{
    [SerializeField] private float raycastLength = 2f; // Agrega esta línea para definir la longitud del Raycast.
    [SerializeField] private Transform[] checkpointsPatrol;
    [SerializeField] private Rigidbody2D myRBD2;
    [SerializeField] private AnimatorController animatorController;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private float normalVelocity = 3f; // Velocidad normal del enemigo.
    [SerializeField] private float chaseVelocity = 5f; // Velocidad al perseguir al jugador.
    [SerializeField] private Transform currentPositionTarget;
    private int patrolPos = 0;
    [SerializeField] public Transform player; // Referencia al jugador.
    private bool isChasing = false; // Indica si el enemigo está persiguiendo al jugador.

    private void Start()
    {
        currentPositionTarget = checkpointsPatrol[patrolPos];
        transform.position = currentPositionTarget.position;
    }

    private void Update()
    {
        CheckNewPoint();

        animatorController.SetVelocity(velocityCharacter: myRBD2.velocity.magnitude);

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer < 3.0f)
        {
            isChasing = true;
            myRBD2.velocity = (player.position - transform.position).normalized * chaseVelocity;
            CheckFlip(myRBD2.velocity.x);
        }
        else
        {
            isChasing = false;
            myRBD2.velocity = (currentPositionTarget.position - transform.position).normalized * normalVelocity;
            CheckFlip(myRBD2.velocity.x);
        }

        // Dibuja el Raycast en la dirección de movimiento.
        Debug.DrawRay(transform.position, myRBD2.velocity.normalized * raycastLength, Color.red);
    }


    private void CheckNewPoint()
    {
        if (Mathf.Abs((transform.position - currentPositionTarget.position).magnitude) < 0.25)
        {
            patrolPos = patrolPos + 1 == checkpointsPatrol.Length ? 0 : patrolPos + 1;
            currentPositionTarget = checkpointsPatrol[patrolPos];
        }
    }

    private void CheckFlip(float x_Position)
    {
        spriteRenderer.flipX = (x_Position - transform.position.x) < 0;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            player = collision.gameObject.transform;
        }

    }

}


