using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AITank : Tank
{
    public Animator fsm { get { return GetComponent<Animator>(); } }
    public NavMeshAgent agent { get { return GetComponent<NavMeshAgent>(); } }


    public Transform[] wayPoints;
    Vector3[] wayPointsPos;
    int index;
    private void Start()
    {
        wayPointsPos = new Vector3[wayPoints.Length];
        for (int i = 0; i < wayPoints.Length; i++)
        {
            wayPointsPos[i] = wayPoints[i].position;
        }
    }

    protected override void Move()
    {
        float distance = Vector3.Distance(transform.position, other.position);
        fsm.SetFloat("distance", distance);

        float distanceFromCurrentWaypoint = Vector3.Distance(transform.position, wayPointsPos[index]);
        fsm.SetFloat("distanceFromCurrentWaypoint", distanceFromCurrentWaypoint);
    }

    internal void Patrol()
    {
        agent.SetDestination(wayPointsPos[index]);
        //LookAt(other);
    }

    internal void Chase()
    {
        agent.SetDestination(other.position);
    }

    float delayed;
    internal void Shoot()
    {
        if ((delayed += Time.deltaTime) > 1f)
        {
            Fire();
            delayed = 0;
        }
    }

    public void LookAt()
    {
        LookAt(other);
    }

    protected override IEnumerator LookAt(Transform other)
    {
        while (Vector3.Angle(turret.forward, (other.position - transform.position)) > 6f)
        {
            turret.Rotate(turret.up, 5f);
            yield return null;
        }
    }

    public void FindNewWaypoint()
    {
        switch (index)
        {
            case 0:
                index = 1;
                break;
            case 1:
                index = 0;
                break;
        }
        agent.SetDestination(wayPointsPos[index]);
    }
}
