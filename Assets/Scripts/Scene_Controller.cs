using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene_Controller : MonoBehaviour
{


    public void ChangeScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }
}
