using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor;
using System;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

public class RegisterFormController : MonoBehaviour
{
    string path;
    public Text filePath;

    // ����� �Է°�
    public TMP_InputField title;           // ��ǰ��
    public TMP_InputField author;          // �۰�
    public TMP_InputField reservedPrice;   // ��� ���۰�
    public Text auctionDate;               // ��� ����
    public TMP_InputField description;     // ��ǰ ����


    public GameObject warningWindow;    // ���â

    void Start()
    {
        warningWindow = GameObject.Find("WarningWindow");
        warningWindow.SetActive(false);
    }

    bool CheckValidation()  // ��ȿ�� �˻�
    {
        string today = DateTime.Now.ToString("yyyy-MM-dd");

        if (path == null || path.Length < 4)
        {
            MakeWarningWindow("��ǰ ������ �����ϼ���");
            return false;
        }
        if (title.text.Length < 2)
        {
            MakeWarningWindow("��ǰ����\n2���� �̻��̾�� �մϴ�.");
            return false;
        }
        if (author.text.Length < 2)
        {
            MakeWarningWindow("�۰� �̸���\n2���� �̻��̾�� �մϴ�.");
            return false;
        }
        if (reservedPrice.text.Length < 1)
        {
            MakeWarningWindow("��� ���۰��� �Է��ϼ���.");
            return false;
        }
        if (!Regex.IsMatch(auctionDate.text, @"^(20|21)\d{2}-(0[1-9]|1[012])-(0[1-9]|[12][0-9]|3[0-1])$"))
        {
            MakeWarningWindow("��� ���ڸ� �����ϼ���.");
            return false;
        }
        if (String.Compare(auctionDate.text, today) != 1)
        {
            MakeWarningWindow("��� ���ڴ�\n���� ���Ŀ��� �մϴ�.");
            return false;
        }
        if(description.text.Length < 3)
        {
            MakeWarningWindow("��ǰ ������\n3���� �̻��̾�� �մϴ�.");
            return false;

        }
        return true;
    }

    void RegisterButtonClick()   // ��ǰ ��� ��ư Ŭ����
    {
        if (CheckValidation())   // ��ȿ�� �˻� ���
        {
            SendNewFileInfo();

            // ==== ������ ����ϴ� �κ� �ʿ� ====
            // ==== NFT API ����ؼ� NFT �ּҵ� ������ �� ====

            // ��ǰ ���������� ��Ͻ� ��ǰ ��� �� ����
            InitializeForm();
            gameObject.SetActive(false);
        }
    }

    void OpenExplorerButtonClick()   // ���� Ž���� ��ư Ŭ����
    {
        // (pc �������� ������ ��, ����Ϸ� �� ��� �����ؾ� �� ��)
        path = EditorUtility.OpenFilePanel("�� ��ǰ ����ϱ�", "", "jpg");
        GetImage();
    }

    void GetImage()     // ��ǰ ���ý� ��ư text �����ϱ�
    {
        if(path != null)
        {
            filePath.text = path;
        }
    }

    void InitializeForm()   // ��ǰ ��� �� �ʱ�ȭ
    {
        path = null;
        filePath.text = "���� Ž��";
        ClearInputField(title);
        ClearInputField(author);
        ClearInputField(description);
        ClearInputField(reservedPrice);
        auctionDate.text = "��� ���� ����";
    }

    void ClearInputField(TMP_InputField inputField)
    {
        inputField.text = "";
    }

    void MakeWarningWindow(string msg)  // ���â �����
    {
        TMP_Text message = warningWindow.transform.GetChild(0).GetComponent<TMP_Text>();
        message.text = msg;
        warningWindow.SetActive(true);
    }

    public void WarningWindowCloseButtonClick()     // ���â �ݱ�
    {
        warningWindow.SetActive(false);
    }

    void SendNewFileInfo()
    {
        GameObject emptyGameObject = GameObject.Instantiate(
                new GameObject(filePath.text),
                this.transform.position,
                Quaternion.identity
                );
        emptyGameObject.transform.SetParent(this.transform);
    }
}