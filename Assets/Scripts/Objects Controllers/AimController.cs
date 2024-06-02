using UnityEngine;

public class AimController : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private float distance = 1.0f;

    void Update()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;

        Vector3 direction = mousePosition - player.transform.position;

        Vector3 targetPosition = player.transform.position + direction.normalized * distance;

        transform.position = targetPosition;

        transform.up = direction;
    }
}