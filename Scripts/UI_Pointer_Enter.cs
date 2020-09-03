using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_Pointer_Enter : MonoBehaviour, IPointerEnterHandler
{
    public GameObject description;
    // Start is called before the first frame update
    void Start()
    {
        description.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //do stuff
        description.SetActive(true);
    }
}
