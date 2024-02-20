using UnityEngine;

    public class Bullet : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private float _force;

        private Vector3 _aimPosition;
        
        public void Initialize(Vector3 aim)
        {
            var direction = (aim - transform.position).normalized;
            direction.z *= _force;
            
            _aimPosition = direction;
        }

        private void Start()
        {
            _rigidbody.AddRelativeForce(_aimPosition, ForceMode.Impulse);
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
                    
                    _rigidbody.Sleep();
                }
            }
        }
    }