using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class AuctionPanel3d : MonoBehaviour
{
    public GameObject auctionPanel;
    public GameObject canvasNew, canvasJoin;
    public Button btn_new, btn_close;

    // Start is called before the first frame update
    void Start()
    {
        btn_new.onClick.AddListener(Onclicked_New);
        btn_close.onClick.AddListener(panelClose);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void panelStart()
    {
        Debug.Log("show auction panel");
        auctionPanel.SetActive(true);
        canvasJoin.SetActive(false);
        canvasNew.SetActive(false);
    }
    public void panelClose()
    {
        auctionPanel.SetActive(false);
    }

    public void Onclicked_Join()
    {
        canvasJoin.SetActive(true);
        canvasNew.SetActive(false);
    }

    public void Onclicked_New()
    {
        var newAuctionPanel = UIManager.Instance.auctionNew3d.GetComponent<NewAuction3d>();
        newAuctionPanel.panelStart();
        canvasJoin.SetActive(false);
    }
}
