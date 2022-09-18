using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ArtPanelAr : MonoBehaviour
{
    public Button btn_close, btn_auction, btn_Qrclose, qrcode;
    public GameObject artPanel, qrPanel;
    // Start is called before the first frame update
    void Start()
    {
        qrcode.onClick.AddListener(Onclicked_show);
        btn_close.onClick.AddListener(Onclicked_close);
        btn_Qrclose.onClick.AddListener(Onclicked_Qrclose);
        btn_auction.onClick.AddListener(Onclicked_auction);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void panelStart()
    {
        Debug.Log("show art panel");
        artPanel.SetActive(true);
        //        var auctionPanel = GameObject.Find("Canvas_Auction").GetComponent<AuctionPanel>();

        var auctionPanel = UIManager.Instance.popupAuction.GetComponent<AuctionPanel>();
        auctionPanel.panelClose();

    }
    public void Onclicked_close()
    {
        artPanel.SetActive(false);
        qrPanel.SetActive(false);
    }
    public void Onclicked_Qrclose()
    {
        qrPanel.SetActive(false);
    }

    public void Onclicked_auction()
    {
        artPanel.SetActive(false);
        var auctionPanel = UIManager.Instance.auctionPanelAr.GetComponent<AuctionPanelAr>();
        auctionPanel.panelStart();
    }

    public void Onclicked_show()
    {
        qrPanel.SetActive(true);
    }
}
