using UnityEngine;

public class FallingDownPoint : MonoBehaviour
{
    [SerializeField] private Vector3 _offset;

    private float _speed;

    public void Initialize(Transform parent, float speed)
    {
        transform.position = parent.position;
        transform.rotation = parent.rotation;
        
        transform.Translate(_offset, Space.Self);
        _speed = speed;
    }

    private void Update()
    {
        var distance = _speed * Time.deltaTime;
        transform.Translate(Vector3.down * distance);
    }
}
