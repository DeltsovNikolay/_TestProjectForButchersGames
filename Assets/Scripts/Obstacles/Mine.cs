using UnityEngine;

public class Mine : MonoBehaviour
{
    [SerializeField] private ParticleSystem _effect;
    [SerializeField] private Transform _parent;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            if (!player.GetIsDeath)
            {
                player.Died();

                CameraShake.Get.Shake();
                _effect.gameObject.SetActive(true);
                _effect.transform.parent = _parent;

                Destroy(gameObject);
            }
        }
    }
}
