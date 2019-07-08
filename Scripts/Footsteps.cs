using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footsteps : MonoBehaviour
{
    public AudioClip WalkClip;
    public AudioSource WalkSource;
    public float AudioStepLenghtWalk = 0.45f;
    public float AudioStepLenghtRun = 0.25f;
    private CharacterController cc;
    private bool step = true;

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        cc = GetComponent<CharacterController>();
        if ((cc.isGrounded == true) && (cc.velocity.magnitude < 4.0f) && (cc.velocity.magnitude > 2.0f) &&
            (step == true) && (hit.gameObject.tag == "Ground"))
        {
            WalkOnGround();
        }
    }
    void WalkOnGround()
    {
        WalkSource.clip = WalkClip;
        WalkSource.volume = Random.Range(0.8f, 1.0f);
        WalkSource.pitch = Random.Range(0.8f, 1.0f);
        WalkSource.Play();
        StartCoroutine(WaitForFootSteps(AudioStepLenghtWalk));
    }

    IEnumerator WaitForFootSteps(float StepsLenght)
    {
        step = false;
        yield return new WaitForSeconds(StepsLenght);
        step = true;
    }
}
