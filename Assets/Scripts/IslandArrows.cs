using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IslandArrows : MonoBehaviour
{

    [SerializeField]
    private int maxArrows = 3;

    private Image[] arrows;

    private float radius;
    private Player player;


    // Start is called before the first frame update
    void Start()
    {
        arrows = GetComponentsInChildren<Image>();

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
        radius *= 2f;

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 playerPos = player.transform.position;
        Collider2D[] hits = Physics2D.OverlapCircleAll(playerPos, radius, LayerMask.GetMask("Island"));

        List<GameObject> pointingAt = new List<GameObject>();
        int count = 0;
        for (int i = 0; i < hits.Length && count < arrows.Length; i++)
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
                GameObject island = col.gameObject;
                pointingAt.Add(island);


                // Vector2 screenPos = new Vector2(viewportPos.x - 0.5f, viewportPos.y - 0.5f) * 2;
                // float max = Mathf.Max(Mathf.Abs(viewportPos.x), Mathf.Abs(viewportPos.y));
                // screenPos = (screenPos / (max * 2)) + new Vector2(0.5f, 0.5f);
                // Image arrow = arrows[i];
                // arrow.rectTransform.rotation = Quaternion.Euler(0, 0, Vector3.Angle(Vector3.up, (col.transform.position - playerPos)));
                // arrow.rectTransform.position = screenPos;

                Vector2 screenPos = Camera.main.WorldToScreenPoint(col.transform.position);
                screenPos = Vector2.ClampMagnitude(screenPos, Camera.main.orthographicSize);
                count++;
            }

        }

    }
}
