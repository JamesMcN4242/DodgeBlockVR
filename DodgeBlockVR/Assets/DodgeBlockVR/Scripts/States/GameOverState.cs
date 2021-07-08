////////////////////////////////////////////////////////////
/////   GameOverState.cs
/////   James McNeil - 2021
////////////////////////////////////////////////////////////

using PersonalFramework;
using TMPro;
using UnityEngine;

public class GameOverState : FlowStateBase
{
    private const float k_uiOffsetDistance = 2.5f;

    private InputManager m_inputManager = null;
    private Transform m_playerTransform = null;
    private Transform m_gameOverUI = null; 

    public GameOverState(InputManager inputManager, Transform playerTransform, int score)
    {
        m_inputManager = inputManager;
        m_playerTransform = playerTransform;

        //TODO: Create a UI class to manage this stuff if anything needs to be added
        GameObject prefab = Resources.Load<GameObject>("Prefabs/UI/GameOverPopup");
        Vector3 spawnPosition = m_playerTransform.position + (m_playerTransform.forward * k_uiOffsetDistance);
        m_gameOverUI = GameObject.Instantiate(prefab, spawnPosition, Quaternion.identity).transform;
        m_gameOverUI.Find("Background/Score").GetComponent<TextMeshProUGUI>().text = $"You achieved a score of: {score}";

        Vector3 facePoint = 2f * spawnPosition - playerTransform.position;
        m_gameOverUI.forward = (facePoint - spawnPosition).normalized;
    }

    protected override void UpdateActiveState()
    {
        m_inputManager.UpdateInput();
        if (m_inputManager.LeftControllerData.PrimaryTriggered || m_inputManager.RightControllerData.PrimaryTriggered)
        {
            ControllingStateStack.PopState(this);
        }
    }

    protected override void FixedUpdateActiveState()
    {
        VRFriendlyUIUtil.UpdateUIPosition(m_gameOverUI, m_playerTransform, k_uiOffsetDistance, Time.fixedDeltaTime);
    }

    protected override void StartDismissingState()
    {
        Object.Destroy(m_gameOverUI.gameObject);
    }
}
