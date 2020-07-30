using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Mover : MonoBehaviour
{
    void Update()
    {
        transform.LookAt(LookAtCursor());
        if (Input.GetMouseButton(0))
        {
            LookAtCursor();
        }
        UpdateAnimator();
    }
    private Vector3 LookAtCursor()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        bool hasHit = Physics.Raycast(ray, out hit);
        if (hasHit)
        {
            Vector3 tarPos = new Vector3(hit.point.x, 0, hit.point.z);
            return tarPos;
        }
        //if(hasHit)
        //{
        //  GetComponent<NavMeshAgent>().destination = hit.point;
        //}
        return Vector3.zero;
    }
    private void UpdateAnimator()
    {
        Vector3 velocity = GetComponent<NavMeshAgent>().velocity;
        Vector3 localVelocity = transform.InverseTransformDirection(velocity);
        float speed = localVelocity.z;
        GetComponent<Animator>().SetFloat("forwardSpeed", speed);
    }
}
