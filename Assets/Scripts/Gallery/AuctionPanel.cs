using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class AuctionPanel : MonoBehaviour
{
    public GameObject auctionPanel;
    public GameObject canvasNew, canvasJoin;
    public Button btn_new, btn_close;
    public Text price;
    string artId;
    public GameObject suggestPrefab, content;
    public Button aaa;
    // Start is called before the first frame update
    void Start()
    {
         
 //       aaa.onClick.AddListener(Onclicked_Join);
        btn_new.onClick.AddListener(Onclicked_New);
        btn_close.onClick.AddListener(panelClose);
//        auctionPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator LoadAuctionList()
    {
        // api ???? ???? ????

        string url = Manager.Instance.url + "v1/pricelist?artId=" + artId;

        UnityWebRequest www = UnityWebRequest.Get(url);
        yield return www.SendWebRequest();

        string jsonString = www.downloadHandler.text;
        var response = JsonUtility.FromJson<PricelistResponse>(jsonString);
        Debug.Log("response.pricelist : " + response.pricelist);
        // api ???? ???? ??
        ShowAuctionInfo(response);
        AuctionListInit(response);
    }
    
    //show auction info
    void ShowAuctionInfo(PricelistResponse response)
    {
        price.text = response.price.ToString("F2") + " KLAY";
    }

    void AuctionListInit(PricelistResponse response)
    {
        //content Initialization
        if (content.transform.childCount > 1)
        {
            for (int i = 1; i < content.transform.childCount; i++)
            {
                var a = content.transform.GetChild(i).gameObject;
                Destroy(a);
            }
            Debug.Log("Destory clone all");
        }

        for (int i=0; i<response.pricelist.Count; i++)
        {
            if (response.pricelist[i].auctionPrice == response.price && response.pricelist[i].remainderRatio == 0)      //if response is successful bid, do not instantiate suggest 
                continue;
            else
            {
                GameObject suggest = Instantiate(suggestPrefab);
                suggest.transform.SetParent(content.transform, false);

                var joinBtn = suggest.GetComponent<JoinButton>();
                joinBtn.GetAuctionInfo(response.pricelist[i].priceId, response.pricelist[i].auctionPrice, response.pricelist[i].remainderRatio, artId);
            }
        }
    }


    // show panel
    public void panelStart(string getArtId)
    {
        artId = getArtId;
        Debug.Log("show auction panel");
        auctionPanel.SetActive(true);
        canvasJoin.SetActive(false);
        canvasNew.SetActive(false);
        StartCoroutine(LoadAuctionList());
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
        var newAuctionPanel = UIManager.Instance.auctionNew.GetComponent<NewAuction>();
        newAuctionPanel.panelStart(artId);
        canvasJoin.SetActive(false);
    }

}

// response class (???? ??????)

[System.Serializable]
class PriceResponse
{
    public string priceId;
    public string auctionId;
    public double auctionPrice;     //???? ?? ?? ????
    public double remainderRatio;   //???? ?? ???? ????
}

class PricelistResponse
{
    public double price; // ???? ??????
    public List<PriceResponse> pricelist; // ???? ???? ??????

}

// TO FRONT : ?????????? ???? ???? ?????? ?? ???????? ???? artId ??????????
// TO FRONT : auctionPrice * (1-remainderRatio)?? ?????? ???? ????????????! ?????? priceId?? ?? ??????????