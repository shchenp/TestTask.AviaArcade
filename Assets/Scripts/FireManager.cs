using UnityEngine;

public class FireManager : MonoBehaviour
{
    [SerializeField] private GameObject ShootEffect;
    [SerializeField] private Transform _plane;
    [SerializeField] private Transform _muzzle;
    [SerializeField] private Bullet _bulletPrefab;

    public void Fire()
    {
        var aim = _plane.transform.position + Vector3.forward;
        
        var impactEffectInstance = Instantiate(ShootEffect, transform.position, transform.rotation);
        Destroy(impactEffectInstance, 4);
    
        ShootBullet(aim);
    }

    private void ShootBullet(Vector3 aim)
    {
        var bullet = Instantiate(_bulletPrefab, _muzzle);
        bullet.Initialize(aim);
    }
}
