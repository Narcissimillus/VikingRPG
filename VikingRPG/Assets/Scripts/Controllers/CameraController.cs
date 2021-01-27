using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    public float height = 2f;
    public float zoom = 10f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void LateUpdate() //se apeleaza imediat dupa update
    {
        transform.position = target.position - offset * zoom; //actualizeaza pozitia
        transform.LookAt(target.position + Vector3.up * height); //seteaza directia camerei inspre player
    }
}
