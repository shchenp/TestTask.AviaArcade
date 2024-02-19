using UnityEngine;
using System.Collections;

public class FPSFireManager : MonoBehaviour
{
    public ImpactInfo[] ImpactElemets = new ImpactInfo[0];
    [Space]
    public float BulletDistance = 100;
    public GameObject ImpactEffect;

    [SerializeField] private Transform _plane;

    public void Fire()
    {
        var impactEffectIstance = Instantiate(ImpactEffect, transform.position, transform.rotation);

        Destroy(impactEffectIstance, 4);
        
        RaycastHit hit;
        var ray = new Ray(_plane.position, transform.forward);
        if (Physics.Raycast(ray, out hit, BulletDistance)) 
        {
            var effect = GetImpactEffect(hit.transform.gameObject);
            if (effect==null)
                return;
            //var effectIstance = Instantiate(effect, hit.point, new Quaternion(), hit.transform) as GameObject;
            var effectIstance = Instantiate(effect, hit.transform);
            effectIstance.transform.position = hit.point;
            
            effectIstance.transform.LookAt(hit.point + hit.normal);
            Destroy(effectIstance, 20);
        }
    }
    
    [System.Serializable]
    public class ImpactInfo
    {
        public MaterialType.MaterialTypeEnum MaterialType;
        public GameObject ImpactEffect;
    }

    GameObject GetImpactEffect(GameObject impactedGameObject)
    {
        var materialType = impactedGameObject.GetComponent<MaterialType>();
        if (materialType==null)
            return null;
        foreach (var impactInfo in ImpactElemets)
        {
            if (impactInfo.MaterialType==materialType.TypeOfMaterial)
                return impactInfo.ImpactEffect;
        }
        return null;
    }
}
