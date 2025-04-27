using UnityEngine;

public class PlatformMovement : MonoBehaviour
{
    [SerializeField] private Vector2 secondPointOffset;
    [SerializeField] private float movementSpeed;
    private Vector2 firstPoint;
    private Vector2 secondPoint;
    private Vector2 currentDestination;

    private void Awake()
    {
        firstPoint = transform.position;
        secondPoint = firstPoint + secondPointOffset;
        currentDestination = secondPoint;
    }

    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, currentDestination, movementSpeed * Time.deltaTime);
        if (transform.position == (Vector3)currentDestination)
        {
            currentDestination = currentDestination != firstPoint? firstPoint : secondPoint;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector3 colliderPosition = collision.transform.position;
        foreach(var contact in collision.contacts)
        {
            var contactDirection = ((Vector3)contact.point - colliderPosition).normalized;
            if (Vector3.Dot(contactDirection, Vector3.down) > .9f)
            {
                collision.collider.transform.SetParent(transform);
                break;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        collision.collider.transform.SetParent(null);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        if (firstPoint == secondPoint)
        {
            Gizmos.DrawSphere(transform.position, 0.1f);
            Gizmos.DrawSphere(transform.position + (Vector3)secondPointOffset, 0.1f);
        }
        else
        {
            Gizmos.DrawSphere(firstPoint, 0.1f);
            Gizmos.DrawSphere(secondPoint, 0.1f);
        }
    }
}
