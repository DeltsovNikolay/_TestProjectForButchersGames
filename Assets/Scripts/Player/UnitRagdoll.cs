using UnityEngine;

public class UnitRagdoll : MonoBehaviour
{
    [SerializeField] private GameObject _ragdoll;

    public void ActivationRagdoll()
    {
        Instantiate(_ragdoll, transform.position, Quaternion.identity);
    }
}
