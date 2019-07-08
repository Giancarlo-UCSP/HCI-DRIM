using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SpawnGhost : NetworkBehaviour
{
    public AudioSource SpawnSource;
    public GameObject Player;
    public GameObject Ghost;
    [SyncVar] public int Ghosts = 0;
    public const int MaxGhosts = 5;
    private Vector3 SpawnPoint;
    public int deletedGhosts = 0;

    void Update()
    {
        if (!isLocalPlayer)
        {
            return;
        }
        if ((Input.GetKeyDown(KeyCode.G)) && (Player.tag == "GhostMaster") && (Ghosts < MaxGhosts))
        {
            SpawnPoint = Player.transform.position;
            SpawnPoint[2] += 0.5f;
            SpawnPoint[1] += 1.0f;
            SpawnPoint[2] += 0.5f;
            CmdNewGhost();
            Ghosts += 1;
            Debug.Log("Ghost+=1");
        }
    }

    [Command]
    void CmdNewGhost()
    {
        SpawnSource.Play();
        GameObject obj = Instantiate(Ghost, SpawnPoint, Quaternion.identity);
        NetworkServer.Spawn(obj);
    }

    public void DeleteGhost()
    {
        if (!isServer)
        {
            return;
        }
        Ghosts -= 1;
        Debug.Log("Ghost-=1");
    }

    void OnGUI()
    {
        if (Player.tag == "GhostMaster")
        {
            GUI.Box(new Rect((Screen.width / 2) - 200, 55, 200, 25), "Active Ghosts: " + Ghosts);                
        }
    }
}
