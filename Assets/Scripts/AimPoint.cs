using UnityEngine;

[RequireComponent(typeof(Collider))]
public class AimPoint : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            Debug.Log("AimPoint OnTriggerEnter");
            var bullet = other.GetComponent<Bullet>();
            bullet.Hit?.Invoke(bullet);
        }
    }
}
