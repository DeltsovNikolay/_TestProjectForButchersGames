using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CompletedLevelTrigger : MonoBehaviour
{
    public Action LevelCompleted;

    [SerializeField] private Transform[] _points;
    [SerializeField] private PoolPlayers _poolPlayers;

    private List<PointFinish> _pointFinishes = new List<PointFinish>();
    private bool _isCompleted;

    private void Start()
    {
        for(int i = 0; i < _points.Length; i++)
        {
            PointFinish point = new PointFinish();
            point.transform = _points[i].transform;
            point.isAdd = false;

            _pointFinishes.Add(point);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            if (_isCompleted) return;

            _isCompleted = true;
            LevelCompleted?.Invoke();

            for(int i = 0; i < _poolPlayers.GetPlayers.Count; i++)
            {
                int index = 0;
                bool isAdd = false;

                while (!isAdd)
                {
                    if (!_pointFinishes[index].isAdd)
                    {
                        _poolPlayers.GetPlayers[i].transform.DOMove(_pointFinishes[index].transform.position, 0.3f);
                        _poolPlayers.GetPlayers[i].transform.DORotate(new Vector3(0f, -180f, 0f), 0.3f);

                        _pointFinishes[index].isAdd = true;

                        isAdd = true;
                    }

                    index++;

                    if(index >= _pointFinishes.Count)
                    {
                        index = 0;
                    }
                }
            }
        }
    }
}

public class PointFinish: MonoBehaviour
{
    public Transform transform;
    public bool isAdd;
}
