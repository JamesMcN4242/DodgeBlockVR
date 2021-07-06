////////////////////////////////////////////////////////////
/////   InputRumbleSettings.cs
/////   James McNeil - 2021
////////////////////////////////////////////////////////////

using UnityEngine;

[CreateAssetMenu(fileName = "InputRumbleSettings", menuName = "Data/InputRumbleSettings")]
public class InputRumbleSettings : ScriptableObject
{
    [Range(0.0f, 1.0f)]public float m_amplitude;
    [Range(0.0f, 3.0f)]public float m_duration;
}
