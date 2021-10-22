using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Sense : MonoBehaviour, ISense
{
    public abstract void InitializeSense();


    public abstract void UpdateSense();
    

    // Start is called before the first frame update
    void Start()
    {
        InitializeSense();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateSense();
    }
}
