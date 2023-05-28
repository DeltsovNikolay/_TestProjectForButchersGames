using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class Drawing : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    public Action<LineRenderer> EndDrawing;

    [SerializeField] private LineRenderer _brush;
    [SerializeField] private Camera _drawCamera;
    [SerializeField] private Vector2 _limitPositionX;
    [SerializeField] private Vector2 _limitPositionY;

    private LineRenderer _currentBrush;
    private Vector3 _lastPos;

    private Ray _ray;
    private RaycastHit _hit;

    private void DestroyBrush()
    {
        if (_currentBrush != null) Destroy(_currentBrush.gameObject);
        _currentBrush = null;
    }

    private void CreateBrush(Vector3 _instancePos)
    {
        if (_currentBrush != null) DestroyBrush();

        _currentBrush = Instantiate(_brush);
        _currentBrush.transform.parent = transform;

        _currentBrush.SetPosition(0, _instancePos);
        _currentBrush.SetPosition(1, _instancePos);
    }

    private void AddPoint(Vector3 pointPos)
    {
        _currentBrush.positionCount++;
        int _posIndex = _currentBrush.positionCount - 1;
        _currentBrush.SetPosition(_posIndex, pointPos);
    }

    public void OnDrag(PointerEventData eventData)
    {
        _ray = _drawCamera.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(_ray, out _hit, 1000);

        if (_hit.collider != null)
        {
            Vector3 _mousePos = new Vector3(_hit.point.x, _hit.point.y + 0.4f, _hit.point.z);

            if (_mousePos != _lastPos && Vector3.Distance(_mousePos, _lastPos) >= 0.2f)
            {
                AddPoint(_mousePos);
                _lastPos = _mousePos;
            }
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        EndDrawing?.Invoke(_currentBrush);

        DestroyBrush();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _ray = _drawCamera.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(_ray, out _hit, 1000);

        if (_hit.collider != null)
        {
            Vector3 _mousePos = new Vector3(_hit.point.x, _hit.point.y + 0.4f, _hit.point.z);

            if (_mousePos.x > _limitPositionX.x && _mousePos.x < _limitPositionX.y && _mousePos.y > _limitPositionY.x && _mousePos.y < _limitPositionY.y)
            {
                CreateBrush(_mousePos);
            }
        }
    }
}
