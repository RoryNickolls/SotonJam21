using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ship : MonoBehaviour
{

    // Added getters and setters and default values. Might be wrong but explain later if this is a mistake

    [SerializeField] private float hullHealth = 100f;
    public float HullHealth
    {
        get { return hullHealth; }
        set { hullHealth = value; }
    }
    [SerializeField] private float fireRate = 10f;
    public float FireRate
    {
        get { return fireRate; }
        set { fireRate = value; }
    }
    [SerializeField] private float maxCannonballs = 10f;
    public float MaxCannonballs
    {
        get { return maxCannonballs; }
        set { maxCannonballs = value; }
    }    
    [SerializeField] private float cannonballDamage = 10f;
    public float CannonballDamage
    {
        get { return cannonballDamage; }
        set { cannonballDamage = value; }
    }
    [SerializeField] private float movementSpeed = 1f;
    public float MovementSpeed
    {
        get { return movementSpeed; }
        set { movementSpeed = value; }
    }
    [SerializeField] private float turningSpeed = 30f;
    public float TurningSpeed
    {
        get { return turningSpeed; }
        set { turningSpeed = value; }
    }



    protected Vector2 velocity;


    // INSERT BEHAVIOUR HERE
}
