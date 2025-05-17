using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class HandController : MonoBehaviour
{
    public InputActionReference triggerButton;
    public bool isTrigger;
    public Text textUI;

    // Start is called before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        triggerButton.action.started += OnTriggerPressed;
        triggerButton.action.canceled += OnTriggerReleased;

        isTrigger = false;
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnTriggerPressed(InputAction.CallbackContext context)
    {
        isTrigger = true;
    }

    void OnTriggerReleased(InputAction.CallbackContext context)
    {
        isTrigger = false;
    }

    public void SetText(string text)
    {
        textUI.text = text;
    }
}
