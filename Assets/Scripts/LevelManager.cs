using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Image = UnityEngine.UIElements.Image;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private GameObject _loadScene;
    [SerializeField] private string _nextLevelName;

    public void LoadLevel(string levelName)
    {
        StartCoroutine(LoadSceneAsync(levelName));
    }

    public void LoadLevelByName()
    {
        StartCoroutine(LoadSceneAsync(_nextLevelName));
    }

    IEnumerator LoadSceneAsync(string levelName)
    {
        AsyncOperation op = SceneManager.LoadSceneAsync(levelName);
        _loadScene.SetActive(true);

        while (!op.isDone)
        {
            _slider.value = op.progress;

            yield return null;
        }

        _loadScene.SetActive(false);
    }
}