using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShip : Ship
{
    // Update is called once per frame
    public override void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        if (vertical == 0)
        {
            Vector3 newCameraPos = Mathf.Sin(Time.time) * (Vector3.up + Vector3.right) * 0.01f;
            newCameraPos.z = -10;
            Camera.main.transform.localPosition = newCameraPos;
        }

        steering = horizontal * turningSpeed;
        velocity = spriteRenderer.transform.up * vertical * movementSpeed;

        base.Update();
    }

    public override void Move()
    {
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
        base.Move();
    }
}
