using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FistController : FloatingJoystick
{
    public delegate void JoystickDown();
    public event JoystickDown OnJoystickDown;

    public delegate void JoystickUp();
    public event JoystickUp OnJoystickUp;

    public Color DefaultColour { get => new Color(0.94f, 0.92f, 0.92f, 0.75f) ; }
    public Color GrabbedColour { get => new Color(0.914f, 0.847f, 0.274f, 0.9f); }

    protected override void Start()
    {
        base.Start();
        ChangeColor(DefaultColour);
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        base.OnPointerDown(eventData);
        OnJoystickDown?.Invoke();
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        base.OnPointerUp(eventData);
        OnJoystickUp?.Invoke();
    }
}
