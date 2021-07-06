////////////////////////////////////////////////////////////
/////   BlockSystem.cs
/////   James McNeil - 2021
////////////////////////////////////////////////////////////

using System.Collections.Generic;
using UnityEngine;

public class BlockSystem
{
    private List<Block> m_blocks = null;
    private BlockSpawnerMono[] m_spawnerObjs = null;
    private BlockData m_blockData = null;
    private Transform m_targetTransform = null;

    private float m_timeSinceBlockCreation = 0.0f;

    public BlockSystem(Transform targetTransform)
    {
        const int startingCapacity = 20;
        m_blocks = new List<Block>(startingCapacity);

        m_spawnerObjs = Object.FindObjectsOfType<BlockSpawnerMono>();
        Debug.Assert(m_spawnerObjs.Length > 0, "No spawner objects were found in the scene");

        m_blockData = Resources.Load<BlockData>("Data/BlockData");

        m_targetTransform = targetTransform;
        CreateNewBlock();
    }

    public void Update(float dt)
    {
        m_timeSinceBlockCreation += dt;
        UpdateBlocks(dt);
        DeleteFinishedBlocks();
        TryCreateMoreBlocks();
    }

    public void DestroyBlock(Transform block)
    {
        for(int i = 0; i < m_blocks.Count; ++i)
        {
            if(block == m_blocks[i].BlockTransform)
            {
                Object.Destroy(m_blocks[i].BlockTransform.gameObject);
                m_blocks.RemoveAt(i);
                return;
            }
        }
    }

    public void DestroyAllBlocks()
    {
        foreach(Block block in m_blocks)
        {
            Object.Destroy(block.BlockTransform.gameObject);
        }
        m_blocks.Clear();
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
            CreateNewBlock();
        }
    }

    private void CreateNewBlock()
    {
        var spawner = m_spawnerObjs[Random.Range(0, m_spawnerObjs.Length)];
        Vector3 startPosition = spawner.transform.position;
        Vector3 endPosition = startPosition + (2f * (m_targetTransform.position - startPosition));
        m_blocks.Add(BlockFactory.CreateBlock(startPosition, endPosition, m_blockData.GetNextBlockSpeed));

        m_timeSinceBlockCreation = 0.0f;
    }

    private bool ShouldCreateNewBlock()
    {
        return m_timeSinceBlockCreation > m_blockData.m_minimumTimeBeforeNextSpawn && m_blocks.Count < m_blockData.m_maximumBlocks;
    }
}
