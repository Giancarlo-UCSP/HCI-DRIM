using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Health : NetworkBehaviour
{
	public GameObject player;
	public const int maxHealth = 100;
    public int currentHealth = maxHealth;
	//[SyncVar (hook = "OnChangeHealth")] public int currentHealth = maxHealth;
	public RectTransform healthbar;

	public void TakeDamage(int amount)
	{
		if (!isServer)
		{
			return;
		}

		currentHealth -= amount;

		if (currentHealth <= 0)
		{
			currentHealth = 0;
			Debug.Log(player.tag + ": Dead");
        
 		}

        healthbar.sizeDelta = new Vector2(currentHealth * 2, healthbar.sizeDelta.y);        

 		
    }

    void OnChangeHealth(int currentHealth)
    {
    	healthbar.sizeDelta = new Vector2(currentHealth * 2, healthbar.sizeDelta.y);        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    	if (Input.GetKeyDown(KeyCode.K))
    	{
        	TakeDamage(10);
    	}
    	if (Input.GetKeyDown(KeyCode.P))
    	{
        	TakeDamage(-10);
    	}
        
    }

    void OnGUI()
    {
    
    	if (currentHealth == 0)
		{
			GUI.color = Color.red;
			GUI.Box(new Rect((Screen.width / 2) - 200, 25, 200, 25), "Dead player");
 		}
 		else if ((currentHealth > 30) && (currentHealth <=100))
 		{
 			GUI.color = Color.green;
    		GUI.Box(new Rect((Screen.width / 2) - 200, 25, 200, 25), "Life: " + currentHealth);        
    	}
    	else if ((currentHealth <= 30) && (currentHealth > 0))
    	{
 			GUI.color = Color.yellow;
    		GUI.Box(new Rect((Screen.width / 2) - 200, 25, 200, 25), "Life: " + currentHealth);        
    	}        
    }

}
