using DG.Tweening;
using UnityEngine;

public class ActionManager : MonoBehaviour
{
    [SerializeField] private PoolPlayers _poolPlayers;
    [SerializeField] private MovementSystem _movementSystem;
    [SerializeField] private GameObject _defealtAlert;
    [SerializeField] private GameObject _victoryAlert;
    [SerializeField] private Animator _gameCanvasAnimator;
    [SerializeField] private CompletedLevelTrigger _completedLevelTrigger;
    [SerializeField] private ParticleSystem[] _effects;
    [SerializeField] private Camera _camera;

    private bool _endGame;
    private int _hashDisableKey;

    private void Start()
    {
        _poolPlayers.FirstDrawing += StartMove;
        _poolPlayers.AllPlayersRemoved += EndGame;
        _completedLevelTrigger.LevelCompleted += VictoryGame;

        _hashDisableKey = Animator.StringToHash("Disable");
    }

    private void OnDestroy()
    {
        _poolPlayers.FirstDrawing -= StartMove;
        _poolPlayers.AllPlayersRemoved -= EndGame;
        _completedLevelTrigger.LevelCompleted -= VictoryGame;
    }

    private void StartMove()
    {
        _movementSystem.IsMove = true;

        for(int i = 0; i< _poolPlayers.GetPlayers.Count; i++)
        {
            _poolPlayers.GetPlayers[i].SetMove(true);
        }
    }

    private void EndGame()
    {
        if (_endGame) return;

        _endGame = true;

        _defealtAlert.SetActive(true);
        _gameCanvasAnimator.SetTrigger(_hashDisableKey);
        _movementSystem.IsMove = false;
    }

    private void VictoryGame()
    {
        if (_endGame) return;

        _endGame = true;

        _victoryAlert.SetActive(true);
        _gameCanvasAnimator.SetTrigger(_hashDisableKey);
        _movementSystem.IsMove = false;

        for (int i = 0; i < _poolPlayers.GetPlayers.Count; i++)
        {
            _poolPlayers.GetPlayers[i].SetMove(false);
            _poolPlayers.GetPlayers[i].SetVictory();
        }

        for(int i = 0; i < _effects.Length; i++)
        {
            _effects[i].Play();
        }

        _camera.transform.DOMove(new Vector3(_camera.transform.position.x, _camera.transform.position.y, -9), 0.5f);
    }
}
