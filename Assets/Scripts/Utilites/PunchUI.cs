using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;

public class PunchUI : MonoBehaviour
{
    [SerializeField] private float _duration;
    [SerializeField] private Vector3 _punchScale;

    private TweenerCore<Vector3, Vector3, VectorOptions> _punchInverse;

    public void Punch(Vector3 scale, float durationScale, CallbackPunch callback = null)
    {
        transform.DOScale(scale, durationScale).onComplete = () =>
        {
            transform.DOScale(_punchScale, _duration).onComplete = () =>
            {
                transform.DOScale(scale, _duration);
                callback?.Invoke();
            };
        };
    }

    public void PuchInverse(Vector3 scale, float durationScale, CallbackPunch callback = null)
    {
        _punchInverse.Kill(true);
        _punchInverse = transform.DOScale(_punchScale, _duration);

        _punchInverse.onComplete = () =>
        {
            transform.DOScale(scale, durationScale).onComplete = () =>
            {
                callback?.Invoke();
            };
        };
    }
}

public delegate void CallbackPunch();