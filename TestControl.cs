using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using System.Net.Sockets;
using System;
using System.Text;



public class TestControl : MonoBehaviour
{
    //send xyz controls to xplane to control plane
    [Range(-1, 1)] public float xAxisRotation;
    [Range(-1, 1)] public float yAxisRotation;
    [Range(-1, 1)] public float zAxisRotation;

    Thread readThread3;
    public sendData udp;
    public colliderCarrierXplane CCX;

     float ws = 1f;
     float wd = 90f;
    float total = 0f;
    float x1;
    float x2;
    byte[] bytethrottle = null;
    byte[] started = { 68, 65, 84, 65, 42 };
    byte[] controls = { 8, 0, 0, 0 };
    byte[] n1 = { 41, 0, 0, 0 };
    byte[] n2 = { 42, 0, 0, 0 };
    byte[] end = { 0, 192, 121, 196, 0, 192, 121, 196, 0, 192, 121, 196, 0, 192, 121, 196, 0, 192, 121, 196 };
    byte[] throttle = { 25, 0, 0, 0 };
    byte[] endthrottle = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
    byte[] throttleAct = { 26, 0, 0, 0 };
    byte[] gears = { 14, 0, 0, 0 };
    byte[] endgears = { 0, 0, 0, 0, 0, 0, 0, 0 };
    byte[] endgears2 = { 0, 192, 121, 196, 0, 192, 121, 196, 0, 192, 121, 196 };
    byte[] weath = { 126, 0, 0, 0 };
    byte[] endweath = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 192, 121, 196, 0, 0, 0, 0, 0, 0, 0, 0 };

    byte[] reseta = { 47, 0, 0, 0 };
    byte[] resetb = {  186, 254, 105, 66, 186, 254, 105, 66, 186, 254, 105, 66, 186, 254, 105, 66, 186, 254, 105, 66, 186, 254, 105, 66 };

    void start()
    {
        readThread3 = new Thread(new ThreadStart(Joystick));
        readThread3.IsBackground = true;
        readThread3.Start();
    }
    void Update()
    {
        Joystick();
        resetPlane();
    }
    //to reset collider killswitch
    void resetPlane()
    {
        if (Input.GetAxis("reset1") == 1)
        {
            CCX.collidePlane = false;
        }

    }

    void Joystick()
    {
        byte[] combined = null;
        byte[] byteElev = BitConverter.GetBytes(Input.GetAxis("Vertical"));
        byte[] byteAilrn = BitConverter.GetBytes(Input.GetAxis("Horizontal"));
        byte[] byteRudder = BitConverter.GetBytes((Input.GetAxis("Horizontal")/4));
        x1 = (Input.GetAxis("takeoff") * 110 + 20);
        x2 = (Input.GetAxis("takeoff") * 75 + 30);
        
        byte[] bytethrottle = BitConverter.GetBytes((Input.GetAxis("takeoff")));
        
        byte[] byteRpm = BitConverter.GetBytes((x1));

        byte[] byteff = BitConverter.GetBytes((x2));

        byte[] bytegear1 = BitConverter.GetBytes((Input.GetAxis("gear")));
        byte[] bytegear2 = BitConverter.GetBytes((Input.GetAxis("brakes")));

        total = ws + ((wd+2)/1000);
        byte[] windspeed = BitConverter.GetBytes(( total ));

        byte[] resetG = BitConverter.GetBytes((Input.GetAxis("reset1")*5000));

        //concatenate 
        ArrayList alist = new ArrayList();
        alist.AddRange(started);
        alist.AddRange(controls);
        alist.AddRange(byteElev);
        alist.AddRange(byteAilrn);
        alist.AddRange(byteRudder);
        alist.AddRange(end);
        alist.AddRange(throttle);
        alist.AddRange(bytethrottle);
        alist.AddRange(bytethrottle);
        alist.AddRange(endthrottle);
        //--
        //--N1
        alist.AddRange(n1);
        alist.AddRange(byteRpm);
        alist.AddRange(byteRpm);
        alist.AddRange(endthrottle);
        //--
        //--N2
        alist.AddRange(n2);
        alist.AddRange(byteff);
        alist.AddRange(byteff);
        alist.AddRange(endthrottle);
        //--l
        //----
        alist.AddRange(weath);
        alist.AddRange(windspeed);
        alist.AddRange(endweath);
        //--
        alist.AddRange(gears);
        alist.AddRange(bytegear1);
        alist.AddRange(bytegear2);
        alist.AddRange(endgears);
        //second brakes
        alist.AddRange(bytegear2);
        alist.AddRange(endgears2);

        //reset
        alist.AddRange(reseta);
        alist.AddRange(resetG);
        alist.AddRange(resetG);
        alist.AddRange(resetb);


        combined = (byte[])alist.ToArray(typeof(byte));
        udp.server.Send(combined, combined.Length);
    }

}