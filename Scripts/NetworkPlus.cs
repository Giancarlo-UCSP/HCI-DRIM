using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkPlus : NetworkBehaviour
{
	public GameObject player;
	[SyncVar] public int playersCounter;
    public bool[] activeGhosts;

    // Start is called before the first frame update
    public override void OnStartServer()
    {
    	playersCounter = 0;
    	const int playerSize = 2;
        activeGhosts = new bool[5];

    	for (int i=0; i<5; i++)
    	{
    		activeGhosts[i] = false;
    	}

		Debug.Log("NetworkPlus: The Server has been started.");
    }

    public override void OnStartClient()
    {
    	Debug.Log("NetworkPlus: New Client has been joined. ID: "+netId);
    	int nId = int.Parse( netId.ToString() );
    	playersCounter = nId; // - 2;
    	Debug.Log("NetworkPlus: Active Players: "+playersCounter);
    	
    	if (playersCounter == 1)
    	{
    		player.tag = "GhostMaster";
    	}

    	if (playersCounter == 2)
    	{
    		player.tag = "Human";
    	}

        if (playersCounter >= 3)
        {
            activeGhosts[playersCounter-3] = true;
        }

		
    }


    
}
