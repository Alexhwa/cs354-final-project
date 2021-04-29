using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    [Header("Renderer Data")]
    [SerializeField] private Material startMaterial;
    [SerializeField] private float startSize;

    // change back to IModule
    List<int> modules;
    // RendererComponent renderer;


    private void Awake() {
        aliveParticles = new HashSet<Particle>();
        deadParticles = new Queue<Particle>();
        modules = new List<int>();
    }

    private void Update() {
        // emit clause
        // if emit (according to emission rate & max particles)
        // 1. get particle
        // 2. initialize particle defaults (position, color, normal, size)
        // 3. init particle across all modules (particles should have all needed keys in dictionary)

        // kill clause

        // update all components
        // 

        // render
    }
}
