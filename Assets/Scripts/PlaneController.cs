using UnityEngine;

public class PlaneController : MonoBehaviour
{
    [SerializeField]
    private Transform _splineTargetPoint;

    [SerializeField]
    private Transform _playerTargetPoint;
    
    [SerializeField] 
    private FixedJoystick _joystick;

    [SerializeField]
    private float _targetMaxOffset = 10;
    
    [SerializeField]
    private float _lookAtDistance = 40;

    [SerializeField]
    private float _speed = 15;

    // todo сделать движение более плавным
    private void Update()
    {
        _playerTargetPoint.position = _splineTargetPoint.position + _splineTargetPoint.TransformDirection(_joystick.Direction) * _targetMaxOffset;
        _playerTargetPoint.position += _splineTargetPoint.forward * _lookAtDistance;

        var direction = (_playerTargetPoint.position - transform.position).normalized;
        transform.LookAt(_playerTargetPoint);
        transform.Translate(direction * _speed * Time.deltaTime, Space.World);
    }
}