using UnityEngine;
using EnemySystem;

public class Enemy : MonoBehaviour, IKillable
{
    public const float SPEED = 3f;
    public const float AGGRO_DISTANCE = 10f;

    [SerializeField] private ParticleSystem _deathEffect;

    private IBehaviour _idleBehaviour;
    private IBehaviour _reactionBehaviour;
    private IBehaviour _currentBehaviour;
    private bool _isDead;

    public void SetBehaviours(IBehaviour idle, IBehaviour reaction)
    {
        _idleBehaviour = idle;
        _reactionBehaviour = reaction;
        _currentBehaviour = _idleBehaviour;
        _currentBehaviour.Enable();
    }

    private void Update()
    {
        if (_isDead) return;

        Transform hero = GetHeroTransform();
        if (hero == null) return;

        float distance = Vector3.Distance(transform.position, hero.position);
        if (distance < AGGRO_DISTANCE)
        {
            SwitchToReaction();
        }
        else
        {
            SwitchToIdle();
        }

        _currentBehaviour.Update();
    }

    private Transform GetHeroTransform()
    {
        Hero hero = FindObjectOfType<Hero>();
        return hero != null ? hero.transform : null;
    }

    private void SwitchToIdle()
    {
        if (_currentBehaviour == _idleBehaviour) return;
        _currentBehaviour.Disable();
        _currentBehaviour = _idleBehaviour;
        _currentBehaviour.Enable();
    }

    private void SwitchToReaction()
    {
        if (_currentBehaviour == _reactionBehaviour) return;
        _currentBehaviour.Disable();
        _currentBehaviour = _reactionBehaviour;
        _currentBehaviour.Enable();
    }

    public void Die()
    {
        _isDead = true;
        _currentBehaviour.Disable();
        if (_deathEffect != null)
        {
            ParticleSystem effect = Instantiate(_deathEffect, transform.position, Quaternion.identity);
            effect.Play();
        }
        Destroy(gameObject);
    }
}