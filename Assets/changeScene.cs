using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

public class ChangeScene : MonoBehaviour
{
    // Offentligt felt til scenens index i build settings
    public int sceneIndex;

    // Metode til at ændre scenen
    public void LoadScene()
    {
        if (sceneIndex >= 0 && sceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(sceneIndex);
        }
        else
        {
            Debug.LogWarning("Invalid scene index!");
        }
    }

    // Metode til at håndtere touch input
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Ended)
            {
                Vector2 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
                RaycastHit2D hit = Physics2D.Raycast(touchPosition, Vector2.zero);

                if (hit.collider != null && hit.collider.gameObject == gameObject)
                {
                    LoadScene();
                }
            }
        }
    }
}

// Editor script indeni samme fil
#if UNITY_EDITOR
[CustomEditor(typeof(ChangeScene))]
public class ChangeSceneEditor : Editor
{
    public override void OnInspectorGUI()
    {
        ChangeScene changeScene = (ChangeScene)target;

        // Hent alle scener i Build Settings
        string[] scenes = new string[SceneManager.sceneCountInBuildSettings];
        for (int i = 0; i < scenes.Length; i++)
        {
            scenes[i] = System.IO.Path.GetFileNameWithoutExtension(SceneUtility.GetScenePathByBuildIndex(i));
        }

        // Vis en dropdown for at vælge scene
        changeScene.sceneIndex = EditorGUILayout.Popup("Scene", changeScene.sceneIndex, scenes);

        // Standard Inspector UI
        DrawDefaultInspector();
    }
}
#endif
