using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AuctionPanel : MonoBehaviour
{
    public GameObject auctionPanel;
    public GameObject canvasNew, canvasJoin;
    public Button btn_new, btn_join, btn_close;
    // Start is called before the first frame update
    void Start()
    {
        btn_join.onClick.AddListener(Onclicked_Join);
        btn_new.onClick.AddListener(Onclicked_New);
        btn_close.onClick.AddListener(panelClose);
        auctionPanel.SetActive(false);
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
        canvasNew.SetActive(true);
        canvasJoin.SetActive(false);
    }

}
