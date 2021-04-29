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
        // TODO
        return default(T);
    }

    // stores value inside data under the name
    public void Set<T>(string name, T value) {
        // TODO
    }

    // clears the data so that this particle can be reused
    public void Reset() {
        // TODO
    }
}
