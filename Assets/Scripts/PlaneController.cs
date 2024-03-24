using Dreamteck.Splines;
using UnityEngine;

public class PlaneController : MonoBehaviour
{
    [SerializeField]
    private SplineFollower _splineTarget;

    [SerializeField]
    private Transform _playerTargetPoint;
    
    [SerializeField] 
    private FixedJoystick _joystick;

    [SerializeField]
    private WingsController _wingsController;

    [SerializeField]
    private float _targetMaxOffset = 10;
    
    [SerializeField]
    private float _lookAtDistance = 40;

    [SerializeField]
    private float _speed = 15;
    
    [SerializeField]
    private AudioSource _engineAudio;

    private Transform _splineTargetPoint;
    private bool _isGameStarted;

    private void Awake()
    {
        _splineTargetPoint = _splineTarget.transform;
        _playerTargetPoint.position = _splineTargetPoint.position;
        _splineTarget.followSpeed = _speed;
        
        GameManager.Instance.SetPlayer(this);
    }

    private void Update()
    {
        if (!_isGameStarted)
        {
            return;
        }
        
        Move();
        
    }

    private void FixedUpdate()
    {
        _wingsController.Rotate(_joystick.Direction);
    }

    private void Move()
    {
        var nextPlayerTargetPointPosition = 
            _splineTargetPoint.position + _splineTargetPoint.TransformDirection(_joystick.Direction) * _targetMaxOffset;
        nextPlayerTargetPointPosition += _splineTargetPoint.forward * _lookAtDistance;

        var nextPointDirection = (nextPlayerTargetPointPosition - _playerTargetPoint.position).normalized;
        var distance = _speed * Time.deltaTime;
        _playerTargetPoint.Translate(nextPointDirection * distance, Space.World);
        
        transform.LookAt(_playerTargetPoint);

        var direction = (_playerTargetPoint.position - transform.position).normalized;
        transform.Translate(direction * distance, Space.World);
    }

    public void OnGameStarted()
    {
        _isGameStarted = true;
        _splineTarget.enabled = true;
        
        _engineAudio.Play();
    }
}