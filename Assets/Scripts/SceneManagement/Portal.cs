using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

namespace RPG.SceneManagement
{
    public class Portal : MonoBehaviour
    {
        #region --Methods-- (Built In)
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                StartCoroutine(Transition());
            }
        }
        #endregion



        #region --Methods-- (Custom PRIVATE)
        private IEnumerator Transition()
        {
            DontDestroyOnLoad(gameObject);

            yield return SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);

            print("Scene Loaded");

            Destroy(gameObject);
        }
        #endregion
    }
}