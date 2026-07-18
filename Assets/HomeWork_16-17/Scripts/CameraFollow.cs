using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private Vector3 _offset = new Vector3(0f, 10f, -10f);

    private void LateUpdate()
    {
        if (_target != null)
        {
            transform.position = _target.position + _offset;
        }
        else
        {
            Debug.LogWarning("Камера: цель не назначена.");
        }
    }
}