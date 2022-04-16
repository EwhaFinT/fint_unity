using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using WebSocketSharp;

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

        webSocket = new WebSocket("ws://fintribe.herokuapp.com/v1/article?articleId=6231f66a15ffd20d91c1b10e");
        webSocket.Connect();

        Debug.Log("websocket 연결 시작");
        webSocket.OnMessage += (sender, e) =>
        {
            Debug.Log($"{((WebSocket)sender).Url}에서 데이터: {e.Data}가 옴.");
        };
    }

    // Update is called once per frame
    void Update()
    {
        if (webSocket == null)
        {
            return;
        }
    }

    public void show()
    {
        board.SetActive(true);
    }

    void onClicked_vote()
    {
        board.SetActive(false);
        // var vote = GameObject.Find("VoteCanvas").GetComponent<VoteScript>();
        // vote.show();
        var votePanel = UIManager.Instance.popupVote.GetComponent<VoteScript>();
        votePanel.show();
        // if(GameObject.Find("VoteCanvas")){
        //     Debug.Log("find vote canvas");
        // }
        // else{
        //     Debug.Log("can't find vote canvas");
        // }
    }

    void onClicked_exit()
    {
        board.SetActive(false);
    }

    void onClicked_write()
    {
        board.SetActive(false);
        // var post = GameObject.Find("PostCanvas").GetComponent<PostScript>();
        // post.show();
        var postPanel = UIManager.Instance.popupPost.GetComponent<PostScript>();
        postPanel.show();
    }

    void onClicked_proposal()
    {
        board.SetActive(false);
        // var post = GameObject.Find("PostCanvas").GetComponent<PostScript>();
        // post.show();
        var proposalPanel = UIManager.Instance.popUpProposal.GetComponent<ProposalScript>();
        proposalPanel.show();
    }
}
