using UnityEngine;

public class WingsController : MonoBehaviour
{
    [SerializeField] private Flap _leftEleron;
    [SerializeField] private Flap _rightEleron;
    [SerializeField] private Flap _ruli;
    [SerializeField] private Rudder _rudder;
    [Space]
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _maxAngle;

    private void Awake()
    {
        _leftEleron.Initialize(_rotationSpeed, _maxAngle);
        _rightEleron.Initialize(_rotationSpeed, _maxAngle);
        _ruli.Initialize(_rotationSpeed, _maxAngle);
        _rudder.Initialize(_rotationSpeed, _maxAngle);
    }

    public void Rotate(Vector2 direction)
    {
        _leftEleron.RotateTo(direction.y);
        _rightEleron.RotateTo(direction.y);
        _ruli.RotateTo(direction.y);
        
        _rudder.RotateTo(-direction.x);
    }
}
