using Dreamteck.Splines;
using UnityEngine;

    public class EnemyPhysicsSettings : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private MeshCollider _collider;
        [SerializeField] private SplineFollower _splineFollower;

        public void Initialize(float speed)
        {
            _splineFollower.followSpeed = speed;
        }
        
        public void SetDefault()
        {
            _rigidbody.velocity = Vector3.zero;
            _splineFollower.follow = false;
            _collider.convex = true;
            _rigidbody.isKinematic = false;
        }
    }