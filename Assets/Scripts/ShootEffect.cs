using System;
using System.Collections.Generic;
using UnityEngine;

    public class ShootEffect : MonoBehaviour
    {
        public Action<ShootEffect> Fired;
        
        [SerializeField] private List<ParticleSystem> _particles;

        private float _time = 4f;
        private float _currentTime;
        private bool _isFired;

        public void Play(Vector3 position)
        {
            transform.SetParent(null);
            transform.position = position;
            
            foreach (var particle in _particles)
            {
                particle.Play();
            }

            _isFired = true;
        }

        private void Update()
        {
            if (_isFired)
            {
                _currentTime += Time.deltaTime;

                if (_currentTime >= _time)
                {
                    Fired?.Invoke(this);
                    
                    _currentTime = 0;
                    _isFired = false;
                }
            }
        }
    }