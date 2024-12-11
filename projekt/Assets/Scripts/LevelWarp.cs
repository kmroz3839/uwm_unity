using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelWarp : MonoBehaviour
{

    public string sceneName;
    public string levelName;
    public int triggerCompletion = -1;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.transform.TryGetComponent(out FPPMvmtGameMovement player))
        {
            player.levelTransitionAnimator.Play("animLevelTransition", -1, 0);
            player.levelTransitionText.text = levelName;
            switch (triggerCompletion) {
                case 0:
                    CrossScene.finishedArea0 = true;
                    break;
                case 1:
                    CrossScene.finishedArea1 = true;
                    break;
            }
            StartCoroutine(Wait1SAndLoadScene());
        }
    }

    IEnumerator Wait1SAndLoadScene()
    {
        yield return new WaitForSeconds(2);
        CrossScene.levelName = levelName;
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }

}
