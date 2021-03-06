using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
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

    Mesh mesh;
    MeshFilter meshFilter;

    // particle data
    HashSet<Particle> aliveParticles;
    Queue<Particle> deadParticles;

    [SerializeField] private uint maxParticles;
    [SerializeField] private float particlesPerSecond;
    [SerializeField] private float startLifetime;
    [SerializeField] private bool isLooping;
    private float emissionTimer;

    [SerializeField] private Material startMaterial;
    [SerializeField] private float startSize;
    [SerializeField] private Color startColor;
    [SerializeField] private Vector3 startRotation;

    private IModule[] modules;

    private void OnEnable() {
        aliveParticles = new HashSet<Particle>();
        deadParticles = new Queue<Particle>();
        mesh = new Mesh();
        meshFilter = GetComponent<MeshFilter>();

        modules = GetComponents<IModule>();
    }

    private void Update() {
        // emit clause
        // if emit (according to emission rate & max particles)
        // 1. get particle
        // 2. initialize particle defaults (position, color, normal, size)
        // 3. init particle across all modules (particles should have all needed keys in dictionary)

        emissionTimer -= Time.deltaTime;
        if(particlesPerSecond < 0)
        {
            particlesPerSecond = 0;
        }
        while(emissionTimer <= 0 && particlesPerSecond > 0)
        {
            emissionTimer += 1f / particlesPerSecond;
            if (aliveParticles.Count < maxParticles) {
                EmitParticle();
            }
        }

        // kill clause
        LifetimeStep();
        // update all components
        foreach (IModule m in modules)
        {
            m.UpdateParticles(aliveParticles);
        }
        
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
        foreach (IModule m in modules)
        {
            m.InitParticle(p);
        }
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
        p.Set<float>("Total_Lifetime", startLifetime);
        p.Set<Vector3>("Position", Vector3.zero);
        p.Set<float>("Size", startSize);
        p.Set<Color>("Color", startColor);
        p.Set<Vector3>("Rotation", startRotation);
        //p.Set<float>("UV", startSize);
    }
    private void PopulateMesh()
    {
        List<Vector3> verts = new List<Vector3>();
        List<int> tris = new List<int>();
        List<Vector2> uvs = new List<Vector2>();
        List<Color> colors = new List<Color>();

        mesh.Clear();

        foreach (Particle p in aliveParticles) {
            var rot = Quaternion.Euler(p.Get<Vector3>("Rotation"));
            var up = rot * Vector3.up * p.Get<float>("Size");
            var right = rot * Vector3.right * p.Get<float>("Size");
            var center = p.Get<Vector3>("Position");

            var baseVertexId = verts.Count;
            verts.Add(center - up - right); // bottom left
            verts.Add(center - up + right); // bottom right
            verts.Add(center + up - right); // top left
            verts.Add(center + up + right); // top right

            tris.Add(baseVertexId + 0); // bottom left triangle
            tris.Add(baseVertexId + 1);
            tris.Add(baseVertexId + 2);
            tris.Add(baseVertexId + 3); // top right triangle
            tris.Add(baseVertexId + 2);
            tris.Add(baseVertexId + 1);
            tris.Add(baseVertexId + 0); // bottom left triangle back
            tris.Add(baseVertexId + 2);
            tris.Add(baseVertexId + 1);
            tris.Add(baseVertexId + 3); // top right triangle back
            tris.Add(baseVertexId + 1);
            tris.Add(baseVertexId + 2);

            uvs.Add(Vector2.zero);
            uvs.Add(Vector2.right);
            uvs.Add(Vector2.up);
            uvs.Add(Vector2.one);

            var color = p.Get<Color>("Color");
            colors.Add(color);
            colors.Add(color);
            colors.Add(color);
            colors.Add(color);
        }
        
        mesh.SetVertices(verts);
        mesh.SetTriangles(tris, 0);
        mesh.SetUVs(0, uvs);
        mesh.SetColors(colors);
    }
}

