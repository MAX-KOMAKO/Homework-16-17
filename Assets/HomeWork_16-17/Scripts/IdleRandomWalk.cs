using UnityEngine;
using EnemySystem;

public class IdleRandomWalk : IBehaviour
{
    private Transform _transform;
    private float _speed;
    private Vector3 _direction;
    private float _changeTimer;

    public IdleRandomWalk(Transform transform, float speed)
    {
        _transform = transform;
        _speed = speed;
        _direction = Random.insideUnitSphere;
        _direction.y = 0f;
        _direction.Normalize();
        _changeTimer = 0f;
    }

    public void Enable() { }

    public void Update()
    {
        _changeTimer += Time.deltaTime;
        if (_changeTimer >= 1f)
        {
            _direction = Random.insideUnitSphere;
            _direction.y = 0f;
            _direction.Normalize();
            _changeTimer = 0f;
        }

        _transform.position += _direction * _speed * Time.deltaTime;
    }

    public void Disable() { }
}