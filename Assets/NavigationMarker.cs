using UnityEngine;

public class NavigationMarker : MonoBehaviour
{
    public string[] targetTags = { "Frozen", "Regular", "Refrigerated", "Bulk" };
    private Transform player;
    private Transform closestTarget;

    void Start()
    {
        player = GameObject.FindWithTag("Player")?.transform;
        if (player == null)
        {
            Debug.LogWarning("Player not found. Please tag the player GameObject as 'Player'.");
        }

        gameObject.SetActive(false);
    }

    void Update()
    {
        if (player != null)
        {
            float closestDistance = float.MaxValue;
            Transform newClosestTarget = null;

            foreach (string tag in targetTags)
            {
                GameObject[] taggedObjects = GameObject.FindGameObjectsWithTag(tag);
                foreach (GameObject obj in taggedObjects)
                {
                    float distance = Vector3.Distance(player.position, obj.transform.position);
                    if (distance < closestDistance)
                    {
                        closestDistance = distance;
                        newClosestTarget = obj.transform;
                    }
                }
            }

            if (newClosestTarget != null)
            {
                closestTarget = newClosestTarget;
                transform.position = closestTarget.position;
            }


            if (closestTarget != null && Vector3.Distance(player.position, closestTarget.position) < 3.0f)
            {
                gameObject.SetActive(false);
            }
        }
    }

}
