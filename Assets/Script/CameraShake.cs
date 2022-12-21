using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public Animator cameraAm;
    void Start()
    {
        
    }
    
    void Update()
    {
        
    }

    public void Shake()
    {
        cameraAm.SetTrigger("Shake");
    }
}
