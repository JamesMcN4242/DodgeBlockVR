////////////////////////////////////////////////////////////
/////   BlockSystem.cs
/////   James McNeil - 2021
////////////////////////////////////////////////////////////

using System.Collections.Generic;
using UnityEngine;

public class BlockSystem
{
    private const int k_minimumBlockCount = 3;
    private List<Block> m_blocks = null;

    private float m_timeSinceBlockCreation = 0.0f;
    public float TimeBeforeNewBlockSpawn { get; set; }

    public BlockSystem()
    {
        const int startingCapacity = 20;
        m_blocks = new List<Block>(startingCapacity);

        TimeBeforeNewBlockSpawn = 0.75f;
        TryCreateMoreBlocks();
    }

    public void Update(float dt)
    {
        m_timeSinceBlockCreation += dt;
        UpdateBlocks(dt);
        DeleteFinishedBlocks();
        TryCreateMoreBlocks();
    }

    private void UpdateBlocks(float dt)
    {
        for (int i = 0; i < m_blocks.Count; ++i)
        {
            Block block = m_blocks[i];
            block.BlockTransform.position = block.BlockTransform.position + (block.Velocity * dt);
            block.TimeLeftAlive -= dt;
            m_blocks[i] = block;
        }
    }

    private void DeleteFinishedBlocks()
    {
        for(int i = m_blocks.Count - 1; i >= 0; --i)
        {
            if(m_blocks[i].TimeLeftAlive <= 0.0f)
            {
                Object.Destroy(m_blocks[i].BlockTransform.gameObject);
                m_blocks.RemoveAt(i);
            }
        }
    }

    private void TryCreateMoreBlocks()
    {
        if (ShouldCreateNewBlock())
        {
            Vector3 startPosition = Vector3.zero;
            Vector3 endPosition = Vector3.one;
            m_blocks.Add(BlockFactory.CreateBlock(startPosition, endPosition, 5.0f));

            m_timeSinceBlockCreation = 0.0f;
        }
    }

    private bool ShouldCreateNewBlock()
    {
        return m_timeSinceBlockCreation > TimeBeforeNewBlockSpawn || m_blocks.Count < k_minimumBlockCount;
    }
}
