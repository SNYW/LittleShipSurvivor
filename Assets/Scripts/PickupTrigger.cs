using UnityEngine;

public class PickupTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<CurrencyPickup>(out var pickup))
        {
            pickup.OnPickup();
        }
    }
}
