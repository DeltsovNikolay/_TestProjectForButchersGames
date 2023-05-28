using System.Collections;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    private static CameraShake _instance;
    private Coroutine _shakeRoutine;

    public static CameraShake Get => _instance;

    private void Awake()
    {
        if (!_instance) _instance = this;
    }

    public void Shake()
    {
        if (_shakeRoutine != null)
        {
            StopCoroutine(_shakeRoutine);
        }

        _shakeRoutine = StartCoroutine(ShakeRoutine());
    }

    private IEnumerator ShakeRoutine()
    {
        Vector3 _origPos = transform.position;
        float _elapsed = 0f;

        while (_elapsed < 0.2f)
        {
            float _x = Random.Range(-0.13f, 0.13f);
            float _y = Random.Range(-0.13f, 0.13f);

            transform.position = new Vector3(transform.position.x + _x, transform.position.y + _y, transform.position.z);

            _elapsed += Time.deltaTime;

            yield return null;
        }

        transform.position = _origPos;
        _shakeRoutine = null;
    }
}