using UnityEngine;
using EnemySystem;

public class ReactionFlee : IBehaviour
{
    private Transform _transform;
    private Transform _target;
    private float _speed;

    public ReactionFlee(Component enemyComponent, Transform target, float speed)
    {
        _transform = enemyComponent.transform;
        _target = target;
        _speed = speed;
    }

    public void Enable() { }

    public void Update()
    {
        if (_target == null) return;
        Vector3 direction = (_transform.position - _target.position).normalized;
        direction.y = 0f;
        _transform.position += direction * _speed * Time.deltaTime;
    }

    public void Disable() { }
}