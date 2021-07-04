////////////////////////////////////////////////////////////
/////   GameDirector.cs
/////   James McNeil - 2021
////////////////////////////////////////////////////////////

using PersonalFramework;
using UnityEngine;

public class GameDirector : LocalDirector
{
    private void Start()
    { 
        m_stateController.PushState(new BaseState());
    }

    [RuntimeInitializeOnLoadMethod]
    private static void CreateDirector()
    {
        GameObject _ = new GameObject("GameDirector", typeof(GameDirector));
    }
}
