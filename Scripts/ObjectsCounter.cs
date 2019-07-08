using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsCounter : MonoBehaviour
{
    public GameObject Player;
    public int Books = 0;
    public int TBooks = 3;
    public int Keys = 0;
    public int TKeys = 1;

    void OnTriggerEnter(Collider CObject)
    {
        if ((CObject.gameObject.tag == "Book") && (Player.tag == "Human"))
        {
            Books += 1;
            Debug.Log("A book was picked up. Total books = " + Books);
            Destroy(CObject.gameObject);
        }
        if ((CObject.gameObject.tag == "Key") && (Player.tag == "Human"))
        {
            Keys += 1;
            Debug.Log("A key was picked up. Total Keys = " + Keys);
            Destroy(CObject.gameObject);
        }
    }

    void OnGUI()
    {
        if ((Books < TBooks) && (Keys < TKeys))
        {
            GUI.Box(new Rect((Screen.width / 2) - 100, 10, 200, 35), "" + Books + " Books and " + Keys + "Keys");
        }
        else
        {
            Player.tag = "HumanWin";
            GUI.Box(new Rect((Screen.width / 2) - 100, 10, 200, 35), "All Objects Collected");
        }
    }
}
