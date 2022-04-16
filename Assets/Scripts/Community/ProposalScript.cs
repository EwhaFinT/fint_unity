using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProposalScript : MonoBehaviour
{
    public GameObject proposal;
    public Button exit;
    // Start is called before the first frame update
    void Start()
    {
        exit.onClick.AddListener(onClicked_exit);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void show()
    {
        proposal.SetActive(true);
    }
    void onClicked_exit()
    {
        proposal.SetActive(false);
    }
}
