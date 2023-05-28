using UnityEngine;

public class TriggerDamaged : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            if (!player.GetIsDeath)
            {
                player.Died();
            }
        }
    }
}
