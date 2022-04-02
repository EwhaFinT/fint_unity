using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VotePop : MonoBehaviour
{
    public GameObject voteBtn;  //클릭할 오브젝트
    public GameObject vote;   //떠오르게 할 팝업창
    public GameObject target;
    public GameObject board;

    void Start()
    {
        vote.SetActive(false);
    }

    void Update()
    {
        // 왼쪽 마우스 버튼을 클릭했을때
        if (Input.GetMouseButtonDown(0))  //0이면 좌클릭, 1이면 우클릭, 2이면 중앙을 클릭
        {
            Debug.Log("btn Click");
            vote.SetActive(true);
            board.SetActive(false);
            
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