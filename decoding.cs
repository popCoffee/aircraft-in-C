using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text;
using System.Text.RegularExpressions;
 
public class decoding : MonoBehaviour
{
    public colliderCarrierXplane coll2;
    public port_connect port;
    string[] storeArrayLimited = new string[36];
    public float posX ;
    public float posY ;
    public float posZ ;
    public float velx;
    public float vely;
    public float velz;
    public float pitch1;
    public float roll1;
    public float head;
    public float ship1;
    public float PlaneHeight;
    public float tailnumber;
    
    //500 for space. if more variables then expand
    //string[] storeArray2 = new string[500];
    string[] storeArray = new string[500];
    public byte[] newData = new byte[500];
    public byte[] newData2 = new byte[36];

    void Start()
    {
        port.bol = false;
    }

    void preanalysis()
    {
        int rr = port.data.Length;

        int j = 0;
        string storeData = null;
        foreach (var oneByte in port.data)
            {
                j++;
                storeData = oneByte.ToString();
                newData[j] = oneByte;
                storeArray[j] = storeData;
            }
        //copy array
        //storeArray2 = storeArray;
        //print("t > " + newData[6]);
    }

    void analysis()
    {
        


        int index1, index2, index3, index4;
        float value1, value2, value3;
        int size1 = port.data.Length;
        //print("size >" + size1);


        for (int k = 0; k < ((size1 -5) / 36); k++)
        {
            //breaking down serialized data into chunks size 36
            Buffer.BlockCopy(newData, 6 + 36 * k, newData2, newData2.GetLowerBound(0), 36);

            //print("t > " + newData2[0]);


            switch (newData2[0])
            { 
                case 17:
                    index1 = 4;
                    pitch1 = translate(newData2, index1);
              //      print("pitch [deg]>> " + pitch1);
                    index2 = 8;
                    roll1 = translate(newData2, index2);
              //      print("roll [deg]>> " + roll1);
                    index3 = 16;
                    head = translate(newData2, index3);
              //      print("heading [magn]>> " + head);
                    break;
                case 21: 
                    //position
                    index1 = 4;
                    posX = translate(newData2, index1);
                //    print("xpos >> " + posX);
                    index2 = 8;
                    posY = translate(newData2, index2);
               //     print("ypos >> " + posY);
                    index3 = 12;
                    posZ = translate(newData2, index3);
                    //print("zpos >> " + posZ);
                    //velocity_z
                    index4 = 24;
                     velz = translate(newData2, index4);
                //    print("velocity z >> " + velz);
                    index4 = 20;
                     vely = translate(newData2, index4);
               //     print("velocity y >> " + vely);
                    index4 = 16;
                     velx = translate(newData2, index4);
               //     print("velocity x >> " + velx);
                    break;

                case 5:
                    //weather
                    index1 = 8;
                    value1 = translate(newData2, index1);
                //    print("temp C >> " + value1);
                    index2 = 16;
                    value2 = translate(newData2, index2);
                //    print("wind speed>> " + value2);
                    index3 = 20;
                    value3 = translate(newData2, index3);
                 //   print("wind dir >> " + value3);
                    //prec
                    index4 = 28;
                    float prec = translate(newData2, index4);
                 //   print("precipitation >> " + prec);
                    break;

                case 125:
                    //position
                    index1 = 12;
                    ship1 = translate(newData2, index1);
                    //print("pos >> " + ship1);
                    break;

                case 104:
                    //position deck height
                    index1 = 8;
                    PlaneHeight = translate(newData2, index1);
                    print("pos deck and plane >> " + PlaneHeight);
                    //if tailnumber  = 2 it is an f18 , if tailnum = 3 then haweye e2.
                    index2 = 4;
                    tailnumber = translate(newData2, index2);
                    break;


                default:
                    break;
            }
        }
    }

    static float translate(byte[] bytes, int index)
    {
        float values = BitConverter.ToSingle(bytes, index);
        return values;
    }

    // Update is called once per frame
    void Update() {
        
        if ( port.bol == true  &&   coll2.collidePlane == false ) { 
            preanalysis();
            analysis();
        }
    }

} 