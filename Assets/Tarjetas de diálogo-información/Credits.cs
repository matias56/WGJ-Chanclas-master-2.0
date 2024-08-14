using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Credits : MonoBehaviour
{
    public GameObject i1;
    public GameObject i2;
    public GameObject credit;
    // Start is called before the first frame update
    void Start()
    {
        i1.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) && i1.activeSelf)
        {
            i1.SetActive(false);
            i2.SetActive(true);

        }
        else if (Input.GetKeyDown(KeyCode.Return) && i2.activeSelf)
        {

            i2.SetActive(false);
            credit.SetActive(true);
        }



    }
}
