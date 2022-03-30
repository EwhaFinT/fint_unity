using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject gallaryPrefab;
    public GameObject communityPrefab;
    public GameObject myroomPrefab;

    private PlayerController player;
    private TeleportPanel TeleportPanel;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("--Initialize");
        Instantiate(gallaryPrefab);
        var community = Instantiate(communityPrefab);
        var myroom = Instantiate(myroomPrefab);
        community.transform.position = new Vector3(0, 10, 0);
        myroom.transform.position = new Vector3(0, 25, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
