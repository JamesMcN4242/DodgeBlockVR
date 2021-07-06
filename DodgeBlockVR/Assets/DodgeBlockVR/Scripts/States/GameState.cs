////////////////////////////////////////////////////////////
/////   GameState.cs
/////   James McNeil - 2021
////////////////////////////////////////////////////////////

using PersonalFramework;
using UnityEngine;

public class GameState : FlowStateBase
{
    const int k_startingPlayerLives = 3;

    private BlockSystem m_blockSystem = null;
    private InputManager m_inputManager = null;

    private PlayerColliderMono m_playerCollider = null;
    private PlayerControllerColliderMono[] m_controllerColliders = null;

    private InputRumbleSettings m_rumbleSettings;
    private PlayerData m_playerData;

    public GameState(InputManager inputManager)
    {
        m_inputManager = inputManager;
        m_rumbleSettings = Resources.Load<InputRumbleSettings>("Data/InputRumbleSettings");
    }

    protected override void StartPresentingState()
    {
        m_playerCollider = Object.FindObjectOfType<PlayerColliderMono>();
        m_blockSystem = new BlockSystem(m_playerCollider.transform);
        m_playerData = new PlayerData() { Lives = k_startingPlayerLives, Score = 0 };

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
            ControllingStateStack.PushState(new PauseState(m_inputManager));
            return;
        }

        //TODO: Add a score manager that adds set amount over X seconds instead of per frame        
        ++m_playerData.Score;
    }

    protected override void FixedUpdateActiveState()
    {
        float dt = Time.fixedDeltaTime;

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
            --m_playerData.Lives;
        }

        if (m_playerData.Lives <= 0)
        {
            //TODO: Game over state.
            ControllingStateStack.PopState(this);
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
                //TODO: Score addition and block break apart
                m_inputManager.SendRumbleToController((InputManager.ControllerType)i, m_rumbleSettings.m_amplitude, m_rumbleSettings.m_duration);
                m_blockSystem.DestroyBlock(blockTrans);
            }
        }
    }
}
