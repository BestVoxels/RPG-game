using UnityEngine;
using RPG.Movement;
using RPG.Attributes;
using UnityEngine.EventSystems;

namespace RPG.Control
{
    public class PlayerController : MonoBehaviour
    {
        private enum CursorType
        {
            None,
            Movement,
            Combat,
            UI
        }



        #region --Fields-- (Inspector)
        [SerializeField] private CursorMapping[] _cursorMappings = null;
        #endregion



        #region --Fields-- (In Class)
        private Camera _camera;
        private Mover _mover;
        private Health _health;
        #endregion



        #region --Methods-- (Built In)
        private void Awake()
        {
            _camera = Camera.main;
            _mover = GetComponent<Mover>();
            _health = GetComponent<Health>();
        }

        private void Update()
        {
            if (InteractWithUI()) return;
            if (_health.IsDead)
            {
                SetCursor(CursorType.None);
                return;
            }

            if (InteractWithComponent()) return;
            if (InteractWithMovement()) return;

            SetCursor(CursorType.None);
        }
        #endregion



        #region --Methods-- (Custom PRIVATE)
        private bool InteractWithUI()
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                SetCursor(CursorType.UI);
                return true;
            }

            return false;
        }

        private bool InteractWithComponent()
        {
            // Draw the ray GET ALL, WON'T GET BLOCK
            RaycastHit[] hitsInfo = Physics.RaycastAll(GetMouseRay());

            foreach (RaycastHit eachHit in hitsInfo)
            {
                foreach (IRaycastable eachRaycastable in eachHit.transform.GetComponents<IRaycastable>())
                {
                    if (eachRaycastable.HandleRaycast(this))
                    {
                        SetCursor(CursorType.Combat);
                        return true;
                    }
                }
            }
            return false;
        }

        private bool InteractWithMovement()
        {
            // Draw the ray GET FIRST HIT
            if (Physics.Raycast(GetMouseRay(), out RaycastHit hitInfo))
            {
                if (Input.GetMouseButton(0))
                {
                    _mover.StartMoveAction(hitInfo.point, 1f);
                }

                SetCursor(CursorType.Movement);
                return true;
            }
            return false;
        }

        private Ray GetMouseRay() => _camera.ScreenPointToRay(Input.mousePosition); // get ray direction from camera to a screen point
        #endregion



        #region --Methods-- (Custom PRIVATE) ~Cursor Stuff~
        private void SetCursor(CursorType cursorType)
        {
            CursorMapping mapping = GetCursorMapping(cursorType);
            Cursor.SetCursor(mapping.texture, mapping.hotspot, CursorMode.Auto);
        }

        private CursorMapping GetCursorMapping(CursorType cursorType)
        {
            foreach (CursorMapping eachMapping in _cursorMappings)
            {
                if (eachMapping.cursorType == cursorType)
                {
                    return eachMapping;
                }
            }

            return _cursorMappings[0];
        }
        #endregion



        #region --Structs-- (Custom PRIVATE)
        [System.Serializable]
        private struct CursorMapping
        {
            public CursorType cursorType;
            public Texture2D texture;
            public Vector2 hotspot;
        }
        #endregion
    }
}