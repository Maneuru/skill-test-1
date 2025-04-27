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

    private void Awake()
    {
        animator = GetComponent<Animator>();
        originalPosition = transform.position;
    }

    private void Update()
    {
        transform.position = originalPosition + maxExcursion * Mathf.Sin(Time.time * speed) * Vector3.up;
    }

    public void OnCollect()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Ensure there a Collector in `other` gameObject and call `CollectItem`
        if (other.TryGetComponent(out Collector collector))
        {
            collector.CollectItem(data);
            animator.SetTrigger("Collect");
        }
    }
}