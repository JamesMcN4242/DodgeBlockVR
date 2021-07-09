////////////////////////////////////////////////////////////
/////   GameState.cs
/////   James McNeil - 2021
////////////////////////////////////////////////////////////

using System;
using PersonalFramework;
using UnityEngine;

public class GameState : FlowStateBase
{
    const int k_startingPlayerLives = 3;

    private ScoreSystem m_scoreSystem = null;
    private BlockSystem m_blockSystem = null;
    private InputManager m_inputManager = null;

    private PlayerColliderMono m_playerCollider = null;
    private PlayerControllerColliderMono[] m_controllerColliders = null;

    private InputRumbleSettings m_rumbleSettings;
    private PlayerData m_playerData;

    public GameState(InputManager inputManager, PlayerColliderMono playerCollider)
    {
        m_inputManager = inputManager;
        m_rumbleSettings = Resources.Load<InputRumbleSettings>("Data/InputRumbleSettings");

        m_playerCollider = playerCollider;
        m_scoreSystem = new ScoreSystem();
    }

    protected override void StartPresentingState()
    {
        m_blockSystem = new BlockSystem(m_playerCollider.transform);
        m_playerData = new PlayerData() { Lives = k_startingPlayerLives };

        Transform parentTransform = m_playerCollider.transform.parent;
        m_controllerColliders = new PlayerControllerColliderMono[2]
        {
            parentTransform.Find("LeftHand/Model").GetComponent<PlayerControllerColliderMono>(),
            parentTransform.Find("RightHand/Model").GetComponent<PlayerControllerColliderMono>()
        };
    }

    protected override void UpdateActiveState()
    {
        m_inputManager.UpdateInput();

        if(m_inputManager.LeftControllerData.PrimaryTriggered)
        {
            ControllingStateStack.PushState(new PauseState(m_inputManager, m_playerCollider.transform, Array.ConvertAll(m_controllerColliders, controller => controller.transform)));
            return;
        }
    }

    protected override void FixedUpdateActiveState()
    {
        float dt = Time.fixedDeltaTime;

        m_scoreSystem.Update(dt);
        m_blockSystem.Update(dt);
        Physics.Simulate(dt);

        HandlePlayerCollisions();
    }

    protected override void StartDismissingState()
    {
        m_blockSystem.DestroyAllBlocks();
    }

    private void HandlePlayerCollisions()
    {
        HandleControllerCollisions();
        HandleHeadCollisions();
    }

    private void HandleHeadCollisions()
    {
        if (!m_playerCollider.HasCollisionsToProcess) return;

        var colliderObjs = m_playerCollider.ConsumeCollisions();
        foreach (GameObject go in colliderObjs)
        {
            m_blockSystem.DestroyBlock(go.transform);
            --m_playerData.Lives;
        }

        if (m_playerData.Lives <= 0)
        {
            Debug.Log($"Player achieved a score of {m_scoreSystem.CurrentScore}");

            //TODO: Change to game over state.
            ControllingStateStack.ChangeState(new GameOverState(m_inputManager, m_playerCollider.transform, m_scoreSystem.CurrentScore));
        }
    }

    private void HandleControllerCollisions()
    {
        for(int i = 0; i < (int)InputManager.ControllerType.COUNT; ++i)
        {
            if (!m_controllerColliders[i].HasCollisionsToProcess) continue;

            var blockTransforms = m_controllerColliders[i].ConsumeCollisions();
            foreach(Transform blockTrans in blockTransforms)
            {
                m_scoreSystem.AddScore(ScoreSystem.ScoreType.BLOCK_HIT);
                m_inputManager.SendRumbleToController((InputManager.ControllerType)i, m_rumbleSettings.m_amplitude, m_rumbleSettings.m_duration);
                m_blockSystem.DestroyBlock(blockTrans);
            }
        }
    }
}
