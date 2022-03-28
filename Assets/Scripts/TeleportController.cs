using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportController : MonoBehaviour
{
    public Vector3 targetPos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("on enter");
        var player = other.gameObject.GetComponent<PlayerController>();

        if (player != null)
        {
            player.SetMove(targetPos);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
