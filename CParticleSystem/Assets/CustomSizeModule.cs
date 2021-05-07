using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum RandomType
{
    None,
    Uniform,
    Normal
};

[System.Serializable]
public class CustomSizeModule : IModule
{
    [SerializeField] private bool oscillate;
    [SerializeField] private float frequency;
    [SerializeField] private RandomType randomization;
    [SerializeField] private Vector2 uniformRange;
    [SerializeField] private float normalMean;
    [SerializeField] private float normalSpread;

    public override void InitParticle(Particle particle, CParticleSystem system)
    {
        particle.Set<float>("Age", 0f);
        particle.Set<float>("Start Size", Randomize(particle.Get<float>("Size")));
    }

    public override void Update(HashSet<Particle> aliveParticles)
    {
        foreach (Particle p in aliveParticles)
        {
            p.Set<float>("Age", p.Get<float>("Age") + Time.deltaTime);
            p.Set<float>("Size", OscillateSize(p));
        }
    }

    private float OscillateSize(Particle p)
    {
        float size = p.Get<float>("Start Size");
        float age = p.Get<float>("Age");

        return oscillate ? size * (Mathf.Sin(2 * Mathf.PI * age * frequency) + 1) / 2 : size;
    }

    private float Randomize(float f)
    {
        switch(randomization)
        {
            case RandomType.Uniform:
                return Random.Range(uniformRange[0], uniformRange[1]);
            case RandomType.Normal:
                // Box-Muller method
                return Mathf.Sqrt(-2 * Mathf.Log(Random.Range(0f, 1f))) * Mathf.Cos(2*Mathf.PI * Random.Range(0f, 1f)) * normalSpread + normalMean;
            default:
                return f;
        }
    }
}
