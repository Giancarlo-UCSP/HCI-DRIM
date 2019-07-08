using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;

public class FanController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Ghost;
    public SerialPort serial;
    public bool status;

    void Start()
    {
        /*status = false;
        serial = new SerialPort("COM4", 9600);
        serial.Write("0");*/
    }

    // Update is called once per frame
    void Update()
    {
        /*if (serial.IsOpen == false)
        {
            serial.Open();
        }

        if (Ghost.tag == "GhostT")
        {
            serial.Write("1");
        }

        if (Ghost.tag == "GhostF")
        {
            serial.Write("0");
        }*/
    }
}

