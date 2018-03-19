using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class colliderCarrierXplane : MonoBehaviour {

    public Rigidbody rb;
    Collision collisionInfo;
    public bool collidePlane ;
    public double Timer = 0.0;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        collidePlane = false;
    }

    void OnCollisionEnter(Collision collisionInfo)
    {
        //need to avoid 000 initial positioning
        if (Timer > 0.07) {
            print("Detected collision between " + gameObject.name + " and " + collisionInfo.collider.name);
            print("TTT Time " + collidePlane);
            if (gameObject.name == "Jet")
            {
                collidePlane = true;
                //print("@@@@ Their relative velocity is " + collisionInfo.relativeVelocity);
            }

        }
    }



    void Update()
    {
        
        Timer += Time.deltaTime; //Time.deltaTime will increase  
    }

}
