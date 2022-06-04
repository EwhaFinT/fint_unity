using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoardImgScript : MonoBehaviour
{
    public GameObject boardImg;
    public GameObject target;
    GameObject[] popupCanvas;
    public GameObject ui;

    // Start is called before the first frame update
    void Start()
    {
        ui = GameObject.Find("UI");
        popupCanvas = new GameObject[ui.transform.childCount];
        for (int i = 3; i < popupCanvas.Length; i++)
        {
            popupCanvas[i] = ui.transform.GetChild(i).gameObject;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //MakeBoard();
        bool notActiving = true;
        // ???? ?????? ?????? ??????????
        if (Input.GetMouseButtonDown(0))  //0???? ??????, 1???? ??????, 2???? ?????? ????
        {
            for (int i = 3; i < popupCanvas.Length; i++)
            {
                if (popupCanvas[i].activeSelf == true)
                    notActiving = false;
            }
            if (notActiving)
            {
                target = GetClickedObject();
                var boardPanel = UIManager.Instance.popupBoard.GetComponent<BoardScript>();
                if (boardImg.Equals(target))
                {
                    boardPanel.show();
                    //string articleId = "62984aff7a30725036d64c4b";
                    //Debug.Log("------------board image article id-----------");
                    //boardPanel.changeArticleId(articleId);
                }
            }

        }

    }

    //private void MakeBoard()
    //{
    //    var boardPanel = UIManager.Instance.popupBoard.GetComponent<BoardScript>();


    //    // ???? ?????? ?????? ??????????
    //    if (Input.GetMouseButtonDown(0))  //0???? ??????, 1???? ??????, 2???? ?????? ????
    //    {
    //        target = GetClickedObject();
    //        if (boardImg.Equals(target))
    //        {
    //            boardPanel.show();
    //        }
    //    }
    //}

    private GameObject GetClickedObject()
    {
        RaycastHit hit;
        GameObject target = null;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); //?????? ?????? ???? ?????? ??????. 

        if (true == (Physics.Raycast(ray.origin, ray.direction * 10, out hit)))   //?????? ?????? ?????????? ?????? ????
        {
            //?????? ?????????? ????????.
            target = hit.collider.gameObject;

        }
        return target;
    }
}