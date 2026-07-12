using UnityEngine;
using EnemySystem;

public class IdlePatrol : IBehaviour
{
    private Transform _transform;
    private Transform[] _points;
    private float _speed;
    private int _currentIndex;

    public IdlePatrol(Transform transform, Transform[] points, float speed)
    {
        _transform = transform;
        _points = points;
        _speed = speed;
        _currentIndex = 0;
    }

    public void Enable()
    {
        if (_points == null || _points.Length == 0)
        {
            Debug.LogWarning("Патрулирование: точки патрулирования не назначены.");
        }
    }

    public void Update()
    {
        if (_points == null || _points.Length == 0) return;

        Transform target = _points[_currentIndex];
        Vector3 direction = (target.position - _transform.position).normalized;
        _transform.position += direction * _speed * Time.deltaTime;

        if (Vector3.Distance(_transform.position, target.position) < 0.5f)
        {
            _currentIndex = (_currentIndex + 1) % _points.Length;
        }
    }

    public void Disable() { }
}