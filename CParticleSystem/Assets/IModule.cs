using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class IModule
{
    public virtual void InitParticle(Particle particle, CParticleSystem system) {}
    public virtual void Update(HashSet<Particle> aliveParticles) {}
}