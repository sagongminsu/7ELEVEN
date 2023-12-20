using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameExitButton : MonoBehaviour
{
    public void OnClick()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
