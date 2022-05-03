using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class ArtPanel : MonoBehaviour
{
    public Button btn_close, btn_auction;
    public GameObject artPanel;
    public RawImage thisImg;
    public Text artInfo;
    public string artId;
    

    // Start is called before the first frame update
    void Start()
    {
        btn_close.onClick.AddListener(Onclicked_close);
        btn_auction.onClick.AddListener(Onclicked_auction);
//        artPanel.SetActive(false);
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
    }

    public void Onclicked_auction()
    {
        artPanel.SetActive(false);
        var auctionPanel = UIManager.Instance.popupAuction.GetComponent<AuctionPanel>();
        auctionPanel.panelStart(artId);
    }

    public void changeImg(GameObject frame)
    {
        var x = thisImg.GetComponent<RawImage>();
        Material[] mt = frame.GetComponent<Renderer>().materials;
        x.texture = mt[1].GetTexture("_MainTex");
    }

    public void getFrame(GameObject frame)
    {
        ArtChange artChange = ArtChange.Instance.GetComponent<ArtChange>();
        artId = artChange.dic[frame];
        StartCoroutine(LoadArtInfo());
    }

    IEnumerator LoadArtInfo()     // ???????? ???? ???? ????????
    {
        //string url = "http://localhost:8080/v1/art-info?artId=" + artId;
        string url = "https://fintribe.herokuapp.com/v1/art-info?artId=" + artId;

        UnityWebRequest www = UnityWebRequest.Get(url);
        yield return www.SendWebRequest();

        string jsonString = www.downloadHandler.text;
        var response = JsonUtility.FromJson<ArtInfoResponse>(jsonString);

        changeArtInfo(response);
    }

    void changeArtInfo(ArtInfoResponse response)
    {
        artInfo.text =
            "?????? : " + response.artName + "\n" +
            "???? : " + response.painter + "\n" +
            "???? ???? : " + response.detail + "\n";
    }
}

class ArtInfoResponse {
    public string artId;
    public string painter; // ????
    public string artName; // ??????
    public string detail;
    public double price; // ??????
    public string nftAdd;
    public string paint; // ?????? url ????
    public bool sold;
    public List<string> userId;
    public List<double> ratio; // ???? ???????? ????
}