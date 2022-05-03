using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : Singleton<Manager>
{
    public GameObject playerPrefab;
    [Header("Rooms")]
    public GameObject gallaryPrefab;
    public GameObject communityPrefab;
    public GameObject myroomPrefab;
    [Header("UI Panels")]
    public GameObject UI;
    private PlayerController player;
    //    public GameObject TeleportPanel, Canvas_Art, Canvas_Auction;
    // Start is called before the first frame update


    void Start()
    {
        Debug.Log("--Initialize");
        Instantiate(gallaryPrefab);
        //Instantiate(TeleportPanel);
        //Instantiate(Canvas_Art);
        //Instantiate(Canvas_Auction);
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
