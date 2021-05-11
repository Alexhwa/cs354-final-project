using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementModule : IModule
{
    [SerializeField] private float speed;
    [Range(0, 180)] [SerializeField] private float coneSpread;
    [SerializeField] private Vector3 coneRotation;
    private Vector3 coneDirection => Quaternion.Euler(coneRotation) * Vector3.forward;
    private Vector3 coneNormal => Quaternion.Euler(coneRotation) * Vector3.up;
    private Vector3 coneTangent => Quaternion.Euler(coneRotation) * Vector3.right;

    [SerializeField] private Vector3 acceleration;

    public override void InitParticle(Particle particle)
    {
        // random point along unit circle
        var randomRotAxis = Quaternion.AngleAxis(Random.Range(0f, 360f), coneDirection) * coneNormal;
        var randomDirection = Quaternion.AngleAxis(Random.Range(-coneSpread, coneSpread), randomRotAxis) * coneDirection;
        
        particle.Set<Vector3>("Velocity", randomDirection * speed);
        particle.Set<Vector3>("Acceleration", acceleration);
    }

    public override void UpdateParticles(HashSet<Particle> aliveParticles)
    {
        foreach (Particle p in aliveParticles) {
            p.Set<Vector3>("Position", p.Get<Vector3>("Position") + p.Get<Vector3>("Velocity") * Time.deltaTime);
            p.Set<Vector3>("Velocity", p.Get<Vector3>("Velocity") + p.Get<Vector3>("Acceleration") * Time.deltaTime);
        }
    }


    public void OnDrawGizmosSelected() {
        var coneDirectionWorld = transform.TransformDirection(coneDirection);
        var coneNormalWorld = transform.TransformDirection(coneNormal);
        var coneTangentWorld = transform.TransformDirection(coneTangent);

        Gizmos.color = new Color(0.3f, 1f, 1f, 1f);
        Gizmos.DrawRay(transform.position, coneDirectionWorld * speed);

        Gizmos.color = new Color(0.1f, 0.3f, 0.6f, 1f);
        Gizmos.DrawRay(transform.position, Quaternion.AngleAxis( coneSpread,  coneNormalWorld) * coneDirectionWorld * speed);
        Gizmos.DrawRay(transform.position, Quaternion.AngleAxis(-coneSpread,  coneNormalWorld) * coneDirectionWorld * speed);
        Gizmos.DrawRay(transform.position, Quaternion.AngleAxis( coneSpread, coneTangentWorld) * coneDirectionWorld * speed);
        Gizmos.DrawRay(transform.position, Quaternion.AngleAxis(-coneSpread, coneTangentWorld) * coneDirectionWorld * speed);
    }
}
