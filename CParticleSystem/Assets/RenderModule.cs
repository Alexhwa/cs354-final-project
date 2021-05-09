using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RenderModule : IModule
{
    [SerializeField] private bool billboarding;

    public void InitParticle(Particle particle, CParticleSystem system)
    {
        if (billboarding)
        {
            particle.Set<Vector3>("Rotation", Vector3.zero);
        }
    }
    public void Update(HashSet<Particle> aliveParticles)
    {
        if (billboarding)
        {
            foreach (Particle p in aliveParticles)
            {
                var lookDir = Quaternion.LookRotation(-Camera.main.transform.forward);
                p.Set<Vector3>("Rotation", lookDir.eulerAngles);
            }
        }
    }
}
