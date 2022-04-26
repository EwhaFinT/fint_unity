using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JoinAuction : MonoBehaviour
{
//    public GameObject content;
    public InputField inputShare;
    public Text sumprice, remain, wallet, pricetopay;
    int price = 2000;       //합산 금액
    // Start is called before the first frame update
    void Start()
    {
        GetContent();
        inputShare.onValueChanged.AddListener(ChangePriceToPay);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void ChangePriceToPay(string _data)
    {
        double.TryParse(_data, out double shareprice);
        double tmp = price * shareprice * 0.01;
        pricetopay.text = "예상 지불 금액 : " + tmp + " KLAY";
    }
    public void GetHopeShare(Text text)
    {
        text.text = inputShare.text;

    }
    public void GetContent()
    {
        

//        Text[] joinInfo = content.GetComponentsInChildren<Text>();
        sumprice.text = "합산 금액 : " + price;
        remain.text = "잔여 지분 : " + "45" + " %";
        wallet.text = "잔여 금액 : " + "67.5" + " KLAY";        //내 지갑에서 가져오기
        

    }
}
