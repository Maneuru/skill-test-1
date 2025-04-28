using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
///<summary>MonoBehaviour that detect collision and trigger item collection</summary>
public class Collectible : MonoBehaviour
{
    // ComponentReferences
    private Animator animator;

    [Header("Collectible Data")]
    [SerializeField] private CollectibleData data;

    [Header("Animation")]
    [SerializeField] private float speed;
    [SerializeField] private float maxExcursion;

    // Private fields
    private Vector3 originalPosition;
    private bool used;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        originalPosition = transform.position;
    }

    private void Update()
    {
        transform.position = originalPosition + maxExcursion * Mathf.Sin(Time.time * speed) * Vector3.up;
    }

    /// <summary>Destroy gameobject on when collected</summary>
    public void OnCollect()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Ensure if has already been used
        if (used)
        {
            return;
        }

        // Ensure there a Collector in `other` gameObject and call `CollectItem`
        if (other.TryGetComponent(out Collector collector))
        {
            // Set that collectible has been used
            used = true;

            // Collect throught collector
            collector.CollectItem(data);
            animator.SetTrigger("Collect");
        }
    }
}