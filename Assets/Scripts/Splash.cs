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
        temp.allowSceneActivation = false;
        float time = 0;
        while (time<1.5f)
        {
            var progress = time / 1.5f; 
            var size = bar.sizeDelta;
            size.x = progress * 480;
            bar.sizeDelta = size;
            time += Time.deltaTime;
            yield return null;
        }
        temp.allowSceneActivation = true;
    }
}
