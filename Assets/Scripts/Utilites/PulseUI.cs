using System.Collections;
using DG.Tweening;
using UnityEngine;

public class PulseUI : MonoBehaviour
{
    [SerializeField] private RectTransform _rectTransform;
    [SerializeField] private float _timePulse;
    [SerializeField] private Vector3 _pulseScale;

    private DG.Tweening.Core.TweenerCore<Vector3, Vector3, DG.Tweening.Plugins.Options.VectorOptions> _pulseTween;
    private DG.Tweening.Core.TweenerCore<Vector3, Vector3, DG.Tweening.Plugins.Options.VectorOptions> _revertPulseTween;
    private Vector3 _initScale;
    private Coroutine _pulseLoopRoutine;

    private void Start()
    {
        _initScale = _rectTransform.localScale;
    }

    public void PulseLoop(float duration)
    {
        if (_pulseLoopRoutine != null) StopCoroutine(_pulseLoopRoutine);
        _pulseLoopRoutine = StartCoroutine(PulseLoopRoutine(duration));
    }

    private IEnumerator PulseLoopRoutine(float duration)
    {
        WaitForSeconds _secondHash = new WaitForSeconds(duration);

        while (true)
        {
            yield return _secondHash;

            Pulse();
        }
    }

    public void Pulse(CallbackPunch callback = null)
    {
        if (_revertPulseTween != null)
        {
            _revertPulseTween.Kill(true);
        }

        if (_pulseTween == null)
        {
            _pulseTween = _rectTransform.DOScale(_pulseScale, _timePulse);

            _pulseTween.onComplete = () =>
            {
                RevertPulse(callback);
            };
        }
        else
        {
            _pulseTween.Kill(true);
            _pulseTween = _rectTransform.DOScale(_pulseScale, _timePulse);

            _pulseTween.onComplete = () =>
            {
                RevertPulse(callback);
            };
        }
    }

    private void RevertPulse(CallbackPunch callback = null)
    {
        _pulseTween = null;
        _revertPulseTween = _rectTransform.DOScale(_initScale, _timePulse);

        _revertPulseTween.onComplete = () =>
        {
            callback?.Invoke();
            _revertPulseTween = null;
        };
    }

    public void StopPulse()
    {
        if (_pulseTween != null) _pulseTween.Kill(true);
    }

    public delegate void CallbackPunch();
}