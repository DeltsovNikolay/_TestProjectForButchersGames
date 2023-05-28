using UnityEngine;

public class PlayerNotActive : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private PoolPlayers _poolPlayers;
    [SerializeField] private ParticleSystem _effect;
    [SerializeField] private Transform _parent;

    private bool _isActive;

    public void Active()
    {
        if (_isActive) return;

        _isActive = true;

        _effect.gameObject.SetActive(true);
        _effect.transform.parent = _parent;

        Player player = Instantiate(_player, transform.position, Quaternion.identity);

        player.SetMove(true);
        _poolPlayers.Add(player);
        player.transform.parent = _poolPlayers.transform;

        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            Active();
        }
    }
}
