using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tank : MonoBehaviour
{
    public Rigidbody rb { get { return GetComponent<Rigidbody>(); } }
    public Material mat;
    public float moveSpeed = 8f;
    public float rotSpeed = 240f;
    public Transform other;
    public Transform turret;
    public Rigidbody bombPrefab;
    public Transform bombSpawn;
    [Range(10000f,50000f)]
    public float shootSpeed = 15000f;

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
    }

    protected abstract void Move();

    protected abstract IEnumerator LookAt(Transform other);


    protected void Fire()
    {
        var bomb = Instantiate(bombPrefab, bombSpawn.position, Quaternion.identity);
        bomb.AddForce(turret.forward * shootSpeed);
    }

    protected void CreateMoveEffect(float moveAxis)
    {
        mat.mainTextureOffset += new Vector2(moveAxis, 0);
    }
}
