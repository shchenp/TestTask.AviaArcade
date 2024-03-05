using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private Transform _muzzle;
    [SerializeField] private ParticleSystem[] _muzzleFlashEffects;

    private MonoBehaviourPool<Bullet> _bulletsPool;

    public void Initialize(Transform aim, MonoBehaviourPool<Bullet> bulletsPool)
    {
        _bulletsPool = bulletsPool;
        _muzzle.LookAt(aim);
    }

    public void Fire()
    {
        var bullet = _bulletsPool.Take();
        bullet.Hit += OnBulletHit;
        
        bullet.Initialize(_muzzle);

        PlayMuzzleEffects();
    }

    private void PlayMuzzleEffects()
    {
        foreach (var muzzleFlashEffect in _muzzleFlashEffects)
        {
            muzzleFlashEffect.Play();
        }
    }

    private void OnBulletHit(Bullet bullet)
    {
        bullet.Hit -= OnBulletHit;
        _bulletsPool.Release(bullet);
    }
}
