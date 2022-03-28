using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject gallaryprefab;
    public GameObject communityPrefab;
    public GameObject panel;

    private PlayerController player;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("--Initialize");
        Instantiate(gallaryprefab);
        var community = Instantiate(communityPrefab);
        community.transform.position = new Vector3(0, 10, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
