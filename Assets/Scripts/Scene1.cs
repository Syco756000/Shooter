using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
// Allows the use of the Scene Management system and functions
using UnityEngine.SceneManagement;
// This allows the either or statement in the Exit() class
#if UNITY_EDITOR
using UnityEditor;
#endif

public class Scene1 : MonoBehaviour
{
 public static Scene1 scene1;
    public TMP_InputField inputField;

    public string player_name;

    private void Awake()
    {
        if (scene1 == null)
        {
            scene1 = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void StartNew()
    {
        player_name = inputField.text;
        //SceneManager handles everything related to loading and unloading scenes.
        SceneManager.LoadScene(1);
    }

    // The code below allows and either or situation.  This will allow re-entering the Unity Editor if that's where the game was loaded from, to allow more editing.  If not loaded from Unity Editor, it will just close the application.
    public void Exit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}
