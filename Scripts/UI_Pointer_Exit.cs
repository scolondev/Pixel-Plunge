using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_Pointer_Exit : MonoBehaviour, IPointerExitHandler
{
    public GameObject description;

    public void OnPointerExit(PointerEventData eventData)
    {
        //do stuff
        description.SetActive(false);
    }
}
