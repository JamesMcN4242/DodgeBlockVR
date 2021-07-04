////////////////////////////////////////////////////////////
/////   BaseState.cs
/////   James McNeil - 2021
////////////////////////////////////////////////////////////

using PersonalFramework;
using UnityEngine;

public class BaseState : FlowStateBase
{
    private BlockSystem m_blockSystem = null;

    protected override void StartPresentingState()
    {
        m_blockSystem = new BlockSystem();
    }

    protected override void UpdateActiveState()
    {
        float dt = Time.deltaTime;
        m_blockSystem.Update(dt);
    }
}
