using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    public Transform target;
    public void Update()
    {
        if(target != null)
        {
            transform.position = target.position;
        }
    }
}
