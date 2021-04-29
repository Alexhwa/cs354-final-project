using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MovementModule : IModule
{
    [SerializeField] private Vector3 velocity;
    [SerializeField] private Vector3 acceleration;

    public void InitParticle(Particle particle)
    {

    }
    public void Update(HashSet<Particle> aliveParticles)
    {

    }
}
