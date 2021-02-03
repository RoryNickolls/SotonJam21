using UnityEngine;
using UnityEngine.UI;

public class IslandArrow : MonoBehaviour
{
    [SerializeField] private Transform target;

    private RectTransform rectTransform;
    private Player player;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    private void Update()
    {
        if (target != null)
        {
            Vector2 viewportPos = Camera.main.WorldToViewportPoint(target.position);
            if (viewportPos.x >= 0 && viewportPos.x <= 1 && viewportPos.y >= 0 && viewportPos.y <= 1)
            {
                return;
            }
            Vector2 playerPos = Camera.main.WorldToScreenPoint(player.transform.position);
            Vector2 targetPos = Camera.main.WorldToScreenPoint(target.position);

            Vector2 diff = targetPos - playerPos;


            float angle = Mathf.Atan2(viewportPos.x, viewportPos.y);
            viewportPos.x = 0.5f;
            viewportPos.y = 0.5f;
            rectTransform.anchoredPosition = new Vector2(viewportPos.x * Screen.width * 2, viewportPos.y * Screen.height * 2);
            rectTransform.localRotation = Quaternion.Euler(0, 0, angle * Mathf.Rad2Deg);
        }
    }
}