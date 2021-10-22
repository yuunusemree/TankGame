using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Perspective : Sense
{
    float fieldOfView;
    float maxCheckDistance;
    public Transform other;
    Animator fsm;
    public override void InitializeSense()
    {
        fieldOfView = 60;
        maxCheckDistance = 20;
        fsm = GetComponent<Animator>();
    }

    public override void UpdateSense()
    {
        Vector3 dir = (other.transform.position - transform.position).normalized;
        Debug.DrawRay(transform.position, dir, Color.yellow);

        float angle = Vector3.Angle(dir, transform.forward);

        Debug.DrawRay(transform.position, transform.forward * maxCheckDistance, Color.blue);

        Debug.DrawRay(transform.position, dir * maxCheckDistance, Color.red);
        if (angle < fieldOfView/2f)
        {
            Ray ray = new Ray(transform.position, dir);
            
            if (Physics.Raycast(ray, out RaycastHit hitInfo, maxCheckDistance))
            {
                Debug.DrawRay(transform.position, dir * maxCheckDistance, Color.green);
                string name = hitInfo.transform.name;

                if (name.Equals("playerTank"))
                {
                    fsm.SetBool("visible", true);
                }
                else
                {
                    fsm.SetBool("visible", false);
                }
            }
            else
            {
                fsm.SetBool("visible", false);
            }
        }

    }
}
