using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArtClick : MonoBehaviour
{
    GameObject[] popupCanvas;
    public GameObject ui;
    public GameObject art, target;
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
        // ���� ���콺 ��ư�� Ŭ��������
        if (Input.GetMouseButtonDown(0))  //0�̸� ��Ŭ��, 1�̸� ��Ŭ��, 2�̸� �߾��� Ŭ��
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
                if (art.Equals(target))
                {
                    artPanel.panelStart();
                    artPanel.changeImg(gameObject);
                    artPanel.changeArtInfo();
                }
            }

        }

    }

    private GameObject GetClickedObject()
    {
        RaycastHit hit;
        GameObject target = null;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); //���콺 ����Ʈ ��ó ��ǥ�� �����. 

        if (true == (Physics.Raycast(ray.origin, ray.direction * 10, out hit)))   //���콺 ��ó�� ������Ʈ�� �ִ��� Ȯ��
        {
            //������ ������Ʈ�� �����Ѵ�.
            target = hit.collider.gameObject;

        }
        return target;
    }


}
