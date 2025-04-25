using UnityEngine;

public class MapBorder : MonoBehaviour
{
    [SerializeField] private bool killOnContact;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (killOnContact && collision.collider.CompareTag("Player"))
        {
            // TODO - Kill
            collision.collider.GetComponent<Character>();
        }
    }
}
