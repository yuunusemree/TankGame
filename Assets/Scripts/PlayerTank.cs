using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTank : Tank
{
    Vector3 touchPoint = Vector3.zero;
    Camera main;
    Vector3 moveDir;
    private void Start()
    {
        main = Camera.main;
    }

    protected override void Move()
    {
        float moveAxis = Input.GetAxis("Vertical");
        float rotAxis = Input.GetAxis("Horizontal");

        rb.MovePosition(transform.position + transform.forward * Time.deltaTime * moveAxis * moveSpeed);
        rb.MoveRotation(transform.rotation * Quaternion.Euler(transform.up * Time.deltaTime * rotAxis * rotSpeed));

        CreateMoveEffect(moveAxis);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(LookAt(other));
        }

        if (Input.GetMouseButton(0))
            SetTouchPoint();

        GoToTouchPoint();
    }

    protected override IEnumerator LookAt(Transform other)
    {
        while (Vector3.Angle(turret.forward, (other.position - transform.position)) > 6f)
        {
            turret.Rotate(turret.up, 5f);
            yield return null;
        }
        Fire();
    }


    private void SetTouchPoint()
    {
        Ray ray = main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.origin, ray.direction*100, Color.yellow);

        if (Physics.Raycast(ray, out RaycastHit info))
        {
            touchPoint = new Vector3(info.point.x, transform.position.y, info.point.z);
            moveDir = touchPoint - transform.position;
        }
    }


    private void GoToTouchPoint()
    {
        transform.position = Vector3.Lerp(transform.position, touchPoint, Time.deltaTime * moveSpeed);
        Quaternion lookRotation = Quaternion.LookRotation(moveDir);
        transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * moveSpeed);
    }


}
