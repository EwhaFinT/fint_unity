using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JoinAuction : MonoBehaviour
{
//    public GameObject content;
    public InputField inputShare;
    public Text price, remainRatio, remainPrice, pricetopay;

    string priceId;
    double auctionPrice, remainderRatio;

    // Start is called before the first frame update
    void Start()
    {
        inputShare.onValueChanged.AddListener(ChangePriceToPay);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PanelStart(string priceId, double auctionPrice, double remainderRatio) 
    {
        this.priceId = priceId;
        this.auctionPrice = auctionPrice;
        this.remainderRatio = remainderRatio;

        ChangeContent();
    }

    private void ChangePriceToPay(string _data)
    {
        double.TryParse(_data, out double shareprice);
        double tmp = auctionPrice * shareprice * 0.01;
        pricetopay.text = "���� ���� �ݾ� : " + tmp + " KLAY";
    }
    public void GetHopeShare(Text text)
    {
        text.text = inputShare.text;

    }
    public void ChangeContent()
    {
        price.text = "�ջ� �ݾ� : " + auctionPrice +" KLAY";
        remainRatio.text = "�ܿ� ���� : " + remainderRatio*100 + " %";
        remainPrice.text = "�ܿ� �ݾ� : " + auctionPrice*remainderRatio + " KLAY";
    }

    class ParticipateAuctionRequest
    {
        public string userId;
        public string priceId;
        public double ratio;
        public List<string> rlp;
    }

    class ParticipateAuctionResponse
    {
        public string priceId;
    }

    // TO FRONT : ��� ���� ��ȸ�� �� �����ϰ� �ִ� priceId ����մϴ�

}
