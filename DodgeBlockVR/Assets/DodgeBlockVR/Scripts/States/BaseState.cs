////////////////////////////////////////////////////////////
/////   BaseState.cs
/////   James McNeil - 2021
////////////////////////////////////////////////////////////

using PersonalFramework;
using UnityEngine;

public class BaseState : FlowStateBase
{
    private InputManager m_inputManager = null;

    protected override void StartPresentingState()
    {
        m_inputManager = Object.FindObjectOfType<InputManager>();
    }

    protected override void UpdateActiveState()
    {
        if (m_inputManager.LeftControllerData.TriggerValue > 0.0f)
        {
            ControllingStateStack.PushState(new GameState());
        }
    }
}
