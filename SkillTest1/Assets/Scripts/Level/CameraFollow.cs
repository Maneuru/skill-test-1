using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector2 offset;
    [SerializeField] private float speed;

    private void Update()
    {
        Vector2 direction = (target.position + (Vector3)offset - transform.position).normalized;
        transform.position +=  speed * Time.unscaledDeltaTime * (Vector3)direction;
    }
}
