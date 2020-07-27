using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.Extras;
using Valve.VR.InteractionSystem;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class LaserInteract : MonoBehaviour
{
    public SteamVR_LaserPointer laserPointer;
    public UIElement uiElement;
    public Button button;
    public EventSystem eventSystem;
    public Toggle toggle;

    void Awake()
    {
        eventSystem = FindObjectOfType<EventSystem>();
        laserPointer = FindObjectOfType<SteamVR_LaserPointer>();
        laserPointer.PointerIn += PointerHover;
        laserPointer.PointerOut += PointerNotHovering;
        laserPointer.PointerClick += PointerInteract;
    }

    private void PointerInteract(object sender, PointerEventArgs e)
    {
        if(e.target.tag == "UIButton")
        {
            button = e.target.GetComponent<Button>();
            button.onClick.Invoke();
        }
        else if(e.target.tag == "UIToggle")
        {
            toggle = e.target.GetComponent<Toggle>();
            toggle.isOn = true;
        }

    }

    private void PointerNotHovering(object sender, PointerEventArgs e)
    {
        if (eventSystem.currentSelectedGameObject == this.gameObject)
        {
            eventSystem.SetSelectedGameObject(null);
        }
    }

    public void PointerHover(object sender, PointerEventArgs e)
    {
        if (e.target.tag == "UIButton")
        {
            button = e.target.GetComponent<Button>();
            button.Select();
        }
        if (e.target.tag == "UIToggle")
        {
            toggle = e.target.GetComponent<Toggle>();
            toggle.Select();
        }
    }

    public void TestClick()
    {
        Debug.Log("Button clicked.");
    }
}
