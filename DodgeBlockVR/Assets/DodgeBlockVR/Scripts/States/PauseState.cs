////////////////////////////////////////////////////////////
/////   PauseState.cs
/////   James McNeil - 2021
////////////////////////////////////////////////////////////

using PersonalFramework;
using UnityEngine;
using UnityEngine.UI;

public class PauseState : FlowStateBase
{
    private const float k_uiOffsetDistance = 3f;
    private static readonly int k_uiLayerMask = 1 << LayerMask.NameToLayer("UI");

    private InputManager m_inputManager = null;

    private Transform[] m_controllerTransforms = null;
    private Transform m_playerTransform = null;
    private Transform m_pauseUI = null;

    private BlockSystem m_blockSystem = null;
    private LineRenderer m_lineRenderObject = null;
    private InputManager.ControllerType m_focusedController = InputManager.ControllerType.RIGHT;
    private Image m_currentRayTarget = null;

    public PauseState(InputManager inputManager, Transform playerTransform, Transform[] controllerTransforms, BlockSystem blockSystem)
    {
        m_inputManager = inputManager;
        m_playerTransform = playerTransform;
        m_controllerTransforms = controllerTransforms;
        m_blockSystem = blockSystem;

        GameObject prefab = Resources.Load<GameObject>("Prefabs/UI/PauseMenu");
        Vector3 spawnPosition = m_playerTransform.position + (m_playerTransform.forward * k_uiOffsetDistance);
        m_pauseUI = GameObject.Instantiate(prefab, spawnPosition, Quaternion.identity).transform;

        Vector3 facePoint = 2f * spawnPosition - playerTransform.position;
        m_pauseUI.forward = (facePoint - spawnPosition).normalized;

        m_lineRenderObject = GameObject.Find("LinePointer").GetComponent<LineRenderer>();
        m_lineRenderObject.enabled = true;
        m_lineRenderObject.startWidth = 0.02f;
        m_lineRenderObject.endWidth = 0.0f;
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

        if(m_inputManager.GetControllerData(m_focusedController).TriggerValue >= 0.9f && m_currentRayTarget != null)
        {         
            UpdateValueState newState = new UpdateValueState(34, 1, 50, null, m_inputManager, m_playerTransform);
            //newState.SetPopupText();
            ControllingStateStack.PushState(newState);
            return;
        }

        UpdateDominantController();
    }

    protected override void FixedUpdateActiveState()
    {
        UpdateLinePosition();
        UpdateSelectedButton();
    }

    protected override void StartDismissingState()
    {
        Object.Destroy(m_pauseUI.gameObject);
        m_lineRenderObject.enabled = false;
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
                m_inputManager.SendRumbleToController(m_focusedController, 0.1f, 0.1f);
            }
        }
        else if(m_currentRayTarget != null)
        {
            m_currentRayTarget.color = Color.white;
            m_currentRayTarget = null;
        }
    }

    private void UpdateDominantController()
    {
        if (m_inputManager.LeftControllerData.TriggerValue > m_inputManager.RightControllerData.TriggerValue)
        {
            m_focusedController = InputManager.ControllerType.LEFT;
        }
        else if (m_inputManager.RightControllerData.TriggerValue > m_inputManager.LeftControllerData.TriggerValue)
        {
            m_focusedController = InputManager.ControllerType.RIGHT;
        }
    }
}
