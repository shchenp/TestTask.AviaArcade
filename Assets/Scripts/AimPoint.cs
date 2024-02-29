using UnityEngine;

[RequireComponent(typeof(Collider))]
public class AimPoint : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            var bullet = other.GetComponent<Bullet>();
            bullet.Hit?.Invoke(bullet);
        }
    }
}
