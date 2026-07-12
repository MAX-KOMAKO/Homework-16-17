using UnityEngine;
using EnemySystem;

public class Enemy : MonoBehaviour
{
    private const float SPEED = 3f;
    private const float AGGRO_DISTANCE = 10f;

    [SerializeField] private ParticleSystem _deathEffect;

    private IBehaviour _idleBehaviour;
    private IBehaviour _reactionBehaviour;
    private IBehaviour _currentBehaviour;
    private Transform _heroTransform;
    private bool _isDead;

    public void Initialize(IdleBehaviourType idleType, ReactionBehaviourType reactionType, Transform[] patrolPoints)
    {
        _heroTransform = GameManager.HeroTransform;
        if (_heroTransform == null)
        {
            Debug.LogError("Враг: Герой не найден.");
            enabled = false;
            return;
        }

        _idleBehaviour = CreateIdleBehaviour(idleType, patrolPoints);
        _reactionBehaviour = CreateReactionBehaviour(reactionType);
        _currentBehaviour = _idleBehaviour;
        _currentBehaviour.Enable();
    }

    private IBehaviour CreateIdleBehaviour(IdleBehaviourType type, Transform[] patrolPoints)
    {
        switch (type)
        {
            case IdleBehaviourType.Stand:
                return new IdleStand();
            case IdleBehaviourType.Patrol:
                return new IdlePatrol(transform, patrolPoints, SPEED);
            case IdleBehaviourType.RandomWalk:
                return new IdleRandomWalk(transform, SPEED);
            default:
                return new IdleStand();
        }
    }

    private IBehaviour CreateReactionBehaviour(ReactionBehaviourType type)
    {
        switch (type)
        {
            case ReactionBehaviourType.Flee:
                return new ReactionFlee(transform, _heroTransform, SPEED);
            case ReactionBehaviourType.Chase:
                return new ReactionChase(transform, _heroTransform, SPEED);
            case ReactionBehaviourType.Die:
                return new ReactionDie(transform, _deathEffect);
            default:
                return new ReactionFlee(transform, _heroTransform, SPEED);
        }
    }

    private void Update()
    {
        if (_isDead) return;
        if (_heroTransform == null) return;

        float distance = Vector3.Distance(transform.position, _heroTransform.position);
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