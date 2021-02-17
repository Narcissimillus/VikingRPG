using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropBookOnDestroy : MonoBehaviour
{
    private bool isQuitting = false;
    public Item bookOfSages;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnApplicationQuit()
    {
        isQuitting = true;
    }

    private void OnDestroy()
    {
        if (!isQuitting && !PauseMenu.isExitting)
        {
            Instantiate(bookOfSages.itemObject, this.transform.position, Quaternion.Euler(0, 90, 0));
        }
    }
}
