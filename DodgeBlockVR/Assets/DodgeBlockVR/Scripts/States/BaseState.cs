////////////////////////////////////////////////////////////
/////   BaseState.cs
/////   James McNeil - 2021
////////////////////////////////////////////////////////////

using PersonalFramework;
using UnityEngine;

public class BaseState : FlowStateBase
{
    private BlockSystem m_blockSystem = null;
    private PlayerColliderMono m_playerCollider = null;

    protected override void StartPresentingState()
    {
        m_playerCollider = Object.FindObjectOfType<PlayerColliderMono>();
        m_blockSystem = new BlockSystem(m_playerCollider.transform);
    }

    protected override void UpdateActiveState()
    {
        float dt = Time.deltaTime;
        m_blockSystem.Update(dt);
    }

    protected override void FixedUpdateActiveState()
    {
        if(m_playerCollider.HasCollisionsToProcess)
        {
            var colliderObjs = m_playerCollider.ConsumeCollisions();
            foreach(GameObject go in colliderObjs)
            {
                Debug.Log("You got hit");
            }
        }
    }
}
