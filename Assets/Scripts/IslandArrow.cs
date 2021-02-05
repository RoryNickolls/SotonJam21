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
            float width = Screen.width;
            float height = Screen.height;
            Vector2 dir = viewportPos - new Vector2(0.5f, 0.5f);
            float angle = Mathf.Atan2(-dir.x, dir.y);
            rectTransform.anchoredPosition = dir.normalized * 0.75f * Camera.main.pixelHeight;
            rectTransform.localRotation = Quaternion.Euler(0, 0, angle * Mathf.Rad2Deg);
        }
    }

    public Transform Target
    {
        get { return target; }
        set
        {
            target = value;
            if (target == null)
            {
                GetComponent<Image>().enabled = false;
            }
            else
            {
                GetComponent<Image>().enabled = true;
            }
        }
    }
}