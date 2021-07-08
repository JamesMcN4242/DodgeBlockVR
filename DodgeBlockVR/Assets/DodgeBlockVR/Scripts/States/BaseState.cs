////////////////////////////////////////////////////////////
/////   BaseState.cs
/////   James McNeil - 2021
////////////////////////////////////////////////////////////

using PersonalFramework;
using UnityEngine;

public class BaseState : FlowStateBase
{
    private const float k_uiOffsetDistance = 3.5f;
    private const float k_timeToProgress = 3.0f;

    private InputManager m_inputManager = null;
    private PlayerColliderMono m_playerCollider = null;
    private Transform m_playerTransform = null;

    private BaseStateUI m_baseUI = null;
    private float m_timeHolding = 0.0f;

    protected override void StartPresentingState()
    {
        m_inputManager = new InputManager();
        m_playerCollider = Object.FindObjectOfType<PlayerColliderMono>();
        m_playerTransform = m_playerCollider.transform;

        GameObject prefab = Resources.Load<GameObject>("Prefabs/UI/StartMenu");
        Vector3 spawnPosition = m_playerTransform.position + (m_playerTransform.forward * k_uiOffsetDistance);
        m_baseUI = new BaseStateUI(prefab, spawnPosition);
    }

    protected override void UpdateActiveState()
    {
        m_inputManager.UpdateInput();
        if (m_inputManager.LeftControllerData.TriggerValue > 0.0f || m_inputManager.RightControllerData.TriggerValue > 0.0f)
        {
            m_timeHolding += Time.deltaTime;
            m_baseUI.SetTimerFill(m_timeHolding, k_timeToProgress);

            if (m_timeHolding >= k_timeToProgress)
            {
                ControllingStateStack.PushState(new GameState(m_inputManager, m_playerCollider));
            }
        }
        else if (m_timeHolding != 0.0f)
        {
            m_timeHolding = 0.0f;
            m_baseUI.SetTimerFill(m_timeHolding, k_timeToProgress);
        }
    }

    protected override void FixedUpdateActiveState()
    {
        m_baseUI.UpdatePositionAndRotation(m_playerTransform, k_uiOffsetDistance, Time.fixedDeltaTime);
    }

    public override void OnBackgrounded()
    {
        m_baseUI.Show(false);
    }

    public override void OnReturnToForeground()
    {
        m_baseUI.Show(true);
    }
}
