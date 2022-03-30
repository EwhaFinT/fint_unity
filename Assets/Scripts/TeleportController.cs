using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TeleportController : MonoBehaviour
{
//    public GameObject teleportPanel;
    public Vector3 targetPos;
    // Start is called before the first frame update
    void Start()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("on enter");

        var panel = GameObject.Find("Canvas_Teleport").GetComponent<TeleportPanel>();
        var player = other.gameObject.GetComponent<PlayerController>();

        if (player != null)
        {
            panel.panelStart();     //show teleport panel
//            player.SetMove(targetPos);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
