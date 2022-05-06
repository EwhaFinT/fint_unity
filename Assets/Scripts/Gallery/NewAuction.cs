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
        _artId = getArtId;
        gameObject.SetActive(true);
    }

    void Onclick_NewAuction()
    {
        StartCoroutine(SendNewInfo());
        //auction panel refresh
        var auctionPanel = UIManager.Instance.popupAuction.GetComponent<AuctionPanel>();
        auctionPanel.panelStart(_artId);
    }

    IEnumerator SendNewInfo()
    {
        string url = "https://fintribe.herokuapp.com/v1/price-success";
        // (for 시연 영상 : rlp 빈 리스트)
        NewPriceRequest newPriceRequest = new NewPriceRequest
        {
            userId = Manager.Instance.ID,
            artId = _artId,
            auctionPrice = sumprice,
            ratio = _ratio/100,
            rlp = new List<string>()
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
            // 경매 참여 실패 : 현재 상한가보다 적은 금액 입력한 경우
        }
        else
        {
            // 경매 참여 성공
        }

        // api 통신 부분 끝

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
        pricetopay.text = "예상 지불 금액 : " + tmp + " KLAY";
    }

}
class NewPriceRequest
{
    public string userId;
    public string artId;
    public double auctionPrice;
    public double ratio;
    public List<string> rlp;
}

class NewPriceResponse
{
    public string priceId;
}
