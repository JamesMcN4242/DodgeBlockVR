////////////////////////////////////////////////////////////
/////   InputManager.cs
/////   James McNeil - 2021
////////////////////////////////////////////////////////////

using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private ControllerInputData[] m_controllerInputs = null;

    private enum ControllerType { LEFT, RIGHT };
    public struct ControllerInputData
    {
        public float TriggerValue;
    }

    public ControllerInputData LeftControllerData => m_controllerInputs[(int)ControllerType.LEFT];

    private void Awake()
    {
        m_controllerInputs = new ControllerInputData[2];
    }

    public void SetTriggerValue(InputAction.CallbackContext triggerData)
    {
        //TODO: Seperate input to allow both triggers setting successfully
        m_controllerInputs[(int)ControllerType.LEFT].TriggerValue = triggerData.ReadValue<float>();
    }
}