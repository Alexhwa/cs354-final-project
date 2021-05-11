using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationModule : IModule
{
    public enum RotationType {
        Local,
        Billboarding
    }

    [SerializeField] private RotationType rotationType;
    [SerializeField] private Vector3 startRotation;
    [SerializeField] private Vector3 rotationVelocity;
    [SerializeField] private Vector3 rotationAcceleration;

    public override void InitParticle(Particle particle)
    {
        particle.Set<Vector3>("Rotation Velocity", rotationVelocity);
        particle.Set<Vector3>("Rotation Acceleration", rotationAcceleration);
        particle.Set<Vector3>("Rotation Offset", startRotation);
    }

    public override void UpdateParticles(HashSet<Particle> aliveParticles)
    {
        Camera camera = null;
        if (rotationType == RotationType.Billboarding) {
            camera = Camera.main;
        }

        foreach (Particle p in aliveParticles) {
            if (rotationType == RotationType.Billboarding) {
                var lookDir = Quaternion.LookRotation(transform.InverseTransformDirection(-camera.transform.forward));
                p.Set<Vector3>("Rotation", p.Get<Vector3>("Rotation Offset") + lookDir.eulerAngles);
                
            }
            else {
                p.Set<Vector3>("Rotation", p.Get<Vector3>("Rotation Offset"));
            }
            
            p.Set<Vector3>("Rotation Offset", p.Get<Vector3>("Rotation Offset") + p.Get<Vector3>("Rotation Velocity") * Time.deltaTime);
            p.Set<Vector3>("Rotation Velocity", p.Get<Vector3>("Rotation Velocity") + p.Get<Vector3>("Rotation Acceleration") * Time.deltaTime);
        }
    }
}
