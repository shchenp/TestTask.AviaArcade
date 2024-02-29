using System.Collections.Generic;
using UnityEngine;

public class FireManager : MonoBehaviour
    {
        [SerializeField] private List<Gun> _guns;
        [SerializeField] private Transform _player;
        [SerializeField] private Transform _aim;
        
        [SerializeField] private Bullet _bulletPrefab;
        [SerializeField] private int _bulletsCount;

        [SerializeField] private ShootEffect _effectPrefab;
        
        private MonoBehaviourPool<Bullet> _bulletsPool;
        private MonoBehaviourPool<ShootEffect> _effectsPool;

        private void Awake()
        {
            _bulletsPool = new MonoBehaviourPool<Bullet>(_bulletPrefab, null, _bulletsCount);

            var effectsCount = _bulletsCount;
            _effectsPool = new MonoBehaviourPool<ShootEffect>(_effectPrefab, _player, effectsCount);

            foreach (var gun in _guns)
            {
                gun.Initialize(_aim);
            }
        }

        public void Fire()
        {
            foreach (var gun in _guns)
            {
                var bullet = _bulletsPool.TakeWithoutSetActive();
                bullet.Hit += OnBulletHit;

                var effect = _effectsPool.Take();
                effect.Fired += OnBulletFired;
                
                effect.Play(gun.transform.position);
                gun.Fire(bullet);
            }
        }

        private void OnBulletHit(Bullet bullet)
        {
            bullet.Hit -= OnBulletHit;
            _bulletsPool.Release(bullet);
        }

        private void OnBulletFired(ShootEffect effect)
        {
            effect.Fired -= OnBulletFired;
            _effectsPool.Release(effect);
        }
    }