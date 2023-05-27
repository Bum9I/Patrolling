using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolBehaviour : MonoBehaviour
{
    [SerializeField] private float _speed = 1f;
    [SerializeField] private float _delayDuration;
    [SerializeField] private Transform[] _points;

    private float _currentTime;
    private float _delayTime;
    private float _distance;
    private float _travelTime;
    
    private int _startPoint;
    private int _nextPoint = 1;

    private void Awake()
    {
        _distance = Vector3.Distance(_points[_startPoint].transform.position, _points[_nextPoint].transform.position);
        _travelTime = _distance / _speed;
        _delayTime = _delayDuration;
    }

    private void Update()
    {
        _delayTime += Time.deltaTime;
        if (_delayTime < _delayDuration)
            return;
        
        _currentTime += Time.deltaTime;

        if (_nextPoint == 0)
            _startPoint = _points.Length - 1;
        var progress = _currentTime / _travelTime;
        var newPosition = Vector3.Lerp(_points[_startPoint].position, _points[_nextPoint].position, progress);
        transform.position = newPosition;

        if (_currentTime >= _travelTime)
        {
            _currentTime = 0f;
            _delayTime = 0f;

            if (_nextPoint == _points.Length - 1)
                _nextPoint = -1;
            else if (_nextPoint == 0)
                _startPoint = -1;
            _startPoint++;
            _nextPoint++;
            
            _distance = Vector3.Distance(_points[_startPoint].transform.position, _points[_nextPoint].transform.position);
            _travelTime = _distance / _speed;
        }
    }
}
