using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[RequireComponent(typeof (MeshFilter))]
[RequireComponent(typeof (MeshRenderer))]
public class CParticleSystem : MonoBehaviour
{
    /*
    Mesh
    Num alive particles
    Free particles (queue)
    Particle[]
    Emitter component // 1
    If particle[] has space, make particle
    If any in particle[] has time elapsed >= lifetime, empty the spot
    Do actions based on parameters
    List of components // 2 through N-1
    Renderer component // N
    Creates a mesh out of all particle data
    Do actions based on parameters
    Bit ID (100000) - used to detect what components are enabled
    Map (string -> value) for default values
    */

    Mesh mesh; // TODO: check if this is actually what you do
    MeshFilter meshFilter;

    // particle data
    HashSet<Particle> aliveParticles;
    Queue<Particle> deadParticles;
    int numAliveParticles => aliveParticles.Count;

    // component data
    [Header("Emission Data")]
    [SerializeField] private int maxParticles;
    [SerializeField] private float particlesPerSecond;
    [SerializeField] private float startLifetime;
    [SerializeField] private bool isLooping;
    private float emissionTimer;

    [Header("Renderer Data")]
    [SerializeField] private Material startMaterial;
    [SerializeField] private float startSize;
    [SerializeField] private Color startColor;
    [SerializeField] private Vector3 startFacing;

    // change back to IModule
    List<int> modules;
    // RendererComponent renderer;


    private void Awake() {
        aliveParticles = new HashSet<Particle>();
        deadParticles = new Queue<Particle>();
        modules = new List<int>();
        mesh = new Mesh();
        meshFilter = GetComponent<MeshFilter>();
    }

    private void Update() {
        // emit clause
        // if emit (according to emission rate & max particles)
        // 1. get particle
        // 2. initialize particle defaults (position, color, normal, size)
        // 3. init particle across all modules (particles should have all needed keys in dictionary)
        while(emissionTimer <= 0)
        {
            emissionTimer += 1f / particlesPerSecond;
            EmitParticle();
        }
        // kill clause
        LifetimeStep();
        // update all components
        // 
        
        // render
        PopulateMesh();
        meshFilter.mesh = mesh;
    }

    private void EmitParticle()
    {
        Particle p;
        if(deadParticles.Count > 0)
        {
            p = deadParticles.Dequeue();
        }
        else if(aliveParticles.Count < maxParticles)
        {
            p = new Particle();
        }
        else
        {
            return;
        }
        InitDefaults(p);
        aliveParticles.Add(p);
    }
    private void LifetimeStep()
    {
        foreach(Particle p in aliveParticles.ToArray())
        {
            p.Set<float>("Lifetime", p.Get<float>("Lifetime") - Time.deltaTime);
            if (p.Get<float>("Lifetime") <= 0)
            {
                KillParticle(p);
            }
        }
    }
    private void KillParticle(Particle p)
    {
        aliveParticles.Remove(p);
        deadParticles.Enqueue(p);
        p.Reset();
    }
    private void InitDefaults(Particle p)
    {
        p.Set<float>("Lifetime", startLifetime);
        p.Set<Vector3>("Position", Vector3.zero);
        p.Set<float>("Size", startSize);
        p.Set<Color>("Color", startColor);
        p.Set<Vector3>("Normal", startFacing);
        //p.Set<float>("UV", startSize);

    }
    private void PopulateMesh()
    {
        List<Vector3> verts = new List<Vector3>();
        List<int> tris = new List<int>();
        mesh.Clear();
        for(int i = 0; i < 2; i++)
        {
            for (int j = 0; j < 2; j++)
            {
                for (int k = 0; k < 2; k++)
                {
                    verts.Add(new Vector3(i, j, k));
                }
            }
        }
        tris.Add(0);
        tris.Add(1);
        tris.Add(2);

        tris.Add(3);
        tris.Add(1);
        tris.Add(2);
        mesh.SetVertices(verts);
        mesh.SetTriangles(tris, 0);
    }
}
