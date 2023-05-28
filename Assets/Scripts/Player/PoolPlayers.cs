using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PoolPlayers : MonoBehaviour
{
    public Action FirstDrawing;
    public Action<int> PlayerRemoved;
    public Action<int> PlayerAdded;
    public Action AllPlayersRemoved;

    [SerializeField] private List<Player> _players;
    [SerializeField] private Drawing _drawing;
    [SerializeField] private GameObject _point;

    private List<Points> _points = new List<Points>();
    private bool _isFirstDraw;
    private int _countPlayers;

    public List<Player> GetPlayers => _players;

    private void Start()
    {
        _drawing.EndDrawing += SetPosition;
        _countPlayers = _players.Count;

        for(int i = 0; i < _players.Count; i++)
        {
            _players[i].Dieded += RemovePlayer;
        }
    }

    private void OnDestroy()
    {
        _drawing.EndDrawing -= SetPosition;

        for (int i = 0; i < _players.Count; i++)
        {
            _players[i].Dieded -= RemovePlayer;
        }
    }

    private void RemovePlayer(Player player)
    {
        _players.Remove(player);
        _countPlayers--;

        PlayerRemoved?.Invoke(_countPlayers);

        if(_countPlayers <= 0)
        {
            AllPlayersRemoved?.Invoke();
        }
    }

    public void Add(Player player)
    {
        _players.Add(player);
        _countPlayers++;
        player.Dieded += RemovePlayer;

        PlayerAdded?.Invoke(_countPlayers);
    }

    private void DestroyPoints()
    {
        for(int i = 0; i < _points.Count; i++)
        {
            if(_points[i].point != null)
                Destroy(_points[i].point);
        }

        _points = new List<Points>();
    }

    private void InitPoints(LineRenderer line)
    {
        if (!_isFirstDraw)
        {
            _isFirstDraw = true;
            FirstDrawing?.Invoke();
        }

        DestroyPoints();

        int playerLine = line.positionCount / _players.Count;
        int number = 0;

        if (playerLine == 0)
        {
            playerLine = 1;
        }

        for (int i = 0; i < line.positionCount; i++)
        {
            if (i == number * (playerLine + 1))
            {
                Points point = new Points(line.GetPosition(i), _point);
                _points.Add(point);

                number++;
            }
        }
    }

    private void SetPosition(LineRenderer line)
    {
        InitPoints(line);

        int indexPointLine = 0;
        int countAddedPlayers = 0;

        while (countAddedPlayers < _players.Count)
        {
            if (indexPointLine >= _points.Count)
            {
                indexPointLine = 0;
            }

            if (!_points[indexPointLine].isAdd)
            {
                if (_points.Count >= _players.Count)
                {
                    _points[indexPointLine].isAdd = true;
                }

                Vector3 pos = new Vector3(_points[indexPointLine].position.x, 0.505f, _points[indexPointLine].position.z + 6);
                _players[countAddedPlayers].transform.DOMove(pos, 0.2f);

                countAddedPlayers++;
            }

            indexPointLine++;
        }
    }
}

public class Points: MonoBehaviour
{
    public Vector3 position;
    public bool isAdd;
    public GameObject point;

    public Points(Vector3 pos, GameObject point)
    {
        //this.point = Instantiate(point, new Vector3(pos.x, 1f, pos.z), Quaternion.identity);

        position = pos;
        isAdd = false;
    }
}
