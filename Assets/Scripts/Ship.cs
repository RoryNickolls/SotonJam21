using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ship : MonoBehaviour
{
    [SerializeField] private float hullHealth;
    [SerializeField] private float fireRate;

    [SerializeField] private float movementSpeed;
    [SerializeField] private float turningSpeed;

    protected Vector2 velocity;


    // INSERT BEHAVIOUR HERE
}
