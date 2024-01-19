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
    public static UiEvent instance;

    private void Awake()
    {
        instance = this;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (eventData.pointerCurrentRaycast.gameObject)
        {
            data = eventData.pointerCurrentRaycast.gameObject;
            if(data.GetComponent<Image>())
                data.GetComponent<Image>().color = Color.gray;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (data)
        {
            if (data.GetComponent<Image>())
                data.GetComponent<Image>().color = Color.white;
        }
    }

}
