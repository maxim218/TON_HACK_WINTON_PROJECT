using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingLevelMainManager : MonoBehaviour {
    private const string NameScene = "FruitCatcher";
    
    private IEnumerator LoadSceneAsync() {
        yield return new WaitForSeconds(BeginDelay);
        AsyncOperation operation = SceneManager.LoadSceneAsync(NameScene);
        while(!operation.isDone) {
            yield return new WaitForSeconds(LoopDelay);
        }
    }

    private void Start() {
        StartCoroutine(LoadSceneAsync());
    }

    private const float LoopDelay = 0.1f;
    private const float BeginDelay = 2.1f;
}
