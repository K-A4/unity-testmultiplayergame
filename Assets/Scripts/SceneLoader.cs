using Fusion;
using UnityEngine.SceneManagement;

public class SceneLoader : NetworkBehaviour
{
    public void LoadScene(string SceneName)
    {
        Destroy(Runner.gameObject);
        SceneManager.LoadScene(SceneName);
    }
}
