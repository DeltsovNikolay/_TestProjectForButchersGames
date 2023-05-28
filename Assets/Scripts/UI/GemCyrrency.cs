using UnityEngine;
using UnityEngine.UI;

public class GemCyrrency : MonoBehaviour
{
    [SerializeField] private Text _gemCount;
    [SerializeField] private PulseUI _pulseUI;
    [SerializeField] private Color _decColor;
    [SerializeField] private Color _addColor;

    private int _count;
    private Color _defaultColor;

    private void Start()
    {
        _count = 0;
        _defaultColor = _gemCount.color;

        Init(_count);
    }

    private void UpdateText()
    {
        _gemCount.text = _count.ToString();
    }

    private void SetColor(Color color)
    {
        _gemCount.color = color;
    }

    public void Init(int count)
    {
        _count = count;
        UpdateText();
    }

    public void Add(int count)
    {
        _count += count;
        UpdateText();

        _gemCount.color = _addColor;
        _pulseUI.Pulse(() =>
        {
            _gemCount.color = _defaultColor;
        });
    }

    public void Dec(int count)
    {
        _count = count;

        if (_count <= 0)
        {
            _count = 0;
        }

        UpdateText();

        _gemCount.color = _decColor;
        _pulseUI.Pulse(() =>
        {
            _gemCount.color = _defaultColor;
        });
    }
}
