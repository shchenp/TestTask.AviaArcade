using System;
using UnityEngine;

    public class Bullet : MonoBehaviour
    {
        public Action<Bullet> Hit;
        
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private float _force;
        
        private bool _isHit;
        private float _currentTime;
        private float _time = 4f;

       public void Initialize(Transform parent, Transform aim)
       {
           SetOn(parent);
        
           Fly(aim);
       }
       
       private void SetOn(Transform parent)
       {
           transform.SetParent(null);
           
           transform.position = Vector3.zero;
           transform.rotation = Quaternion.identity;
           _rigidbody.velocity = Vector3.zero;
           
           transform.SetParent(parent, false);
       }

        private void Fly(Transform aim)
        {
            var direction = (aim.position - transform.position).normalized;
            _rigidbody.AddForce(direction * _force, ForceMode.Impulse);
        }

        private void Update()
        {
            var ray = new Ray(transform.position, transform.forward);
            
            if (Physics.Raycast(ray, out var hit, 2f))
            {
                if (hit.transform.gameObject.CompareTag("Enemy"))
                {
                    var enemy = hit.transform.GetComponent<Enemy>();
                    
                    var effect = Instantiate(enemy.ImpactEffectPrefab, enemy.transform);
                    
                    effect.transform.position = hit.point;
                    effect.transform.LookAt(hit.point + hit.point.normalized);

                    _isHit = true;
                }
            }
            
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