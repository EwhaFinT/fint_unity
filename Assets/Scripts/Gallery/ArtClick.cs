using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArtClick : MonoBehaviour
{
    GameObject[] popupCanvas;
    public GameObject ui;
    public GameObject art, target;
    public GameObject art3d, artAr;
    void Start()
    {
        ui = GameObject.Find("UI");
        popupCanvas = new GameObject[ui.transform.childCount];
        for (int i = 3; i < popupCanvas.Length; i++)
        {
            popupCanvas[i] = ui.transform.GetChild(i).gameObject;
        }
    }

    void Update()
    {
        bool notActiving = true;
        // 왼쪽 마우스 버튼을 클릭했을때
        if (Input.GetMouseButtonDown(0))  //0이면 좌클릭, 1이면 우클릭, 2이면 중앙을 클릭
        {
            for (int i = 3; i < popupCanvas.Length; i++)
            {
                if (popupCanvas[i].activeSelf == true)
                    notActiving = false;
            }
            if (notActiving)
            {
                target = GetClickedObject();
                var artPanel = UIManager.Instance.popupArtInfo.GetComponent<ArtPanel>();
                var artpanel3d = UIManager.Instance.popup3dArt.GetComponent<ArtPanel3d>();
                var artpanelAr = UIManager.Instance.popupArArt.GetComponent<ArtPanelAr>();
                if (art.Equals(target))
                {
                    artPanel.panelStart();
                    artPanel.changeImg(gameObject);
                    artPanel.getFrame(gameObject);
                }
                if (art3d.Equals(target))
                {
                    artpanel3d.panelStart();
                }
                if (artAr.Equals(target))
                {
                    artpanelAr.panelStart();
                }
            }

            }

        }
    

    private GameObject GetClickedObject()
        {
            RaycastHit hit;
            GameObject target = null;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); //마우스 포인트 근처 좌표를 만든다. 

            if (true == (Physics.Raycast(ray.origin, ray.direction * 10, out hit)))   //마우스 근처에 오브젝트가 있는지 확인
            {
                //있으면 오브젝트를 저장한다.
                target = hit.collider.gameObject;

            }
            return target;
        }


    
}
