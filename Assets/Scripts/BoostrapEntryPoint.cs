using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BoostrapEntryPoint : MonoBehaviour
{
    private IEnumerator Start()
    {
        var sceneloading = SceneManager.LoadSceneAsync("Lobby");

        while (sceneloading.isDone == false)
        {
            Debug.Log($"LoadingProgress{sceneloading.progress}");
            yield return null;
        }
    }
}
