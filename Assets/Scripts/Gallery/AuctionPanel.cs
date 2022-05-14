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
        // api 통신 부분 시작

        string url = "https://fintribe.herokuapp.com/v1/pricelist?artId=" + artId;

        UnityWebRequest www = UnityWebRequest.Get(url);
        yield return www.SendWebRequest();

        string jsonString = www.downloadHandler.text;
        var response = JsonUtility.FromJson<PricelistResponse>(jsonString);
        Debug.Log("esponse.pricelist : " + response.pricelist);
        // api 통신 부분 끝
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

// response class (외부 클래스)

[System.Serializable]
class PriceResponse
{
    public string priceId;
    public string auctionId;
    public double auctionPrice;     //제안 내 총 가격
    public double remainderRatio;   //제안 내 남은 지분
}

class PricelistResponse
{
    public double price; // 현재 상한가
    public List<PriceResponse> pricelist; // 기존 제안 리스트

}

// TO FRONT : 메인홀에서 전체 작품 조회할 때 저장하고 있던 artId 사용합니다
// TO FRONT : auctionPrice * (1-remainderRatio)로 경매가 현황 계산해주세요! 그리고 priceId도 꼭 저장해놓기