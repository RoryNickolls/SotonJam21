using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField]
    private float movementSpeed = 5.0f;

    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 velocity = new Vector3(horizontal, vertical, 0) * movementSpeed;

        if (velocity.x < 0)
        {
            spriteRenderer.flipX = true;
        }
        else if (velocity.x > 0)
        {
            spriteRenderer.flipX = false;
        }

        transform.position += velocity * Time.deltaTime;
    }
}
