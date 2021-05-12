using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorOverLifetimeModule : IModule
{

    [SerializeField] private Gradient colors;
    private Color o_color;

    public override void InitParticle(Particle particle)
    {
        o_color = particle.Get<Color>("Color");
    }
    public override void UpdateParticles(HashSet<Particle> aliveParticles)
    {
        foreach (Particle p in aliveParticles)
        {
            var x = p.Get<float>("Lifetime") / p.Get<float>("Total_Lifetime");
            p.Set<Color>("Color", colors.Evaluate(1 - x) * o_color);
        }
    }

}
