using UnityEngine;
using UnityEngine.UI;

public class PlayerCount : MonoBehaviour
{
    [SerializeField] private Text _countPlayer;
    [SerializeField] private PoolPlayers _poolPlayers;
    [SerializeField] private PulseUI _pulseUI;
    [SerializeField] private Color _decColor;
    [SerializeField] private Color _addColor;

    private int _count;
    private Color _defaultColor;

    private void Start()
    {
        _poolPlayers.PlayerRemoved += Dec;
        _poolPlayers.PlayerAdded += Add;

        _defaultColor = _countPlayer.color;

        Init(_poolPlayers.GetPlayers.Count);
    }

    private void OnDestroy()
    {
        _poolPlayers.PlayerRemoved -= Dec;
        _poolPlayers.PlayerAdded -= Add;
    }

    private void UpdateText()
    {
        _countPlayer.text = _count.ToString();
    }

    private void SetColor(Color color)
    {
        _countPlayer.color = color;
    }

    public void Init(int count)
    {
        _count = count;
        UpdateText();
    }

    public void Add(int count)
    {
        _count = count;
        UpdateText();

        _countPlayer.color = _addColor;
        _pulseUI.Pulse(() =>
        {
            _countPlayer.color = _defaultColor;
        });
    }

    public void Dec(int count)
    {
        _count = count;

        if(_count <= 0)
        {
            _count = 0;
        }

        UpdateText();

        _countPlayer.color = _decColor;
        _pulseUI.Pulse(() =>
        {
            _countPlayer.color = _defaultColor;
        });
    }
}
