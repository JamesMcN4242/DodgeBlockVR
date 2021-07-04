////////////////////////////////////////////////////////////
/////   BlockFactory.cs
/////   James McNeil - 2021
////////////////////////////////////////////////////////////

using UnityEngine;

public static class BlockFactory
{
    private const string k_blockPrefab = "Prefabs/Block";

    private static Transform s_blockParent = null;
    private static GameObject s_blockPrefab = null;

    [RuntimeInitializeOnLoadMethod]
    private static void LoadBlockPrefab()
    {
        s_blockPrefab = Resources.Load<GameObject>(k_blockPrefab);
        s_blockParent = new GameObject("Blocks").transform;
    }

    public static Block CreateBlock(Vector3 startPosition, Vector3 endPosition, float speed)
    {
        GameObject block = GameObject.Instantiate(s_blockPrefab, s_blockParent);
        Vector3 velocity = (endPosition - startPosition).normalized * speed;

        return new Block()
        {
            BlockTransform = block.transform,
            Velocity = velocity,
            TimeLeftAlive = (endPosition - startPosition).magnitude / speed
        };
    }
}