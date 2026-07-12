using UnityEngine;
using EnemySystem;

public class ReactionChase : IBehaviour
{
    private Transform _transform;
    private Transform _target;
    private float _speed;

    public ReactionChase(Transform transform, Transform target, float speed)
    {
        _transform = transform;
        _target = target;
        _speed = speed;
    }

    public void Enable() { }

    public void Update()
    {
        if (_target == null) return;
        Vector3 direction = (_target.position - _transform.position).normalized;
        direction.y = 0f;
        _transform.position += direction * _speed * Time.deltaTime;
    }

    public void Disable() { }
}