using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IModule : MonoBehaviour
{
    public virtual void InitParticle(Particle particle) {}
    public virtual void UpdateParticles(HashSet<Particle> aliveParticles) {}
}