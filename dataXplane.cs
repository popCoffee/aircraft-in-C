using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dataXplane : MonoBehaviour {

    public colliderCarrierXplane coll;
    public decoding decode;
    private float rollvalue ;
    float initialx;
    float initialy;
    float initialz;
    float heightshipDeck = (22+12);
    public bool neverDone = false;
    public Rigidbody rb;

    float newv =30;

    void Start () {

        rb = GetComponent<Rigidbody>();
        neverDone = true;
    }

    void Update () {
        print("this >>> prob " + coll.collidePlane);
        initializedxyz();

        //collider initiated when touching carrier
        if (coll.collidePlane == false) {
            positionxyz();
            Rotation();
        }
        else if(coll.collidePlane == true)
        {
            slowdown();
            float y = 0;
            float z = -1;
            float x = 0;
            //z += Time.deltaTime * 10 ;
            transform.rotation = Quaternion.Euler(x, y, z);
        }
	}

    private void Rotation()
    {
        //rotate body
        float y = decode.head;
        float z = -decode.roll1;
        float x = -decode.pitch1;
        //z += Time.deltaTime * 10 ;
        transform.rotation = Quaternion.Euler(x, y, z);

    }

    void initializedxyz()
    {
        
            if (neverDone == true  && decode.posX != 0)
            {
                //print("pos @@@ >" + decode.posX + decode.posY + decode.posZ);
                initialx = decode.posX;
                initialy = decode.posY;
                initialz = decode.posZ;
                neverDone = false;

            }

    }

    void positionxyz()
    {
        Vector3 newPosition = transform.position;
        newPosition.x = (decode.posX - initialx);
        //print("x newPosotion  >" + newPosition.x);
        // print("y pos pl  >" + decode.posY);
        //newPosition.y = (decode.posY - initialy)+ heightshipDeck;
        newPosition.y = (decode.PlaneHeight + heightshipDeck);
        newPosition.z = -(decode.posZ - initialz);
        transform.position = newPosition;

    }

    void slowdown()
    {
        
        newv = newv * 0.99f;
        rb.velocity = new Vector3(0, 0, newv);

    }

}
