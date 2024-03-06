using System.Collections;
using Dreamteck.Splines;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private EffectsContainer _impactEffectPrefab;
    [SerializeField] private float _impactEffectTime;
    [Space]
    [SerializeField] private float _health;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private SplineFollower _splineFollower;
    [SerializeField] private AudioSource _crashAudio;
    [SerializeField] private Transform _headPoint;
    [SerializeField] private Transform _forcePosition;

    private MonoBehaviourPool<EffectsContainer> _impactEffectsPool;


    private void Awake()
    {
        _impactEffectsPool = new MonoBehaviourPool<EffectsContainer>(_impactEffectPrefab, transform);
    }

    public void OnHit(Vector3 hitPoint, Vector3 hitNormal)
    {
        var effect = _impactEffectsPool.Take();
        effect.transform.position = hitPoint;
        effect.transform.LookAt(hitPoint + hitNormal);
        effect.Play();

        this.DoAfter(() => _impactEffectsPool.Release(effect), _impactEffectTime);

        _health--;

        if (_health <= 0)
        {
            _rigidbody.centerOfMass = _headPoint.localPosition;
            _rigidbody.velocity = Vector3.zero;
            _splineFollower.follow = false;
            _rigidbody.useGravity = true;
            _rigidbody.isKinematic = false;
            
            _crashAudio.Play();
            
            StartCoroutine(MoveDown());
            StartCoroutine(DoRotate());
        }
    }

    private IEnumerator MoveDown()
    {
        var count = 0f;

        while (count < 45)
        {
            transform.Rotate(transform.right, 0.1f, Space.World);
            count += 0.1f;
            
            yield return null;
        }
    }

    private IEnumerator DoRotate()
    {
        var currentTime = 0f;
        while (currentTime <= 10f)
        {
            transform.Rotate(transform.forward, 0.5f, Space.World);

            currentTime += Time.deltaTime;
            yield return null;
        }
    }
}