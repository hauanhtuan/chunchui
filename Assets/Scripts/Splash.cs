using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Splash : MonoBehaviour
{
    [SerializeField] private RectTransform bar;
    private IEnumerator Start()
    {
        var temp = SceneManager.LoadSceneAsync("Gameplay");
        while(!temp.isDone)
        {
            var progress = temp.progress;
            var size = bar.sizeDelta;
            size.x = progress * 480;
            bar.sizeDelta = size;
            yield return null;
        }
    }
}
