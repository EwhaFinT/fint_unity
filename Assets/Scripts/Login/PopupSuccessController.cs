using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class PopupSuccessController : MonoBehaviour
{
    public GameObject popupSuccess;

    // Start is called before the first frame update
    void Start()
    {
        popupSuccess.transform.GetChild(0).GetComponent<Button>().onClick.AddListener(OffWarn);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MakePopupMessage(string msg)
    {
        TMP_Text message = popupSuccess.transform.GetChild(0).GetChild(0).GetComponent<TMP_Text>();
        message.text = msg;
        popupSuccess.SetActive(true);
    }

    public void MakeSuccessMessage()
    {
        TMP_Text message = popupSuccess.transform.GetChild(0).GetChild(0).GetComponent<TMP_Text>();
        message.text = "Success!";
    }

    public void OffWarn()
    {
        popupSuccess.SetActive(false);
    }
}
