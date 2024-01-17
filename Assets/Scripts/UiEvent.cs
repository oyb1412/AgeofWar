using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UiEvent : MonoBehaviour
    , IPointerEnterHandler
    , IPointerExitHandler
{
    GameObject data;
    public GameObject selectPanel;
    public GameObject unitPanel;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (eventData.pointerCurrentRaycast.gameObject)
        {
            data = eventData.pointerCurrentRaycast.gameObject;
            data.GetComponent<Image>().color = Color.gray;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (data)
        {
            data.GetComponent<Image>().color = Color.white;
        }
    }

}
