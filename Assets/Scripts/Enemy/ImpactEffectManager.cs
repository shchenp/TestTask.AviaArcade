using UnityEngine;

    public class ImpactEffectManager : MonoBehaviour
    {
        [SerializeField] private EffectsContainer _impactEffectPrefab;
        [SerializeField] private float _impactEffectTime;
        
        private MonoBehaviourPool<EffectsContainer> _impactEffectsPool;
        
        private void Awake()
        {
            _impactEffectsPool = new MonoBehaviourPool<EffectsContainer>(_impactEffectPrefab, transform);
        }
        
        public void PlayImpactEffect(Vector3 hitPoint, Vector3 hitNormal)
        {
            var effect = _impactEffectsPool.Take();
            effect.transform.position = hitPoint;
            effect.transform.LookAt(hitPoint + hitNormal);
            effect.Play();

            this.DoAfter(() => _impactEffectsPool.Release(effect), _impactEffectTime);
        }
    }