using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Footsteps : MonoBehaviour
{
    AudioSource audioSource;
    public AudioClip[] clips;
    NavMeshAgent agent;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void PlayFootstep()
    {
        int clipNo = Random.Range(0, 9);
        audioSource.clip = clips[clipNo];
        if(agent.velocity.magnitude > 0f)
        {
            audioSource.volume = Random.Range(0.8f, 1f);
            audioSource.pitch = Random.Range(0.8f, 1.2f);
            audioSource.Play();
        }
    }
}
