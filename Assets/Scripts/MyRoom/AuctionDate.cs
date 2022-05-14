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
    public string yy, mm, dd, finalDate;

    public GameObject warningWindow;    // ���â

    // Start is called before the first frame update
    void Start()
    {
        btn_close.onClick.AddListener(Onclicked_close);
        btn_OK.onClick.AddListener(Onclicked_OK);
        warningWindow = GameObject.Find("WarningWindow");
        warningWindow.SetActive(false);
    }

    public void Onclicked_close()
    {
        gameObject.SetActive(false);
    }
    public void Onclicked_OK()
    {
        //�� �гη� ������
    }

    void GetDate()
    {
        string today = DateTime.Now.ToString("yyyy-MM-dd");

        yy = year.text;
        mm = month.text;
        dd = day.text;

        finalDate = "{yy}-{mm}-{dd}";
        if (CheckValidation(finalDate, today))
            SendDate(finalDate);

    }

    public string SendDate(string date)
    {

        return date;
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
