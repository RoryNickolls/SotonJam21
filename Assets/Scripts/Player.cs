using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{

    // Reference to the actual ship object
    [SerializeField] private GameObject PlayerGameObject;

    // Ship variables stored locally so functions don't need to be repeatedly called
    private float hHealth;
    private float mCannons;
    private float cDamage;
    private float fRate;
    private float tSpeed;
    private float mSpeed;
    private float currentRotation;

    // A reference to the sprite renderer
    private SpriteRenderer spriteRenderer;

    // References to the UI elements and player resources
    private float playerGold;
    private float playerHullPoints;
    private float playerLoadedCannons;
    [SerializeField] private GameObject GoldReference;
    [SerializeField] private GameObject HullReference;
    [SerializeField] private GameObject CannonsReference;

    // References to the UI objects for ease of toggling
    [SerializeField] private GameObject ShopObjectReference;
    [SerializeField] private GameObject PauseMenuReference;

    private void Start()
    {
        InitialiseGameWorld();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateShipDesiredLocation();
    }

    private void UpdateShipDesiredLocation()
    {

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        if (vertical == 0)
        {
            Vector3 newCameraPos = Mathf.Sin(Time.time) * (Vector3.up + Vector3.right) * 0.01f;
            newCameraPos.z = -10;
            Camera.main.transform.localPosition = newCameraPos;
        }

        currentRotation -= horizontal * tSpeed * Time.deltaTime;
        spriteRenderer.transform.rotation = Quaternion.Euler(0, 0, currentRotation);

        Vector3 velocity = spriteRenderer.transform.up * vertical * mSpeed;

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

    // I'm not sure what the performance impact is of this, figured not GetComponent every frame was an
    // improvement but maybe it undermines the point of making a derived class for ship.
    private void UpdateShipStats()
    {    
        hHealth = PlayerGameObject.GetComponent<PlayerShip>().HullHealth;    
        mCannons = PlayerGameObject.GetComponent<PlayerShip>().MaxCannonballs;   
        cDamage = PlayerGameObject.GetComponent<PlayerShip>().CannonballDamage;   
        fRate = PlayerGameObject.GetComponent<PlayerShip>().FireRate;
        mSpeed = PlayerGameObject.GetComponent<PlayerShip>().MovementSpeed;
        tSpeed = PlayerGameObject.GetComponent<PlayerShip>().TurningSpeed;
    }

    private void UpdatePlayerUI()
    {
        GoldReference.GetComponent<TMP_Text>().text = playerGold.ToString();
        HullReference.GetComponent<TMP_Text>().text = playerHullPoints.ToString();
        CannonsReference.GetComponent<TMP_Text>().text = playerLoadedCannons.ToString();
    }

    private void ToggleActive(GameObject Object)
    {
        if(Object.activeInHierarchy == true)
        {
            Object.SetActive(false);
        }
        else
        {
            Object.SetActive(true);
        }
    }

    private void InitialiseGameWorld()
    {
        // Starting variables and update player UI to match
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        UpdateShipStats();
        playerGold = 100f;
        playerHullPoints = hHealth;
        playerLoadedCannons = mCannons;
        UpdatePlayerUI();
        ShopObjectReference.SetActive(false);
        PauseMenuReference.SetActive(false);
    }

    private void OnGUI()
    {
        Event e = Event.current;
        if (e.isKey)
        {
            if (e.keyCode == KeyCode.Escape)
            {
                if(PauseMenuReference.activeInHierarchy == true)
                {
                    PauseMenuReference.SetActive(false);
                    return;
                }
                if(PauseMenuReference.activeInHierarchy == false)
                {
                    PauseMenuReference.SetActive(true);
                    return;
                }
            }
        }
    }


}
