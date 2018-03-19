using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Sockets;
using System;
using System.Text;
using System.Net;
using System.Threading;
using System.Linq;

public class sendData : MonoBehaviour {

    public byte[] dataOut = new byte[1024];

    Thread readThread2;
    Thread readThread3;

    public UdpClient server = new UdpClient("127.0.0.1", 49000);

    // Use this for initialization
    void Start () {

        // create thread for reading UDP messages
        readThread2 = new Thread(new ThreadStart(SendData));
        readThread2.IsBackground = true;
        readThread2.Start();
    }
	
	// Update is called once per frame
	void Update () {
    }

    public void SendData()
    {
    }

}
