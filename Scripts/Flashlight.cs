using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    public GameObject Player;
    public AudioSource ButtonSource;
    public Light SpotLight;
    private bool l;
    // Start is called before the first frame update
    void Start()
    {
        SpotLight.enabled = false;
        l = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && (Player.tag == "Human"))
        {
            if (l == false)
            {
                SpotLight.enabled = true;
                l = true;
            }
            else
            {
                SpotLight.enabled = false;
                l = false;
            }
            ButtonSource.Play();
        }
    }
}
