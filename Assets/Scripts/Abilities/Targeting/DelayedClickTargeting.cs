using System.Collections;
using UnityEngine;
using RPG.Control;

namespace RPG.Abilities.Targeting
{
    [CreateAssetMenu(fileName = "Untitled Delayed Click Targeting", menuName = "RPG/Game Item/Targeting/New Delayed Click", order = 126)]
    public class DelayedClickTargeting : TargetingStrategy
    {
        #region --Fields-- (Inspector)
        [SerializeField] private Texture2D _cursorTexture;
        [SerializeField] private Vector2 _cursorHotspot;
        #endregion



        #region --Methods-- (Custom PRIVATE)
        private IEnumerator Targeting(GameObject user, PlayerController playerController)
        {
            playerController.enabled = false;

            while (true)
            {
                Cursor.SetCursor(_cursorTexture, _cursorHotspot, CursorMode.Auto);

                if (Input.GetMouseButtonDown(0))
                {
                    // This will return as clicked for couple of frames, so wait until mouse is up.
                    yield return new WaitWhile(() => Input.GetMouseButton(0));

                    // If enable while mouse is down, InteractWithMovement will triggered
                    playerController.enabled = true;

                    yield break;
                }

                yield return null;
            }
        }
        #endregion



        #region --Methods-- (Override)
        public override void StartTargeting(GameObject user)
        {
            PlayerController playerController = user.transform.root.GetComponentInChildren<PlayerController>();
            playerController.StartCoroutine( Targeting(user, playerController) );
        }
        #endregion
    }
}