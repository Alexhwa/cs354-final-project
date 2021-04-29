using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle
{
    private Dictionary<string, object> data;

    public Particle() {
        data = new Dictionary<string, object>();
    }

    // returns data[name] cast into type T
    public T Get<T>(string name) {
        if (!data.ContainsKey(name)) {
            Debug.LogError("Particle system does not define a particle property named " + name);
            return default(T);
        }
        
        var value = data[name];
        if (!(value is T)) {
            Debug.LogError("Particle system property named " + name + " is not of type " + typeof(T));
            return default(T);
        }

        return (T)(value);
    }

    // stores value inside data under the name
    public void Set<T>(string name, T value) {
        if (!data.ContainsKey(name)) {
            data.Add(name, value);
        } else {
            data[name] = value;
        }
    }

    // clears the data so that this particle can be reused
    public void Reset() {
        data.Clear();
    }
}
