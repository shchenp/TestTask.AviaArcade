using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _health;
    [SerializeField] private float _speed;
    [SerializeField] private AudioSource _engineAudio;
    [Space]
    [SerializeField] private AudioSource _crashAudio;
    [SerializeField] private float _audioDelay;
    [Space]
    [SerializeField] private FallingDownPoint _fallingDownPoint;
    [SerializeField] private float _rotationValue;
    [SerializeField] private float _fallingDownTime;
    [SerializeField] private EnemyPhysicsSettings _settings;
    [SerializeField] private ImpactEffectManager _impactEffectManager;
    [Space]
    [SerializeField] private EffectsContainer _smokeEffects;
    [SerializeField] private EffectsContainer _explosionEffect;
    
    private bool isFallingDown;
    private float _currentTime;
    private float _explosionEffectTime;

    private void Awake()
    {
        _settings.Initialize(_speed);
        _explosionEffectTime = _audioDelay;
        
        GameManager.Instance.SetEnemy(this);
    }
    
    private void Update()
    {
        if (!isFallingDown)
        {
            return;
        }
        
        MoveToFallingDownPoint();
        Roll();
    }

    public void OnHit(Vector3 hitPoint, Vector3 hitNormal)
    {
        _impactEffectManager.PlayImpactEffect(hitPoint, hitNormal);

        _health--;

        if (_health <= 0 && !isFallingDown)
        {
            _settings.SetDefault();
            _smokeEffects.Play();

            isFallingDown = true;
            _fallingDownPoint.Initialize(transform, _speed);
            
            _crashAudio.PlayDelayed(_audioDelay);
        }
    }

    private void MoveToFallingDownPoint()
    {
        _currentTime += Time.deltaTime;
        
        var distance = _speed * Time.deltaTime;
        var newPosition =
            Vector3.MoveTowards(transform.position, _fallingDownPoint.transform.position, distance);
        transform.position = newPosition;
    
        if (_currentTime >= _fallingDownTime)
        {
            _explosionEffect.Play();
            
            this.DoAfter(() => gameObject.SetActive(false), _explosionEffectTime);
        }
    }
    
    private void Roll()
    {
        var rotation = Time.deltaTime * _rotationValue;
        transform.Rotate(Vector3.forward * rotation, Space.Self);
    }

    public void OnGameStarted()
    {
        _settings.ActivateSplineFollower();
        _engineAudio.Play();
    }
}