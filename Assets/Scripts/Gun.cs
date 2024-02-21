using UnityEngine;
using UnityEngine.Serialization;

public class Gun : MonoBehaviour
{
    [SerializeField] private GameObject _shootEffect;
    [SerializeField] private Transform _plane;
    [SerializeField] private Transform _muzzle;
    [SerializeField] private Bullet _bulletPrefab;

    public void Fire()
    {
        var aim = _plane.transform.position + Vector3.forward;
        
        var impactEffectInstance = Instantiate(_shootEffect, transform.position, transform.rotation);
        Destroy(impactEffectInstance, 4);
    
        ShootBullet(aim);
    }

    private void ShootBullet(Vector3 aim)
    {
        var bullet = Instantiate(_bulletPrefab, _muzzle);
        bullet.Initialize(aim);
    }
}
