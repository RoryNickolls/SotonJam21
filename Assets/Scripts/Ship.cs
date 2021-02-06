using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ship : MonoBehaviour
{
    [SerializeField] private float hullHealth;
    [SerializeField] private float fireRate;

    [SerializeField] protected float movementSpeed;
    [SerializeField] protected float turningSpeed;

    protected Vector3 velocity;
    protected float steering = 0f;
    private float rotation = 0f;

    protected SpriteRenderer spriteRenderer;

    public virtual void Start()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    public virtual void Update()
    {
        Move();
    }


    public virtual void Move()
    {
        rotation -= steering * Time.deltaTime;
        spriteRenderer.transform.rotation = Quaternion.Euler(0, 0, rotation);
        transform.position += velocity * Time.deltaTime;
    }
}
