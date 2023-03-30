using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] private Transform _player;

    [SerializeField] private GameObject[] _obstacles;

    [Range(-50, 50)]
    [SerializeField] private float _leftMargin, _rightMargin;

    [SerializeField] private uint _obstacleXAmount, _obstacleYAmount;

    [SerializeField] private float _obstacleXGap, _obstacleYGap, _gapRandomRange;

    [SerializeField] private float _playerDistanceToSpawn = 20f;
    private float _heightThreshold;
    
    private Vector2 _lastPos;

    private void Start()
    {
        _lastPos = new Vector2(_leftMargin, 0f);
        _heightThreshold = _playerDistanceToSpawn;

        SpawnObstacles();
    }

    private void Update()
    {
        if (_player.position.y >= _heightThreshold)
        {
            SpawnObstacles();
            _heightThreshold += _playerDistanceToSpawn;
        }
    }

    private void SpawnObstacles()
    {
        for (int x = 0; x < _obstacleXAmount; x++)
        {
            for (int y = 0; y < _obstacleYAmount; y++)
            {
                var newPos = new Vector2(
                    _lastPos.x + _obstacleXGap + Random.Range(_gapRandomRange * -1, _gapRandomRange),
                    _lastPos.y + Random.Range(_gapRandomRange * -1, _gapRandomRange));

                if (newPos.x >= _rightMargin)
                {
                    Debug.LogWarning("Reached right margain while generating obstacles.");
                    continue;
                }

                Instantiate(_obstacles[0], newPos, Quaternion.identity, transform);
                _lastPos = newPos;
            }
            _lastPos.x = _leftMargin;
            _lastPos.y += _obstacleYGap;
        }
    }

    private void DestroyObstacles()
    {
        for (int i = 0; i < transform.childCount; i++)
            Destroy(transform.GetChild(i).gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(new Vector2(_leftMargin, 0), new Vector2(_rightMargin, 0));

        Gizmos.color = Color.magenta;
        Gizmos.DrawLine(new Vector2(_leftMargin, _heightThreshold), new Vector2(_rightMargin, _heightThreshold));
    }
}
