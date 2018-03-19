using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Sockets;
using System;
using System.Text;
using System.Net;
using System.Threading;
using System.Diagnostics;
using System.IO;
 using System.Runtime.Serialization;
 using System.Runtime.Serialization.Formatters.Binary;

public class port_connect : MonoBehaviour
{

    public byte[] data = new byte[1024];

    // read Thread
    Thread readThread;
    Thread readThreadSend;

    // udpclient object
    UdpClient client;

    // port number
    public int port1 = 49003;
    public bool bol = false;

    // start from unity3d
    void Start()
    {

        StartCoroutine(_wait() );
        
       
    }

    IEnumerator _wait()
    {
        //System.Diagnostics.Process.Start("C:/Users/BEMR_WS26/Desktop/X-Plane 11.url");
        Time.timeScale = 1f;
        yield return new WaitForSeconds(0.0f);

        // create thread for reading UDP messages
        readThread = new Thread(new ThreadStart(ReceiveData));
        readThread.IsBackground = true;
        readThread.Start();
        print("X-Plane Data Read: \n\n");
        //public boolean for pausing other scripts,functions
        bol = true;

    }

    // Unity Update Function
    void Update()
    {

    }

    // Unity Application Quit Function
    void OnApplicationQuit()
    {
        stopThread();
    }

    // Stop reading UDP messages
    private void stopThread()
    {
        if (readThread.IsAlive)
        {
            readThread.Abort();
        }
        client.Close();
    }

    // receive thread function
    public void ReceiveData()
    {

        UdpClient client = new UdpClient(49001);
        while (true)
        {
            try
            {
                // receive bytes
                IPEndPoint anyIP = new IPEndPoint(IPAddress.Any, 0);

                data = client.Receive(ref anyIP);
                //display data as numbers in form {  }
                var sb = new StringBuilder();
                    foreach (var b in data)
                    {
                        sb.Append(b + ", ");
                    }   
                    
                    sb.Append(" Done");
                    //print("..>> " + sb);
                
                
            }
            catch (Exception err)
            {
                print(err.ToString());
            }
        }

    }

}