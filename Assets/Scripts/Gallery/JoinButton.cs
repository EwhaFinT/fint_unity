using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JoinButton : MonoBehaviour
{
    public Button suggest;
    string priceId, artId;
    double auctionPrice, remainderRatio;
    // Start is called before the first frame update
    void Start()
    {
        suggest.onClick.AddListener(Onclicked_Join);
    }

    //Update is called once per frame
    void Update()
    {
    }

    public void Onclicked_Join()
    {
        UIManager.Instance.auctionJoin.SetActive(true);
        var joinPanel = UIManager.Instance.auctionJoin.GetComponent<JoinAuction>();
        joinPanel.PanelStart(priceId, auctionPrice, remainderRatio, artId); 
    }

    public void GetAuctionInfo(string priceId, double auctionPrice, double remainderRatio, string artId)
    {
        this.priceId = priceId;
        this.auctionPrice = auctionPrice;
        this.remainderRatio = remainderRatio;
        this.artId = artId;

        ShowText();
    }

    void ShowText()
    {
        double remainPrice = auctionPrice * (1-remainderRatio);
        suggest.GetComponentInChildren<Text>().text = remainPrice.ToString("F0") + " KLAY / " + auctionPrice.ToString("F0") + " KLAY";
    }
}