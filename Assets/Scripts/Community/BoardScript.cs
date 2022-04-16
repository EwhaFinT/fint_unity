using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using WebSocketSharp;
using TMPro;

public class BoardScript : MonoBehaviour
{   
    public GameObject board;
    public Button voteBtn;
    public Button BoardExitBtn;
    public Button WriteBtn;
    public Button ProposalBtn;

    private WebSocket webSocket;


    // Start is called before the first frame update
    void Start()
    {
        // board.SetActive(false);
        voteBtn.onClick.AddListener(onClicked_vote);
        BoardExitBtn.onClick.AddListener(onClicked_exit);
        WriteBtn.onClick.AddListener(onClicked_write);
        ProposalBtn.onClick.AddListener(onClicked_proposal);

        
    }

    // Update is called once per frame
    void Update()
    {
        if (webSocket == null)
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            webSocket.Send("Hello");
        }
    }

    public void show()
    {
        board.SetActive(true);
    }

    void onClicked_vote()
    {
        board.SetActive(false);
        
        var votePanel = UIManager.Instance.popupVote.GetComponent<VoteScript>();
        votePanel.show();
    }

    void onClicked_exit()
    {
        board.SetActive(false);
    }

    void onClicked_write()
    {
        board.SetActive(false);
        
        var postPanel = UIManager.Instance.popupPost.GetComponent<PostScript>();
        postPanel.show();
    }

    void onClicked_proposal()
    {
        board.SetActive(false);
        
        var proposalPanel = UIManager.Instance.popUpProposal.GetComponent<ProposalScript>();
        proposalPanel.show();
    }
}
