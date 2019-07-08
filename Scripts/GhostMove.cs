using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostMove : MonoBehaviour
{
    public GameObject Player;
    public GameObject Ghost;
    public float Speed = 2.0f;
    public float MinDist = 4.0f;
    public float MaxDist = 8.0f;
    public float BDist = 12.0f;
    public bool Detection = false;
    public bool BLose = false;
    public Vector3 ipos;

    // Start is called before the first frame update
    void Start()
    {
        ipos = Player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Player.tag == "Human")
        {
            //Move
            Ghost.transform.LookAt(Player.transform);
            if ((Vector3.Distance(Ghost.transform.position, Player.transform.position) >= MinDist) &&
                 (Vector3.Distance(Ghost.transform.position, Player.transform.position) <= MaxDist))
            {
                Ghost.transform.position += Ghost.transform.forward * Speed * Time.deltaTime;
            }
            else if (Vector3.Distance(Ghost.transform.position, Player.transform.position) > MaxDist)
            {
                Ghost.transform.position = ipos;
            }
            else if (Vector3.Distance(Ghost.transform.position, Player.transform.position) < MinDist)
            {
                BLose = true;
                Health get_health = Player.GetComponent<Health>();
                if (get_health != null)
                {
                    get_health.TakeDamage(10);
                }
            }
            //Detection
            if (Vector3.Distance(Ghost.transform.position, Player.transform.position) <= BDist)
            {
                Detection = true;
                Ghost.tag = "GhostT";
            }
            else
            {
                Detection = false;
                Ghost.tag = "GhostF";
            }
        }
    }

    private void OnGUI()
    {
        if (BLose == true)
        {
            GUI.Box(new Rect((Screen.width / 2) - 100, 50, 200, 35), "You Lose");
        }
    }
}
