using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Action<Player> Dieded;

    [SerializeField] private UnitRagdoll _unitRagdoll;
    [SerializeField] private Animator _animator;

    private bool _isDeath;
    private int _hashIsMoveKey;

    public bool GetIsDeath => _isDeath;

    private void Awake()
    {
        _hashIsMoveKey = Animator.StringToHash("IsMove");
    }

    public void SetMove(bool isMove)
    {
        _animator.SetBool(_hashIsMoveKey, isMove);
    }

    public void Died()
    {
        if (_isDeath) return;

        _isDeath = true;
        Dieded?.Invoke(this);

        _unitRagdoll.ActivationRagdoll();

        Destroy(gameObject);
    }

    public void SetVictory()
    {
        _animator.SetTrigger($"Victory_{UnityEngine.Random.Range(0, 4)}");
    }
}
