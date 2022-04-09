using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PopupWarnController : MonoBehaviour
{
    public GameObject popupWarn;

    // Start is called before the first frame update
    void Start()
    {
        popupWarn.transform.GetChild(0).GetComponent<Button>().onClick.AddListener(OffWarn);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MakePopupWarn(string msg)  // ???? ?????
    {
        TMP_Text message = popupWarn.transform.GetChild(0).GetChild(0).GetComponent<TMP_Text>();
        message.text = msg;
        popupWarn.SetActive(true);
    }

    public void OffWarn()
    {
        popupWarn.SetActive(false);
    }
}
