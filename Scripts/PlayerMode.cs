using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMode : MonoBehaviour
{
    public GameObject Player;
    public Renderer Flashlight;
    public GameObject HumanModel;
    // Start is called before the first frame update
    void Start()
    {
        Flashlight.enabled = false;
        HumanModel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Player.tag == "GhostMaster")
        {
            Flashlight.enabled = false;
            HumanModel.SetActive(false);
        }

        if (Player.tag == "Human")
        {
            Flashlight.enabled = true;
            HumanModel.SetActive(true);
        }
    }
}
