using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField]
    private float movementSpeed = 5.0f;

    [SerializeField]
    private float steeringSpeed = 1.0f;

    private float rotation = 0f;

    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");


        rotation -= horizontal * steeringSpeed * Time.deltaTime;
        spriteRenderer.transform.rotation = Quaternion.Euler(0, 0, rotation);

        Vector3 velocity = spriteRenderer.transform.up * vertical * movementSpeed;

        float newX = transform.position.x + velocity.x * Time.deltaTime;
        float newY = transform.position.y + velocity.y * Time.deltaTime;

        if (Physics2D.OverlapPoint(new Vector2(newX, transform.position.y), LayerMask.GetMask("Island")))
        {
            velocity.x = 0;
        }

        if (Physics2D.OverlapPoint(new Vector2(transform.position.x, newY), LayerMask.GetMask("Island")))
        {
            velocity.y = 0;
        }

        transform.position += velocity * Time.deltaTime;
    }
}
