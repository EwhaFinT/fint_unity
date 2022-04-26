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
    int price = 2000;       //�ջ� �ݾ�
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
        pricetopay.text = "���� ���� �ݾ� : " + tmp + " KLAY";
    }
    public void GetHopeShare(Text text)
    {
        text.text = inputShare.text;

    }
    public void GetContent()
    {
        

//        Text[] joinInfo = content.GetComponentsInChildren<Text>();
        sumprice.text = "�ջ� �ݾ� : " + price;
        remain.text = "�ܿ� ���� : " + "45" + " %";
        wallet.text = "�ܿ� �ݾ� : " + "67.5" + " KLAY";        //�� �������� ��������
        

    }
}
