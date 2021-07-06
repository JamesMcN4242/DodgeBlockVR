////////////////////////////////////////////////////////////
/////   InputManager.cs
/////   James McNeil - 2021
////////////////////////////////////////////////////////////

using UnityEngine;
using UnityEngine.InputSystem;

using static UnityEngine.Debug;

public class InputManager
{
    public enum ControllerType { LEFT, RIGHT, COUNT };
    public struct ControllerInputData
    {
        public float TriggerValue;
    }

    private struct ControllerInputActions
    {
        public InputAction m_grabAction;
    }

    private InputActionMap m_inputActionMap = null;
    private ControllerInputData[] m_controllerInputs = null;
    private ControllerInputActions[] m_controllerActions;

    public ControllerInputData LeftControllerData => m_controllerInputs[(int)ControllerType.LEFT];
    public ControllerInputData RightControllerData => m_controllerInputs[(int)ControllerType.RIGHT];

    public InputManager()
    {
        m_controllerInputs = new ControllerInputData[(int)ControllerType.COUNT];

        InputActionAsset inputAsset = Resources.Load<InputActionAsset>("InputActions/ViveController");
        m_inputActionMap = inputAsset.FindActionMap("Player");
        Assert(m_inputActionMap != null, "Input Action Map was not found");

        m_controllerActions = GetControllerInputActions(m_inputActionMap);
        m_inputActionMap.Enable();
    }

    public void UpdateInput()
    {
        for(int i = 0; i < (int)ControllerType.COUNT; ++i)
        {
            m_controllerInputs[i].TriggerValue = m_controllerActions[i].m_grabAction.ReadValue<float>();
        }
    }

    private static ControllerInputActions[] GetControllerInputActions(InputActionMap actionMap)
    {
        return new ControllerInputActions[]
        {
            GetSpecificControllerInputActions(actionMap, ControllerType.LEFT),
            GetSpecificControllerInputActions(actionMap, ControllerType.RIGHT)
        };
    }

    private static ControllerInputActions GetSpecificControllerInputActions(InputActionMap actionMap, ControllerType type)
    {
        string prefix = type == ControllerType.LEFT ? "Left" : "Right";
        return new ControllerInputActions
        {
            m_grabAction = actionMap.FindAction(prefix + "Grab")
        };
    }
}