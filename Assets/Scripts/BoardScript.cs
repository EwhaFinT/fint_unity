using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoardScript : MonoBehaviour
{   
    public GameObject board;
    public Button voteBtn;
    public Button BoardExitBtn;

    // Start is called before the first frame update
    void Start()
    {
        board.SetActive(false);
        voteBtn.onClick.AddListener(onClicked_vote);
        BoardExitBtn.onClick.AddListener(onClicked_exit);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void show()
    {
        board.SetActive(true);
    }
    void onClicked_vote()
    {
        // board.SetActive(false);
        // var vote = GameObject.Find("VoteCanvas").GetComponent<VoteScript>();
        // vote.show();
        if(GameObject.Find("VoteCanvas")){
            Debug.Log("find vote canvas");
        }
        else{
            Debug.Log("can't find vote canvas");
        }
    }
    void onClicked_exit()
    {
        board.SetActive(false);
    }
}
