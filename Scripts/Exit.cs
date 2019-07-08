using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : MonoBehaviour
{
    public GameObject Player;
    public bool BExit = false;
    public bool BWin = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if ( (BExit == true) && (Player.tag == "HumanWin") )
        {
            BWin = true;
            Debug.Log("HumanWin");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Exit")
        {
            BExit = true;
        }
    }
}
