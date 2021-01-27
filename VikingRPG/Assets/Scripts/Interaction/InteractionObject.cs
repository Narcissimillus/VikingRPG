using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionObject : MonoBehaviour
{

    public float radius = 1f;
    public Transform player;
    [HideInInspector] public bool done = false;

    //metoda abstracta, speficica fiecarui tip de interactiuni
    public virtual void Interaction()
    {

    }

    void Update()
    {
        
        Vector3 a = player.position;
        Vector3 b = transform.position;
        if (Mathf.Abs(a.y - b.y) <= 2)
        {
            a.y = 0;
            b.y = 0;
        }
        float distance = Vector3.Distance(a, b);
        if (distance <= radius && !done) // avem interactiune cu obiectul
        {
            done = true;
            Interaction();
        }
        else if(distance > radius)
        {
            done = false;
        }
    }

    void OnDrawGizmosSelected()
    {
        if(player == null)
        {
            player = transform;
        }
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}