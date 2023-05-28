using UnityEngine;

public class Gem : MonoBehaviour
{
    [SerializeField] private GemCyrrency _gemCurrency;
    [SerializeField] private ParticleSystem _effect;
    [SerializeField] private Transform _parent;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            _gemCurrency.Add(1);
            _effect.gameObject.SetActive(true);
            _effect.transform.parent = _parent;

            Destroy(gameObject);
        }
    }
}
