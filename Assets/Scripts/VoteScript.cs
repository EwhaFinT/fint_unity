using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VoteScript : MonoBehaviour
{   
    public GameObject vote;
    // Start is called before the first frame update
    void Start()
    {
        vote.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void show()
    {
        vote.SetActive(true);
    }
}
