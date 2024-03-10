using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Action<Bullet> Hit;
    
    [SerializeField] private TrailRenderer _trailRenderer;
    [SerializeField] private float _force;
    [SerializeField] private float _lifeTime;
    
   public void Initialize(Transform parent)
   {
       transform.position = parent.position;
       transform.rotation = parent.rotation;
       
       this.DoAfter(() => Hit?.Invoke(this), _lifeTime);
   }

   private void OnDisable()
   {
       _trailRenderer.Clear();
   }

   private void MoveForward(float distance)
   {
       transform.position += transform.forward * distance;
   }

    private void Update()
    {
        var flyDistance = _force * Time.deltaTime;
        MoveForward(flyDistance);
        
        var ray = new Ray(transform.position, transform.forward);
        if (Physics.Raycast(ray, out var hit, flyDistance))
        {
            if (hit.transform.gameObject.CompareTag(GlobalConstants.EnemyTag))
            {
                var enemy = hit.transform.GetComponent<Enemy>();
                enemy.OnHit(hit.point, hit.normal);
                
                Hit?.Invoke(this);
            }
        }
    }
}