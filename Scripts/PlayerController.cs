using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private Animator animator;

    public GameObject externalPlayerPosition;
    public GameObject player;

    public Transform tCamera;
    public float speed = 4.0f;
    private CharacterController cc;
    private bool VR = true;
    public Camera PlayerCamera;
    public float HorizontalSpeed = 2.0f;
    public float VerticalSpeed = 2.0f;
    float h;
    float v;
    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<CharacterController>();
        externalPlayerPosition = GameObject.Find("SharePosition");
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetKey(KeyCode.V)) && (VR == true))
        {
            VR = false;
        }
        if (VR == false)
        {
            h = HorizontalSpeed * Input.GetAxis("Mouse X");
            v = VerticalSpeed * Input.GetAxis("Mouse Y");
            transform.Rotate(0, h, 0);
            PlayerCamera.transform.Rotate(-v, 0, 0);
        }
        if (Input.GetKey(KeyCode.W))
        {
            Vector3 forward = tCamera.TransformDirection(Vector3.forward);
            cc.SimpleMove(forward * speed);
            animator.SetFloat("Blend", 1.0f);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            Vector3 back = tCamera.TransformDirection(Vector3.back);
            cc.SimpleMove(back * speed);
            animator.SetFloat("Blend", -1.0f);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            Vector3 left = tCamera.TransformDirection(Vector3.left);
            cc.SimpleMove(left * speed);
            animator.SetFloat("Blend", 0.0f);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            Vector3 right = tCamera.TransformDirection(Vector3.right);
            cc.SimpleMove(right * speed);
            animator.SetFloat("Blend", 0.0f);
        }

        //if (player.tag == "Player")
        //{
            
            //externalPlayerPosition.transform.position = player.transform.position;
            //Debug.Log("GG" +externalPlayerPosition.transform.position);

          
      //  }


    }
}
