////////////////////////////////////////////////////////////
/////   PauseState.cs
/////   James McNeil - 2021
////////////////////////////////////////////////////////////

using PersonalFramework;
using UnityEngine;

public class PauseState : FlowStateBase
{
    private const float k_uiOffsetDistance = 2.5f;

    private InputManager m_inputManager = null;
    private Transform m_playerTransform = null;
    private Transform m_pauseUI = null; 

    public PauseState(InputManager inputManager, Transform playerTransform)
    {
        m_inputManager = inputManager;
        m_playerTransform = playerTransform;

        GameObject prefab = Resources.Load<GameObject>("Prefabs/UI/PauseMenu");
        Vector3 spawnPosition = m_playerTransform.position + (m_playerTransform.forward * k_uiOffsetDistance);
        m_pauseUI = GameObject.Instantiate(prefab, spawnPosition, Quaternion.identity).transform;

        Vector3 facePoint = 2f * spawnPosition - playerTransform.position;
        m_pauseUI.forward = (facePoint - spawnPosition).normalized;
    }

    protected override void UpdateActiveState()
    {
        m_inputManager.UpdateInput();
        if (m_inputManager.LeftControllerData.PrimaryTriggered)
        {
            ControllingStateStack.PopState(this);
        }
    }

    protected override void FixedUpdateActiveState()
    {
        VRFriendlyUIUtil.UpdateUIPosition(m_pauseUI, m_playerTransform, k_uiOffsetDistance, Time.fixedDeltaTime);
    }

    protected override void StartDismissingState()
    {
        Object.Destroy(m_pauseUI.gameObject);
    }
}
