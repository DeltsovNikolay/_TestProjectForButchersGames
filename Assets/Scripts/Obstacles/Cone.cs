using System.Collections;
using UnityEngine;

public class Cone : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    private int _hashShowKey;

    private void Start()
    {
        _hashShowKey = Animator.StringToHash("Show");

        StartCoroutine(ShowRoutine());
    }

    private IEnumerator ShowRoutine()
    {
        WaitForSeconds hashSecond = new WaitForSeconds(Random.Range(2, 5f));

        while (true)
        {
            yield return hashSecond;

            _animator.SetTrigger(_hashShowKey);
        }
    }
}
