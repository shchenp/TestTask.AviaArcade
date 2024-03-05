using UnityEngine;

public class EffectsContainer : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem[] _particles;

    public void Play()
    {
        foreach (var particle in _particles)
        {
            particle.Play();
        }
    }
}