using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlsShip : MonoBehaviour {

    public decoding decode;
    float initialz;
    float initialx;
    float heightDeck = 22;
    bool neverDone = false;

    void Start () {
        neverDone = true;
    }
    void Update () {
        initializedxyz();
        positionxyz();
    }


    void initializedxyz()
    {

        if (neverDone == true && decode.posX != 0)
        {
            initialx = decode.posX;
            initialz = decode.posZ;
            neverDone = false;
        }

    }

    void positionxyz()
    {
        Vector3 newPosition = transform.position;
        newPosition.x = (decode.posX - initialx);

        newPosition.y = heightDeck;
        newPosition.z = Mathf.Abs(decode.ship1 - initialz);
        transform.position = newPosition;
        //print("z pos  >" + newPosition.z);
    }





}
