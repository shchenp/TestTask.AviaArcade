using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private Transform _muzzle;

    private Transform _aim;

    public void Initialize(Transform aim)
    {
        _aim = aim;
        _muzzle.LookAt(aim);
    }

    public void Fire(Bullet bullet)
    {
        bullet.Initialize(_muzzle, _aim);
    }
}
