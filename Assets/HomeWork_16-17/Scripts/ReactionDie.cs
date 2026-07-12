using UnityEngine;
using EnemySystem;

public class ReactionDie : IBehaviour
{
    private Transform _transform;
    private ParticleSystem _deathEffect;

    public ReactionDie(Transform transform, ParticleSystem deathEffect)
    {
        _transform = transform;
        _deathEffect = deathEffect;
    }

    public void Enable()
    {
        Enemy enemy = _transform.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.Die();
        }
        else
        {
            Debug.LogError("Реакция смерть: компонент Enemy не найден.");
        }
    }

    public void Update() { }

    public void Disable() { }
}