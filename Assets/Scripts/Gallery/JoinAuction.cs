using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class JoinAuction : MonoBehaviour
{
//    public GameObject content;
    public InputField inputShare;
    public Text price, remainRatio, remainPrice, pricetopay;
    public Button joinAuction;

    string _priceId, artId;
    double auctionPrice, _ratio;
    double _remainderRatio;

    // Start is called before the first frame update
    void Start()
    {
        inputShare.onValueChanged.AddListener(ChangePriceToPay);
        joinAuction.onClick.AddListener(Onclick_JoinAuction);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PanelStart(string priceId, double auctionPrice, double remainderRatio, string artId) 
    {
        _priceId = priceId;
        this.auctionPrice = auctionPrice;
        _remainderRatio = remainderRatio;
        this.artId = artId;

        ChangeContent();
    }

    void Onclick_JoinAuction()
    {
        StartCoroutine(SendJoinInfo());

    }

    IEnumerator SendJoinInfo()
    {
        ParticipateAuctionRequest participateAuctionRequest = new ParticipateAuctionRequest
        {
            userId = Manager.Instance.ID,
            priceId = _priceId,
            ratio = _ratio/100
        };
        Debug.Log("userid : "+ participateAuctionRequest.userId);
        string url = "https://fintribe.herokuapp.com/v1/participate";
        string jsonBody = JsonUtility.ToJson(participateAuctionRequest);

        UnityWebRequest www = UnityWebRequest.Post(url, jsonBody);
        byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(jsonBody);
        www.uploadHandler = new UploadHandlerRaw(jsonToSend);
        www.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        www.SetRequestHeader("Content-Type", "application/json");
        yield return www.SendWebRequest();


        string jsonString = www.downloadHandler.text;
        var response = JsonUtility.FromJson<ParticipateAuctionResponse>(jsonString);

        if (response.priceId == "0")
        {
            // 경매 참여 실패 : 잔여 지분을 초과한 경우
        }
        else
        {
            // 경매 참여 성공
        }

        // api 통신 부분 끝
        //auction panel refresh
        var auctionPanel = UIManager.Instance.popupAuction.GetComponent<AuctionPanel>();
        auctionPanel.panelStart(artId);
    }


    private void ChangePriceToPay(string _data)
    {
        double.TryParse(_data, out double shareprice);
        _ratio = shareprice;
        double tmp = auctionPrice * shareprice * 0.01;
        pricetopay.text = "예상 지불 금액 : " + tmp.ToString("F2") + " KLAY";
    }

    public void ChangeContent()
    {
        price.text = "합산 금액 : " + auctionPrice.ToString("F2") +" KLAY";
        remainRatio.text = "잔여 지분 : " + (_remainderRatio*100).ToString("F2") + " %";
        remainPrice.text = "잔여 금액 : " + (auctionPrice*_remainderRatio).ToString("F2") + " KLAY";
    }

    class ParticipateAuctionRequest
    {
        public string userId;
        public string priceId;
        public double ratio;       //myratio
    }

    class ParticipateAuctionResponse
    {
        public string priceId;
    }

    // TO FRONT : 경매 정보 조회할 때 저장하고 있던 priceId 사용합니다

}
