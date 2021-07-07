////////////////////////////////////////////////////////////
/////   ScoreSystem.cs
/////   James McNeil - 2021
////////////////////////////////////////////////////////////

using UnityEngine;

public class ScoreSystem
{
    public enum ScoreType { SURVIVAL_TIME, BLOCK_HIT, COUNT }

    private int[] m_scoreValues;
    private ScoreData m_scoreData;
    private float m_survivalPointTimer = 0.0f;

    public int CurrentScore => m_scoreData.m_currentScore;
    private float TimeBeforeSurvivalPoints { set; get; }

    public ScoreSystem(GameType gameType = GameType.Default)
    {
        m_scoreValues = new int[(int)ScoreType.COUNT];
        ChangeGameType(gameType);
    }

    public void Update(float dt)
    {
        m_survivalPointTimer += dt;
        if(m_survivalPointTimer >= TimeBeforeSurvivalPoints)
        {
            m_survivalPointTimer = 0.0f;
            AddScore(ScoreType.SURVIVAL_TIME);
        }
    }

    public void AddScore(ScoreType scoreType)
    {
        m_scoreData.m_currentScore += m_scoreValues[(int)scoreType];
    }

    public void ChangeGameType(GameType gameType)
    {
        var scoreSettings = Resources.Load<GameScoreSettings>($"Data/{gameType}ScoreSettings");
        
        TimeBeforeSurvivalPoints = scoreSettings.m_survivalTimeThreshold;
        m_scoreValues[(int)ScoreType.SURVIVAL_TIME] = scoreSettings.m_survivalTimePoints;
        m_scoreValues[(int)ScoreType.BLOCK_HIT] = scoreSettings.m_blockDestructionPoints;
    }
}
