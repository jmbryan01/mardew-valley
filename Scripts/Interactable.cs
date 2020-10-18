using UnityEngine;

public class Interactable : MonoBehaviour
{
    [SerializeField] private float radius = 3f;
    [SerializeField] private Transform interactionTransform;

    private bool hasBeenChosen = false;
    private bool hasAttemptedToInteract = false;
    private Transform player;

    private Renderer sprite;
    private Color startColor;

    void Start()
    {
        sprite = GetComponent<Renderer>();
    }

    public virtual void Interact()
    {
        // This method is meant to be overridden
        Debug.Log("Interacting with " + transform.name);
    }

    void Update()
    {
        if(hasBeenChosen && !hasAttemptedToInteract)
        {
            float distance = Vector2.Distance(player.position, interactionTransform.position);
            if (distance <= radius)
            {
                Interact();
            }
            hasAttemptedToInteract = true;
        }
    }

    public void TargetInteractable(Transform playerTransform)
    {
        player = playerTransform;
        hasBeenChosen = true;
        hasAttemptedToInteract = false;
    }

    // Highlight the sprite when the mouse hovers over the sprite collider
    void OnMouseEnter()
    {
        startColor = sprite.material.color;
        sprite.material.color = Color.red;
    }

    // Remove color highlight when the mouse leaves the sprite collider
    void OnMouseExit()
    {
        sprite.material.color = startColor;
    }

    void OnDrawGizmosSelected()
    {
        if(interactionTransform == null)
        {
            interactionTransform = transform;
        }
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(interactionTransform.position, radius);
    }
}
