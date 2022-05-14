using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class AuctionDate : MonoBehaviour
{
    public Button btn_close, btn_OK;
    public TMP_InputField year, month, day;
    string yy, mm, dd, finalDate;


    // Start is called before the first frame update
    void Start()
    {
        btn_close.onClick.AddListener(Onclicked_close);
        btn_OK.onClick.AddListener(Onclicked_OK);
    }

    public void Onclicked_close()
    {
        gameObject.SetActive(false);
    }
    public void Onclicked_OK()
    {
        //�� �гη� ������
        GetDate();
    }

    void GetDate()
    {
        string today = DateTime.Now.ToString("yyyy-MM-dd");

        yy = year.text;
        mm = month.text;
        dd = day.text;

        finalDate = $"{yy}-{mm}-{dd}";
        if (CheckValidation(finalDate, today))
            SendDate(finalDate);
//        SendDate(finalDate);
    }

    public void SendDate(string date)
    {

        var auctionPanel = UIManager.Instance.popupRegister.GetComponent<RegisterFormController>();
        auctionPanel.GetDate(date);
    }

    bool CheckValidation(string date, string today)
    {
        if (String.Compare(date, today) != 1)
        {
            var popupWarn = UIManager.Instance.popupWarn.GetComponent<PopupWarnController>();
            popupWarn.MakePopupWarn("��� ���ڴ�\n���� ���Ŀ��� �մϴ�.");
            return false;
        }
        else
            return true;
    }


}
