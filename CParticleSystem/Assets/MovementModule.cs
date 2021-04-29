using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MovementModule : IModule
{
    [SerializeField] private Vector3 velocity;
    [SerializeField] private Vector3 acceleration;

    void InitParticle(Particle particle)
    {

    }
    void Update(HashSet<Particle> aliveParticles)
    {

    }
}
