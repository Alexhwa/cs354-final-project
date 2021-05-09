using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RenderModule : IModule
{
    [SerializeField] private bool billboarding;

    public void InitParticle(Particle particle, CParticleSystem system)
    {
        if (billboarding)
        {
            particle.Set<Vector3>("Rotation", Vector3.zero);
        }
    }
    public void Update(HashSet<Particle> aliveParticles)
    {
        if (billboarding)
        {
            foreach (Particle p in aliveParticles)
            {
                /*
                Vector3 camForward = Camera.main.transform.forward;
                Vector3 camPos = Camera.main.transform.position;
                Plane plane = new Plane(p.Get<Vector3>("Position"), -camForward);
                Ray r = new Ray(camPos, camForward);
                float enter = 0;
                plane.Raycast(r, out enter);
                r.GetPoint(enter);
                */

                var lookDir = Quaternion.LookRotation(-Camera.main.transform.forward);
                p.Set<Vector3>("Rotation", lookDir.eulerAngles);
            }
        }
    }
}
