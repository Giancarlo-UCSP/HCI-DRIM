using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ObjectsSelector : NetworkBehaviour
{
    public GameObject Player;
    public int Objects = 0;
    public int TObjects = 4;
    public Material Highlight;
    public Material DefaultB;
    public Material DefaultK;
    private Transform _selection;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Player.tag == "Human")
        {
            if (_selection != null)
            {
                var selectionRenderer = _selection.GetComponent<Renderer>();
                if (selectionRenderer.CompareTag("Book"))
                {
                    selectionRenderer.material = DefaultB;
                }
                else if (selectionRenderer.CompareTag("Key"))
                {
                    selectionRenderer.material = DefaultK;
                }
                _selection = null;
            }
            var ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 5.0f))
            {
                var selection = hit.transform;
                if ((selection.CompareTag("Book")) || (selection.CompareTag("Key")))
                {
                    var selectionRenderer = selection.GetComponent<Renderer>();
                    if (selectionRenderer != null)
                    {
                        selectionRenderer.material = Highlight;
                        if (Input.GetKeyDown(KeyCode.P))
                        {
                            Objects += 1;
                            Debug.Log("A object was picked up. Total objects = " + Objects);
                            //Destroy(hit.collider.gameObject);
                            CmdDestroyObject(hit.collider.gameObject);
                        }
                    }
                    _selection = selection;
                }
            }
            if (Objects == TObjects)
            {
                //Player.layer = LayerMask.NameToLayer("HumanWin");
                Player.tag = "HumanWin";
            }
        }
    }
    [Command]
    void CmdDestroyObject(GameObject obj)
    {
        //if (!obj) return;
        NetworkServer.Destroy(obj);
    }
}
