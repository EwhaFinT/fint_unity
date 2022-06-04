using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class PostPopupScript : MonoBehaviour
{
    public GameObject PostPopUp;
    public Button exit;
    public TextMeshProUGUI message;

    // Start is called before the first frame update
    void Start()
    {
        exit.onClick.AddListener(onClicked_exit);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void MakePopupWarn(long status)  // ???? ?????
    {
        TMP_Text message = PostPopUp.transform.GetChild(0).GetChild(0).GetComponent<TMP_Text>();
        Debug.Log("status: " + status);
        if(status == 200)
        {
            message.text = "성공적으로 등록되었습니다.";
        }
        else
        {
            message.text = "등록에 실패했습니다.";
        }
        PostPopUp.SetActive(true);
    }
    void onClicked_exit()
    {
        var postPanel = UIManager.Instance.popupPost.GetComponent<PostScript>();
        var proposalPanel = UIManager.Instance.popUpProposal.GetComponent<ProposalScript>();
        var votePanel = UIManager.Instance.popupVote.GetComponent<VoteScript>();

        PostPopUp.SetActive(false);
        postPanel.onClicked_exit();
        proposalPanel.onClicked_exit();
        //votePanel.onClicked_exit();
        //votePanel.ReloadVote();
        if (votePanel.isActiveAndEnabled)
        {
            votePanel.ReloadVote();
        }
    }
}
