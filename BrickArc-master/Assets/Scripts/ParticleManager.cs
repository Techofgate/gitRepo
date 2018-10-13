using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour {

    public GameObject[] Particles;

	public void Emit(int index, Transform transform) {
        Instantiate(Particles[index], transform.position, transform.rotation);
    }

    public void Emit(int index, Vector3 position, Quaternion rotation) {
        Instantiate(Particles[index], position, rotation);
    }
}
