using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class NewAuction3d : MonoBehaviour
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
    }

    public void panelStart()
    {
        InitializeForm();
        gameObject.SetActive(true);
    }

    void InitializeForm()   //Initialization
    {
        inputPrice.text = "";
        inputShare.text = "";
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
