using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SpawnItems : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    ColorItemGenerator CIG;

    bool areClicked = false;

    void Start()
    {
        CIG = FindObjectOfType<ColorItemGenerator>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            CIG.SpawnPressed();
        }


        if (areClicked)
        {
            CIG.SpawnPressed();
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        areClicked = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        areClicked = false;
    }
}
