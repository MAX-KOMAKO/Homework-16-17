using UnityEngine;
using EnemySystem;

public class EnemySpawnPoint : MonoBehaviour
{
    [SerializeField] private IdleBehaviourType _idleType;
    [SerializeField] private ReactionBehaviourType _reactionType;
    [SerializeField] private Transform[] _patrolPoints;
    [SerializeField] private GameObject _enemyPrefab;

    private void Start()
    {
        if (_enemyPrefab == null)
        {
            Debug.LogError("Точка спавна: префаб врага не назначен.");
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

        enemy.Initialize(_idleType, _reactionType, _patrolPoints);
    }
}