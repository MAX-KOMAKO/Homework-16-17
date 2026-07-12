using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Vector3 _offset = new Vector3(0f, 10f, -10f);
    private Transform _target;

    private void Start()
    {
        _target = GameManager.HeroTransform;
        if (_target == null)
        {
            Hero hero = FindObjectOfType<Hero>();
            if (hero != null)
            {
                _target = hero.transform;
                GameManager.HeroTransform = _target;
            }
            else
            {
                Debug.LogError("Камера: Герой не найден.");
            }
        }
    }

    private void LateUpdate()
    {
        if (_target != null)
        {
            transform.position = _target.position + _offset;
        }
    }
}