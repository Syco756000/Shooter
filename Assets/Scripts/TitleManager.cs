using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//allows the use of the Scene Management system and functions
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif


public class TitleManager : MonoBehaviour
{
    // Allow for your Initials to be input into the text box
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void StartNew()
    {
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
