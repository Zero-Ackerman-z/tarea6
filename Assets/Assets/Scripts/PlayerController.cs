using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private LayerMask groundLayers;
    [SerializeField] private Rigidbody2D myRBD2;
    [SerializeField] private float velocityModifier = 5f;
    [SerializeField] private float rayDistance = 10f;
    [SerializeField] private AnimatorController animatorController;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] public GameObject projectilePrefab;
    [SerializeField] public Transform firePoint;
    public float projectileSpeed = 10f;

    private void Update()
    {
        Vector2 movementPlayer = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        myRBD2.velocity = movementPlayer * velocityModifier;

        animatorController.SetVelocity(velocityCharacter: myRBD2.velocity.magnitude);

        Vector2 mouseInput = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        CheckFlip(mouseInput.x);

        Debug.DrawRay(transform.position, mouseInput.normalized * rayDistance, Color.red);

        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Right Click");
            Shoot();
        }
        else if (Input.GetMouseButtonDown(1))
        {
            Debug.Log("Left Click");
            Shoot();
        }
    }

    private void CheckFlip(float x_Position)
    {
        spriteRenderer.flipX = (x_Position - transform.position.x) < 0;
    }

    void Shoot()
    {
        // Obtiene la posición del ratón en el mundo 2D.
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Calcula la dirección desde el "firePoint" hacia la posición del ratón.
        Vector2 direction = (mousePosition - (Vector2)firePoint.position).normalized;

        // Calcula la posición de inicio de la bala basada en la posición del jugador.
        Vector2 bulletStartPosition = (Vector2)transform.position;

        // Crea un nuevo proyectil en la posición calculada con la rotación adecuada.
        GameObject projectile = Instantiate(projectilePrefab, bulletStartPosition, Quaternion.Euler(0, 0, 0));

        // Obtiene el componente Rigidbody2D del proyectil.
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();

        // Aplica velocidad al proyectil en la dirección calculada.
        rb.velocity = direction * projectileSpeed;
    }
}