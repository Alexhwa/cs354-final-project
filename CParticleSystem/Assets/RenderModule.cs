using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderModule : IModule
{
    [SerializeField] private bool billboarding;

    public override void InitParticle(Particle particle)
    {
        if (billboarding)
        {
            particle.Set<Vector3>("Rotation", Vector3.zero);
        }
    }
    public override void UpdateParticles(HashSet<Particle> aliveParticles)
    {
        if (billboarding)
        {
            var camera = Camera.main;

            foreach (Particle p in aliveParticles)
            {
                var lookDir = Quaternion.LookRotation(-camera.transform.forward);
                p.Set<Vector3>("Rotation", lookDir.eulerAngles);
            }
        }
    }
}
