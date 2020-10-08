using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundSpark : MonoBehaviour
{
    public GameObject sparkPanel, sparkStart, sparkEnd;
    Vector3 sparkOrgin;

    void Start()
    {
        sparkPanel.transform.position = sparkStart.transform.position;
    }

    void FixedUpdate()
    {
        if (sparkPanel.transform.position.y > sparkEnd.transform.position.y)
        {
            sparkPanel.transform.position += new Vector3(0f,-3f,0f);
        }
        else
        {
            sparkPanel.transform.position = sparkStart.transform.position;
        }
    }
}
