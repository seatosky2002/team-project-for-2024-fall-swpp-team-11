using UnityEngine;

public class CharacterInteract2D : MonoBehaviour
{
    private IInteractable currentInteractable;

    void Update()
    {
        FindClosestInteractable();
    }

    private void FindClosestInteractable()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, 3f);

        IInteractable closestInteractable = null;
        float closestDistance = Mathf.Infinity;

        foreach (var hitCollider in hitColliders)
        {
            IInteractable interactable = hitCollider.GetComponent<IInteractable>();
            if (interactable != null)
            {
                float distance = Vector2.Distance(transform.position, hitCollider.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestInteractable = interactable;
                }
            }
        }

        if (currentInteractable != closestInteractable)
        {
            if (currentInteractable != null)
                currentInteractable.OnDefocus();

            currentInteractable = closestInteractable;

            if (currentInteractable != null)
                currentInteractable.OnFocus();
        }
    }

    public void TryInteract()
    {
        if (currentInteractable != null)
        {
            currentInteractable.Interact();
        }
    }
}
