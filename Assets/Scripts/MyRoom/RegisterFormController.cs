using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor;
using System;
using System.Text.RegularExpressions;
using UnityEngine.Networking;
using System.IO;
using Newtonsoft.Json.Linq;

public class RegisterFormController : MonoBehaviour
{
    string path, artUrl, artId;
    public Text filePath;
    bool success = false;

    // ����� �Է°�
    public TMP_InputField title;           // ��ǰ��
    public TMP_InputField author;          // �۰�
    public TMP_InputField reservedPrice;   // ��� ���۰�
    public Text auctionDate;               // ��� ����
    public TMP_InputField description;     // ��ǰ ����
    //public Button btn_right, btn_left;
    //public GameObject firstPanel, secondPanel;
    public GameObject warningWindow;    // ���â
    public GameObject calendar;

    public Button btn_close, btn_FindFile, btn_upload, btn_pickDate;
    void Start()
    {
        btn_close.onClick.AddListener(Onclicked_close);
        btn_FindFile.onClick.AddListener(OpenExplorerButtonClick);
        btn_upload.onClick.AddListener(RegisterButtonClick);
        btn_pickDate.onClick.AddListener(ShowCalendar);
        warningWindow = GameObject.Find("WarningWindow");
        warningWindow.SetActive(false);
    }
    public void Onclicked_close()
    {
        gameObject.SetActive(false);
    }


    public void ShowCalendar()
    {
        calendar.SetActive(true);
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
        //if (!Regex.IsMatch(auctionDate.text, @"^(20|21)\d{2}-(0[1-9]|1[012])-(0[1-9]|[12][0-9]|3[0-1])$"))
        //{
        //    MakeWarningWindow("��� ���ڸ� �����ϼ���.");
        //    return false;
        //}
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

            // ==== ������ ����ϴ� �κ�====
            StartCoroutine(ImageUpload(path));
            
            // ==== NFT API ����ؼ� NFT �ּҵ� ������ �� ====

            // ��ǰ ���������� ��Ͻ� ��ǰ ��� �� ����
            if (success == true)
            {
                InitializeForm();
                gameObject.SetActive(false);
            }

        }
    }


    public void OpenExplorerButtonClick()   // ���� Ž���� ��ư Ŭ����
    {
        // (pc �������� ������ ��, ����Ϸ� �� ��� �����ؾ� �� ��)
        path = EditorUtility.OpenFilePanel("�� ��ǰ ����ϱ�", "", "jpg");
        GetImage();
        //StartCoroutine(ImageUpload(path));
    }

    IEnumerator ImageUpload(string path)
    {
        Debug.Log("start upload img");
        // �̹��� base64string���� ��ȯ
        FileInfo file = new FileInfo(path);
        byte[] byteTexture = File.ReadAllBytes(file.FullName);

        // �̹��� ���� post ��û
        string url = "https://api.imgbb.com/1/upload?key=8bb1d0057129896bc0546e05caf616b4";
        WWWForm form = new WWWForm();
        string base64string = Convert.ToBase64String(byteTexture);
        form.AddField("image", base64string);

        UnityWebRequest www = UnityWebRequest.Post(url, form);
        www.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        yield return www.SendWebRequest();

        string jsonString = www.downloadHandler.text;
        var data = JObject.Parse(jsonString);
        
        artUrl = data["data"]["url"].ToString();         //get art url part from jsonString

        Debug.Log("complete upload img");

        StartCoroutine(SendReguisterInfo());
    }

    IEnumerator SendReguisterInfo()
    {
        
        string url = "https://fintribe.herokuapp.com/v1/upload";

        
        //DateTime tmp = new DateTime(2022, 05, 23);
        //Debug.Log(tmp);
        //Debug.Log(tmp.Date);
        double.TryParse(reservedPrice.text, out double price);
        UploadRequest uploadRequest = new UploadRequest
        {
            userId = Manager.Instance.ID,
            painter = author.text,
            artName = title.text,
            detail = description.text,
            price = price,
            paint = artUrl,
            auctionDate = "2022-05-12"
        };

        string jsonBody = JsonUtility.ToJson(uploadRequest);

        UnityWebRequest www = UnityWebRequest.Post(url, jsonBody);
        byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(jsonBody);
        www.uploadHandler = new UploadHandlerRaw(jsonToSend);
        www.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        www.SetRequestHeader("Content-Type", "application/json");
        yield return www.SendWebRequest();

        string jsonString = www.downloadHandler.text;
        var response = JsonUtility.FromJson<UploadResponse>(jsonString);

        if (response.artId == null)
        {
            MakeWarningWindow("��ǰ ���ε� ����");
            success = false;
        }
        else
        {
            response.artId = artId;
            success = true;
        }
            
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
class UploadRequest
{
    public string userId;
    public string painter; // �۰�
    public string artName; // ��ǰ��
    public string detail;
    public double price; // ��Ű�
    public string paint; // �̹��� url �ּ�
    public string auctionDate;
}

class UploadResponse
{
    public string artId;
}