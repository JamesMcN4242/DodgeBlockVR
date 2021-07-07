////////////////////////////////////////////////////////////
/////   GameScoreSettings.cs
/////   James McNeil - 2021
////////////////////////////////////////////////////////////

using UnityEngine;

[CreateAssetMenu(fileName = "ScoreSettings", menuName = "Data/ScoreSettings")]
public class GameScoreSettings : ScriptableObject
{
    public float m_survivalTimeThreshold = 3.0f;
    public int m_survivalTimePoints = 5;
    public int m_blockDestructionPoints = 25;
}
