using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class NewAuction : MonoBehaviour
{
    public InputField inputPrice, inputShare;
    public Text pricetopay;
    double sumprice, _ratio;
    string _artId;
    public Button newAuction_btn;

    // Start is called before the first frame update
    void Start()
    {
        inputPrice.onValueChanged.AddListener(InputPrice_Text);
        inputShare.onValueChanged.AddListener(ChangePriceToPay);
        newAuction_btn.onClick.AddListener(Onclick_NewAuction);
    }

    public void panelStart(string getArtId)
    {
        InitializeForm();
        _artId = getArtId;
        gameObject.SetActive(true);
    }

    void Onclick_NewAuction()
    {
        StartCoroutine(SendNewInfo());
    }

    void InitializeForm()   //Initialization
    {
        inputPrice.text = "";
        inputShare.text = "";
    }


    IEnumerator SendNewInfo()
    {
        string url = Manager.Instance.url + "v1/price";
        NewPriceRequest newPriceRequest = new NewPriceRequest
        {
            userId = Manager.Instance.ID,
            artId = _artId,
            auctionPrice = sumprice,
            ratio = _ratio/100
        };

        string jsonBody = JsonUtility.ToJson(newPriceRequest);

        UnityWebRequest www = UnityWebRequest.Post(url, jsonBody);
        byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(jsonBody);
        www.uploadHandler = new UploadHandlerRaw(jsonToSend);
        www.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        www.SetRequestHeader("Content-Type", "application/json");
        yield return www.SendWebRequest();

        string jsonString = www.downloadHandler.text;
        var response = JsonUtility.FromJson<NewPriceResponse>(jsonString);

        if (response.priceId == "0")
        {
            var popupWarn = UIManager.Instance.popupWarn.GetComponent<PopupWarnController>();
            popupWarn.MakePopupWarn("Please check\n the input again.");
        }
        else
        {
            var popup = UIManager.Instance.popupSuccess.GetComponent<PopupSuccessController>();
            popup.MakePopupMessage("Success!\n E-mail will be sent at midnight\n if you win the auction.");
        }

        // api 烹脚 何盒 场
        //auction panel refresh
        var auctionPanel = UIManager.Instance.popupAuction.GetComponent<AuctionPanel>();
        auctionPanel.panelStart(_artId);
    }

    private void InputPrice_Text(string _data)
    {
        double.TryParse(_data, out double tmp);
        sumprice = tmp;
    }

    private void ChangePriceToPay(string _data)
    {
        double.TryParse(_data, out double shareprice);
        _ratio = shareprice;
        double tmp = sumprice * shareprice * 0.01;
        pricetopay.text = "예상 지불 금액 : " + tmp.ToString("F2") + " KLAY";
    }

}
class NewPriceRequest
{
    public string userId;
    public string artId;
    public double auctionPrice;
    public double ratio;
}

class NewPriceResponse
{
    public string priceId;
}
