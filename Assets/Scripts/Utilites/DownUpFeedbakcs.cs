using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI.Utilites
{
    public class DownUpFeedbakcs : MonoBehaviour, IPointerExitHandler, IPointerEnterHandler, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] private Vector3 _scale = new Vector3(0.75f, 0.75f, 0.75f);
        [SerializeField] private Vector3 _pointerEnterScale = new Vector3(1.1f, 1.1f, 1.1f);
        [SerializeField] private float _duration = 0.2f;

        private Vector3 _defaultScale;
        private Tween _downTween;
        private Tween _upTween;
        private Tween _pointerEnterTween;
        
        private void Start()
        {
            _defaultScale = transform.localScale;
        }

        public void DownFeedbacks()
        {
            _upTween.Kill();
            _downTween.Kill();
            _pointerEnterTween.Kill();
            _downTween = transform.DOScale(_scale, _duration);
        }

        public void UpFeedbacks()
        {
            _downTween.Kill();
            _upTween.Kill();
            _pointerEnterTween.Kill();
            _upTween = transform.DOScale(_defaultScale, _duration);
        }

        public void PointerEnter()
        {
            _downTween.Kill();
            _upTween.Kill();
            _pointerEnterTween.Kill();
            _pointerEnterTween = transform.DOScale(_pointerEnterScale, _duration);
        }

        public void PointerExit()
        {
            UpFeedbacks();
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            UpFeedbacks();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            DownFeedbacks();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            PointerEnter();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            PointerExit();
        }
    }
}