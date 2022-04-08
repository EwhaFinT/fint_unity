using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VoteScript : MonoBehaviour
{   
    public GameObject vote;
    public Button exit;   //떠오르게 할 팝업창
    // Start is called before the first frame update
    void Start()
    {
        vote.SetActive(false);
        exit.onClick.AddListener(onClicked_exit);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void show()
    {
        vote.SetActive(true);
    }
    void onClicked_exit()
    {
        vote.SetActive(false);
    }
}
