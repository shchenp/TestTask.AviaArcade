using System.Collections.Generic;
using UnityEngine;

public class FireManager : MonoBehaviour
    {
        [SerializeField] private List<Gun> _guns;
        [SerializeField] private Transform _aim;
        
        [SerializeField] private Bullet _bulletPrefab;
        
        private MonoBehaviourPool<Bullet> _bulletsPool;

        private void Awake()
        {
            _bulletsPool = new MonoBehaviourPool<Bullet>(_bulletPrefab, null);

            foreach (var gun in _guns)
            {
                gun.Initialize(_aim, _bulletsPool);
            }
        }

        public void Fire()
        {
            foreach (var gun in _guns)
            {
                gun.Fire();
            }
        }
    }