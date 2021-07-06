////////////////////////////////////////////////////////////
/////   PauseState.cs
/////   James McNeil - 2021
////////////////////////////////////////////////////////////

using PersonalFramework;

public class PauseState : FlowStateBase
{
    private InputManager m_inputManager = null;

    public PauseState(InputManager inputManager)
    {
        m_inputManager = inputManager;

        //TODO: Aquire and show VR friendly UI
    }

    protected override void UpdateActiveState()
    {
        m_inputManager.UpdateInput();
        if (m_inputManager.LeftControllerData.PrimaryTriggered)
        {
            ControllingStateStack.PopState(this);
        }
    }
}
