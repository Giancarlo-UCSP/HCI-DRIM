using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SpawnObjects : NetworkBehaviour
{
    public GameObject[] SpawnPointsB;
    public GameObject[] SpawnPointsK;
    public GameObject Player;
    public GameObject Book;
    public GameObject Key;
    private bool start = false;
    private int randomB = 0;
    private int randomK = 0;
    // Start is called before the first frame update
    void Start()
    {
        SpawnPointsB = GameObject.FindGameObjectsWithTag("SpawnPointB");
        SpawnPointsK = GameObject.FindGameObjectsWithTag("SpawnPointK");
    }

    // Update is called once per frame
    void Update()
    {
        if (!isLocalPlayer)
        {
            return;
        }
        if ((Input.GetKeyDown(KeyCode.O)) && (Player.tag == "Human") && (start == false))
        {
            CmdSpawnObjets();
            start = true;
        }
    }

    [Command]
    void CmdSpawnObjets()
    {
        for (int i=0; i<4; i++)
        {
            randomB = Random.Range(0, SpawnPointsB.Length);
            GameObject objB = Instantiate(Book, SpawnPointsB[randomB].transform.position, Quaternion.identity);
            NetworkServer.Spawn(objB);
        }
        randomK = Random.Range(0, SpawnPointsK.Length);
        GameObject objK = Instantiate(Key, SpawnPointsK[randomK].transform.position, Quaternion.identity);
        NetworkServer.Spawn(objK);
    }
}
