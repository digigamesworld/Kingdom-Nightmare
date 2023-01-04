using System;
using UnityEngine.EventSystems;
using UnityEngine;


public class Deselect : MonoBehaviour
{
    public static event Action DeselectAll;
   private EventSystem _event;
    private void Awake()
    {
        _event = FindObjectOfType<EventSystem>();
    }


    private void Update()
    {
        if (_event.currentSelectedGameObject == null && Input.GetMouseButton(0)) DeselectAll?.Invoke();
    }
 
}
