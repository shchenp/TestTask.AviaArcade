using UnityEngine;

public class Enemy : MonoBehaviour
    {
        public GameObject ImpactEffectPrefab => _impactEffectPrefab;
        
        [SerializeField] private GameObject _impactEffectPrefab;
    }