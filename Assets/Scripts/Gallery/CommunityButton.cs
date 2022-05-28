using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CommunityButton : MonoBehaviour
{
    public Button community;
    string communityId, artName;
    // Start is called before the first frame update
    void Start()
    {
        community.onClick.AddListener(Onclicked_community);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Onclicked_community()
    {
        CommunityManager.Instance.CommunityID = communityId;    //assign community id to communityPrefab
        Debug.Log("community prefab ID : "+CommunityManager.Instance.CommunityID);
        var teleport = UIManager.Instance.popupTeleport.GetComponent<TeleportPanel>();
        teleport.MoveCommunity();       //move to community


    }

    public void GetCommunityInfo(string communityId, string artName)
    {
        this.artName = artName;
        this.communityId = communityId;

        ShowText();
    }

    void ShowText()
    {
        community.GetComponentInChildren<Text>().text = artName;
    }
}
