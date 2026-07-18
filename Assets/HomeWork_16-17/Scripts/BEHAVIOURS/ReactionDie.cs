using UnityEngine;
using EnemySystem;

public class ReactionDie : IBehaviour
{
    private IKillable _killable;

    public ReactionDie(IKillable killable)
    {
        _killable = killable;
    }

    public void Enable()
    {
        if (_killable != null)
        {
            _killable.Die();
        }
        else
        {
            Debug.LogError("Реакция смерть: объект не реализует IKillable.");
        }
    }

    public void Update() { }
    public void Disable() { }
}