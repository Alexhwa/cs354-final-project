using System;
using System.Collections.Generic;
using UnityEngine;

public interface IShape {}

[Serializable]
public class Cube : IShape
{
    public Vector3 size;
}

[Serializable]
public class Thing
{
    public int weight;
}

[ExecuteInEditMode]
public class BuildingBlocks : MonoBehaviour
{
    [SerializeReference]
    public List<IShape> inventory;

    [SerializeReference]
    public System.Object bin;

    [SerializeReference]
    public List<System.Object> bins;

    void OnEnable()
    {
        if (inventory == null)
        {
            inventory = new List<IShape>()
            {
                new Cube() {size = new Vector3(1.0f, 1.0f, 1.0f)}
            };
            Debug.Log("Created list");
        }
        else
            Debug.Log("Read list");

        if (bins == null)
        {
            // This is supported, the 'bins' serialized field is declared as holding a collection type.
            bins = new List<System.Object>() { new Cube(), new Thing() };
        }

        if (bin == null)
        {
            // !! DO NOT USE !!
            // Although, this is syntaxically correct, it is NOT supported as a valid serialization construct because the 'bin' serialized field is declared as holding a single reference type.
            bin = new List<System.Object>() { new Cube() };
        }
    }
}