using UnityEngine;

public class Rotate : MonoBehaviour
{
    [SerializeField] private float _speed = 150f;
    [SerializeField] private Vector3 _rotateVector = Vector3.back;

    private void Update()
    {
        transform.Rotate(_rotateVector, _speed * Time.deltaTime);
    }
}
