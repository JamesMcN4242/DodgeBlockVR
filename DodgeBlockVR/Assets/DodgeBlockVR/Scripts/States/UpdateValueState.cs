////////////////////////////////////////////////////////////
/////   UpdateValueState.cs
/////   James McNeil - 2021
////////////////////////////////////////////////////////////

using PersonalFramework;
using System;
using UnityEngine;

public class UpdateValueState : FlowStateBase
{
    private const float k_uiOffsetDistance = 2.5f;
    private const float k_timeBeforeChange = 0.15f;
    private readonly int k_minimumVal;
    private readonly int k_maximumVal;

    private Action<int> m_updatedValueAction = null;
    private InputManager m_inputManager = null;
    private UpdateValueUI m_valueUI = null;
    private Transform m_playerTransform = null;

    private int m_currentVal;
    private float m_leftGripHoldTime = 0.0f;
    private float m_rightGripHoldTime = 0.0f;

    public UpdateValueState(int currentVal, int minVal, int maxVal, Action<int> onValChange, InputManager inputManager, Transform playerTransform)
    {
        m_currentVal = currentVal;
        k_minimumVal = minVal;
        k_maximumVal = maxVal;

        m_updatedValueAction = onValChange;
        m_updatedValueAction += SetFillAmount;

        m_inputManager = inputManager;
        m_playerTransform = playerTransform;

        GameObject prefab = Resources.Load<GameObject>("Prefabs/UI/UpdateValueUI");
        Vector3 spawnPosition = m_playerTransform.position + (m_playerTransform.forward * k_uiOffsetDistance);
        m_valueUI = GameObject.Instantiate(prefab, spawnPosition, Quaternion.identity).GetComponent<UpdateValueUI>();

        Vector3 facePoint = 2f * spawnPosition - playerTransform.position;
        m_valueUI.transform.forward = (facePoint - spawnPosition).normalized;
        SetFillAmount(currentVal);
    }

    public void SetPopupText(string text)
    {
        m_valueUI.SetStaticValues(text, k_minimumVal, k_maximumVal);
    }

    protected override void UpdateActiveState()
    {
        m_inputManager.UpdateInput();
        if(m_inputManager.LeftControllerData.PrimaryTriggered)
        {
            ControllingStateStack.PopState(this);
            return;
        }

        bool leftGripActive = m_inputManager.LeftControllerData.GripValue > 0.0f;
        bool rightGripActive = m_inputManager.RightControllerData.GripValue > 0.0f;
        UpdateTimesHeld(leftGripActive, rightGripActive);
        if(leftGripActive && rightGripActive) return;

        if(leftGripActive && m_leftGripHoldTime >= k_timeBeforeChange && m_currentVal > k_minimumVal)
        {
            m_leftGripHoldTime = 0.0f;
            m_updatedValueAction(--m_currentVal);
        }
        else if(rightGripActive && m_rightGripHoldTime >= k_timeBeforeChange && m_currentVal < k_maximumVal)
        {
            m_rightGripHoldTime = 0.0f;
            m_updatedValueAction(++m_currentVal);
        }
    }

    protected override void FixedUpdateActiveState()
    {
        VRFriendlyUIUtil.UpdateUIPosition(m_valueUI.transform, m_playerTransform, k_uiOffsetDistance, Time.fixedDeltaTime);
    }

    protected override void StartDismissingState()
    {
        GameObject.Destroy(m_valueUI.gameObject);
    }

    private void UpdateTimesHeld(bool leftGripActive, bool rightGripActive)
    {
        float dt = Time.deltaTime;

        if(leftGripActive)
        {
            m_leftGripHoldTime += dt;
        }
        else
        {
            m_leftGripHoldTime = 0.0f;
        }    

        if(rightGripActive)
        {
            m_rightGripHoldTime += dt;
        }
        else
        {
            m_rightGripHoldTime = 0.0f;
        }
    }

    private void SetFillAmount(int newValue)
    {
        m_valueUI.SetCurrentValue(newValue, k_minimumVal, k_maximumVal);
    }
}
