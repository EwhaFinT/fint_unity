using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArtClick : MonoBehaviour
{

    public GameObject art, target;

    void Start()
    {
        
    }

    void Update()
    {

        // ���� ���콺 ��ư�� Ŭ��������
        if (Input.GetMouseButtonDown(0))  //0�̸� ��Ŭ��, 1�̸� ��Ŭ��, 2�̸� �߾��� Ŭ��
        {
            target = GetClickedObject();
            var artPanel = UIManager.Instance.popupArtInfo.GetComponent<ArtPanel>();
            if (target.Equals(art))
            {
                artPanel.panelStart();
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
