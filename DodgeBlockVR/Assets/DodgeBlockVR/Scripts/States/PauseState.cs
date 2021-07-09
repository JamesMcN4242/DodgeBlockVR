////////////////////////////////////////////////////////////
/////   PauseState.cs
/////   James McNeil - 2021
////////////////////////////////////////////////////////////

using PersonalFramework;
using UnityEngine;
using UnityEngine.UI;

public class PauseState : FlowStateBase
{
    private const float k_uiOffsetDistance = 2.5f;
    private static readonly int k_uiLayerMask = 1 << LayerMask.NameToLayer("UI");

    private InputManager m_inputManager = null;

    private Transform[] m_controllerTransforms = null;
    private Transform m_playerTransform = null;
    private Transform m_pauseUI = null;

    private LineRenderer m_lineRenderObject = null;
    private InputManager.ControllerType m_focusedController = InputManager.ControllerType.RIGHT;
    private Image m_currentRayTarget = null;

    public PauseState(InputManager inputManager, Transform playerTransform, Transform[] controllerTransforms)
    {
        m_inputManager = inputManager;
        m_playerTransform = playerTransform;
        m_controllerTransforms = controllerTransforms;

        GameObject prefab = Resources.Load<GameObject>("Prefabs/UI/PauseMenu");
        Vector3 spawnPosition = m_playerTransform.position + (m_playerTransform.forward * k_uiOffsetDistance);
        m_pauseUI = GameObject.Instantiate(prefab, spawnPosition, Quaternion.identity).transform;

        Vector3 facePoint = 2f * spawnPosition - playerTransform.position;
        m_pauseUI.forward = (facePoint - spawnPosition).normalized;

        m_lineRenderObject = new GameObject("LineRenderObj").AddComponent<LineRenderer>();
        m_lineRenderObject.startWidth = 0.02f;
        m_lineRenderObject.endWidth = 0.02f;
        m_lineRenderObject.startColor = Color.cyan;
        m_lineRenderObject.endColor = Color.cyan;
        UpdateLinePosition();
    }

    protected override void UpdateActiveState()
    {
        m_inputManager.UpdateInput();
        if (m_inputManager.LeftControllerData.PrimaryTriggered)
        {
            ControllingStateStack.PopState(this);
            return;
        }

        if(m_inputManager.GetControllerData(m_focusedController).TriggerValue >= 0.9f)
        {
            //TODO: Push new state for updating values
            Debug.Log("Attempting to push state");
        }

        if(m_inputManager.LeftControllerData.TriggerValue > m_inputManager.RightControllerData.TriggerValue)
        {
            m_focusedController = InputManager.ControllerType.LEFT;
        }
        else if(m_inputManager.RightControllerData.TriggerValue > m_inputManager.LeftControllerData.TriggerValue)
        {
            m_focusedController = InputManager.ControllerType.RIGHT;
        }
    }

    protected override void FixedUpdateActiveState()
    {
        UpdateLinePosition();
        UpdateSelectedButton();
    }

    protected override void StartDismissingState()
    {
        Object.Destroy(m_pauseUI.gameObject);
        Object.Destroy(m_lineRenderObject.gameObject);
    }

    private void UpdateLinePosition()
    {
        const float lineDistance = 10.0f;
        Vector3 lineStartPosition = m_controllerTransforms[(int)m_focusedController].position;
        m_lineRenderObject.SetPositions(new Vector3[] { lineStartPosition, lineStartPosition + m_controllerTransforms[(int)m_focusedController].forward * lineDistance });
    }

    private void UpdateSelectedButton()
    {
        Ray rayFromController = new Ray(m_controllerTransforms[(int)m_focusedController].position, m_controllerTransforms[(int)m_focusedController].forward);
        if (Physics.Raycast(rayFromController, out RaycastHit hit, float.MaxValue, k_uiLayerMask))
        {
            Image newSelection = hit.collider.GetComponent<Image>();
            if(newSelection != m_currentRayTarget)
            {
                m_currentRayTarget = hit.collider.GetComponent<Image>();
                m_currentRayTarget.color = Color.gray;
                m_inputManager.SendRumbleToController(m_focusedController, 0.2f, 0.2f);
            }
        }
        else if(m_currentRayTarget != null)
        {
            m_currentRayTarget.color = Color.white;
            m_currentRayTarget = null;
        }
    }
}
