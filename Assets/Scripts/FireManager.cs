using System.Collections.Generic;
using UnityEngine;

    public class FireManager : MonoBehaviour
    {
        [SerializeField] private List<Gun> _guns;

        public void Fire()
        {
            foreach (var gun in _guns)
            {
                gun.Fire();
            }
        }
    }