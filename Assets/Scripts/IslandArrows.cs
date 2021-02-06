using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IslandArrows : MonoBehaviour
{

    [SerializeField]
    private int maxArrows = 3;

    private float radius;
    private PlayerShip player;

    private List<IslandArrow> activeArrows;


    // Start is called before the first frame update
    void Start()
    {
        activeArrows = new List<IslandArrow>(GetComponentsInChildren<IslandArrow>());

        float height = 2f * Camera.main.orthographicSize;
        float width = height * Camera.main.aspect;
        if (width > height)
        {
            radius = width;
        }
        else
        {
            radius = height;
        }

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerShip>();
    }


    // Update is called once per frame
    void Update()
    {
        Vector3 playerPos = player.transform.position;
        Collider2D[] hits = Physics2D.OverlapCircleAll(playerPos, radius, LayerMask.GetMask("Island"));

        // Find nearby islands
        List<GameObject> nearbyIslands = new List<GameObject>();
        for (int i = 0; i < hits.Length; i++)
        {
            Collider2D col = hits[i];

            Vector2 viewportPos = Camera.main.WorldToViewportPoint(col.transform.position);
            if (viewportPos.x >= 0 && viewportPos.x <= 1 && viewportPos.y >= 0 && viewportPos.y <= 1)
            {
                // On-screen island, do nothing
                continue;
            }
            else
            {
                nearbyIslands.Add(col.gameObject);
            }
        }

        // Deactivate arrows for islands that are no longer nearby
        foreach (IslandArrow arrow in activeArrows)
        {
            // Island is not nearby anymore so deactivate arrow
            if (arrow.Target != null && !nearbyIslands.Contains(arrow.Target.gameObject))
            {
                arrow.Target = null;
            }
        }

        for (int i = 0; i < nearbyIslands.Count; i++)
        {
            GameObject island = nearbyIslands[i];
            if (i < activeArrows.Count)
            {
                activeArrows[i].Target = island.transform;
            }
            else
            {
                IslandArrow newArrow = Instantiate(activeArrows[i - 1], transform);
                newArrow.Target = island.transform;
                activeArrows.Add(newArrow);
            }
        }

    }
}
