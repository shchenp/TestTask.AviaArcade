using System;
using UnityEngine;

    //TODO Разобраться с артефактами трейлов
    public class Bullet : MonoBehaviour
    {
        public Action<Bullet> Hit;
        
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private TrailRenderer _trailRenderer;
        [SerializeField] private float _force;
        
        private bool _isHit;
        private float _currentTime;
        private float _time = 4f;

       public void Initialize(Transform parent, Transform aim)
       {
           SetOn(parent);
           FlyTo(aim);
       }

       private void OnDisable()
       {
           //transform.SetParent(null);
           _trailRenderer.Clear();
           
           transform.position = Vector3.zero;
           transform.rotation = Quaternion.identity;
           _rigidbody.velocity = Vector3.zero;
       }

       private void SetOn(Transform parent)
       {
           transform.SetParent(parent, false);
           gameObject.SetActive(true);
       }

        private void FlyTo(Transform aim)
        {
            var direction = (aim.position - transform.position).normalized;
            _rigidbody.AddForce(direction * _force, ForceMode.Impulse);
        }

        private void Update()
        {
            var ray = new Ray(transform.position, transform.forward);
            
            if (Physics.Raycast(ray, out var hit, 2f) && !_isHit)
            {
                if (hit.transform.gameObject.CompareTag("Enemy"))
                {
                    var enemy = hit.transform.GetComponent<Enemy>();
                    
                    var effect = Instantiate(enemy.ImpactEffectPrefab, enemy.transform);
                    
                    effect.transform.position = hit.point;
                    effect.transform.LookAt(hit.point + hit.point.normalized);

                    _isHit = true;
                    _rigidbody.Sleep();
                }
            }
            
            // TODO изменить реализацию исчезновения следов от пуль
            
            if (_isHit)
            {
                _currentTime += Time.deltaTime;

                if (_currentTime >= _time)
                {
                    Hit?.Invoke(this);
                    
                    _currentTime = 0;
                    _isHit = false;
                }
            }
        }
    }