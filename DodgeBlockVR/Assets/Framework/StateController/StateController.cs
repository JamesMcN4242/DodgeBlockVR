////////////////////////////////////////////////////////////
/////   StateController.cs
/////   James McNeil - 2020
////////////////////////////////////////////////////////////

using System.Collections.Generic;
using UnityEngine;

namespace PersonalFramework
{
    public class StateController
    {
        private Stack<FlowStateBase> m_stateStack = new Stack<FlowStateBase>();
        private FlowStateBase m_stateToChangeTo = null;

        public void PushState(FlowStateBase state)
        {
            Debug.Assert(m_stateStack.Count == 0 || m_stateStack.Peek() != state, "Trying to push already active state");

            if (m_stateStack.Count > 0 && state != m_stateToChangeTo)
            {
                m_stateStack.Peek().OnBackgrounded();
            }
            m_stateStack.Push(state);
            state.SetStateController(this);
        }

        public void PopState(FlowStateBase state)
        {
            Debug.Assert(m_stateStack.Count > 0 && m_stateStack.Peek() == state, "Trying to pop non active state");
            m_stateStack.Peek().EndActiveState();
        }

        public void ChangeState(FlowStateBase state)
        {
            Debug.Assert(m_stateStack.Count > 0, "Trying to change state when not currently in one");
            m_stateToChangeTo = state;
            m_stateStack.Peek().EndActiveState();
        }

        public void UpdateStack()
        {
            if (m_stateStack.Count > 0)
            {
                FlowStateBase state = m_stateStack.Peek();
                state.UpdateState();
                if (state.IsDismissed())
                {
                    m_stateStack.Pop(); 
                    if(m_stateToChangeTo != null)
                    {
                        PushState(m_stateToChangeTo);
                        m_stateToChangeTo = null;
                    }
                    else if (m_stateStack.Count > 0)
                    {
                        m_stateStack.Peek().OnReturnToForeground();
                    }
                }
            }
        }

        public void FixedUpdateStack()
        {
            if (m_stateStack.Count > 0)
            {
                FlowStateBase state = m_stateStack.Peek();
                state.FixedUpdateState();
            }
        }
    }
}