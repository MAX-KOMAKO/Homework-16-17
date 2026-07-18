using UnityEngine;
using EnemySystem;

public class EnemySpawnPoint : MonoBehaviour
{
    [SerializeField] private IdleBehaviourType _idleType;
    [SerializeField] private ReactionBehaviourType _reactionType;
    [SerializeField] private Transform[] _patrolPoints;
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private Transform _heroTransform;

    private void Start()
    {
        if (_enemyPrefab == null)
        {
            Debug.LogError("Точка спавна: префаб врага не назначен.");
            return;
        }
        if (_heroTransform == null)
        {
            Debug.LogError("Точка спавна: трансформ героя не назначен.");
            return;
        }

        GameObject enemyObj = Instantiate(_enemyPrefab, transform.position, Quaternion.identity);
        Enemy enemy = enemyObj.GetComponent<Enemy>();
        if (enemy == null)
        {
            Debug.LogError("Точка спавна: префаб врага не содержит компонент Enemy.");
            Destroy(enemyObj);
            return;
        }

        IBehaviour idleBehaviour = CreateIdleBehaviour(_idleType, enemy.transform, _patrolPoints);
        IBehaviour reactionBehaviour = CreateReactionBehaviour(_reactionType, enemy, _heroTransform);

        enemy.SetBehaviours(idleBehaviour, reactionBehaviour);
    }

    private IBehaviour CreateIdleBehaviour(IdleBehaviourType type, Transform enemyTransform, Transform[] patrolPoints)
    {
        switch (type)
        {
            case IdleBehaviourType.Stand:
                return new IdleStand();
            case IdleBehaviourType.Patrol:
                return new IdlePatrol(enemyTransform, patrolPoints, Enemy.SPEED);
            case IdleBehaviourType.RandomWalk:
                return new IdleRandomWalk(enemyTransform, Enemy.SPEED);
            default:
                return new IdleStand();
        }
    }

    private IBehaviour CreateReactionBehaviour(ReactionBehaviourType type, IKillable killable, Transform heroTransform)
    {
        switch (type)
        {
            case ReactionBehaviourType.Flee:
                return new ReactionFlee(killable as Component, heroTransform, Enemy.SPEED);
            case ReactionBehaviourType.Chase:
                return new ReactionChase(killable as Component, heroTransform, Enemy.SPEED);
            case ReactionBehaviourType.Die:
                return new ReactionDie(killable);
            default:
                return new ReactionFlee(killable as Component, heroTransform, Enemy.SPEED);
        }
    }
}