using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneLoader 
{
    public enum Scenes
    {
        MainMenu,
        Loading,
        GameScene
    }
    private static Scenes scene;


    public static void OnSceneLoader(Scenes scene){
        SceneLoader.scene = scene;
        SceneManager.LoadScene(Scenes.Loading.ToString());   
        }

    public static void OnLoadingComplete()
    {
        SceneManager.LoadScene(scene.ToString());
    }
}
