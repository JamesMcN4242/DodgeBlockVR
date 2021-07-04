////////////////////////////////////////////////////////////
/////   BaseState.cs
/////   James McNeil - 2021
////////////////////////////////////////////////////////////

using PersonalFramework;
using UnityEngine;

public class BaseState : FlowStateBase
{
    const int k_startingPlayerLives = 3;

    private BlockSystem m_blockSystem = null;
    private PlayerColliderMono m_playerCollider = null;
    private PlayerData m_playerData;

    protected override void StartPresentingState()
    {
        m_playerCollider = Object.FindObjectOfType<PlayerColliderMono>();
        m_blockSystem = new BlockSystem(m_playerCollider.transform);
        m_playerData = new PlayerData() { Lives = k_startingPlayerLives, Score = 0 };
    }

    protected override void UpdateActiveState()
    {
        float dt = Time.deltaTime;
        m_blockSystem.Update(dt);

        //TODO: Remove if check since we'll be in a different state if are out of lives
        if(m_playerData.Lives > 0)
        {
            //TODO: Add a score manager that adds set amount over X seconds instead of per frame
            ++m_playerData.Score;
        }
    }

    protected override void FixedUpdateActiveState()
    {
        if(m_playerCollider.HasCollisionsToProcess)
        {
            var colliderObjs = m_playerCollider.ConsumeCollisions();
            foreach(GameObject go in colliderObjs)
            {
                --m_playerData.Lives;
            }

            if(m_playerData.Lives <= 0)
            {
                //TODO: Game over state.
                Debug.Log($"Game Over. You scored: {m_playerData.Score}");
            }
        }
    }
}
