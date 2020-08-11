using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    private Camera camera;
    // Start is called before the first frame update
    private void Start()
    {
        camera = Camera.main;
    }
    // Update is called once per frame
    void LateUpdate()
    {
        transform.LookAt(transform.position + camera.transform.forward);
    }
}
