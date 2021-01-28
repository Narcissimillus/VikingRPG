using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ParticleSysManager : MonoBehaviour
{
    public ParticleSystem[] hitParticles;

    public static ParticleSysManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Play(int index)
    {
        if (hitParticles[index] == null)
        {
            Debug.LogWarning("Particle " + name + " is missing!");
            return;
        }
        hitParticles[index].Play();
    }
}
