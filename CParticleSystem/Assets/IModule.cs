using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IModule
{
    void InitParticle(Particle particle);
    void Update(HashSet<Particle> aliveParticles);
}
