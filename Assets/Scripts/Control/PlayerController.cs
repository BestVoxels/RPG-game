using UnityEngine;
using RPG.Movement;
using RPG.Attributes;
using UnityEngine.EventSystems;
using UnityEngine.AI;
using System;

namespace RPG.Control
{
    public class PlayerController : MonoBehaviour
    {
        #region --Fields-- (Inspector)
        [Tooltip("Only Navigate where there is NavMesh with 'extended range' to place where there is No NavMesh")]
        [Range(0.1f, 4f)]
        [SerializeField] private float _maxNavMeshDetectionRange = 2f;
        [Tooltip("How long can Player Travel? need to have maximum otherwise player can just go in one shot on the other side of river for example.")]
        [SerializeField] private float _maxNavMeshPathLength = 40f;
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



        #region --Methods-- (Custom PRIVATE) ~Raycast Stuff~
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
            RaycastHit[] hitsInfo = RaycastAllSorted();

            foreach (RaycastHit eachHit in hitsInfo)
            {
                foreach (IRaycastable eachRaycastable in eachHit.transform.GetComponents<IRaycastable>())
                {
                    if (eachRaycastable.HandleRaycast(this))
                    {
                        SetCursor(eachRaycastable.GetCursorType());
                        return true;
                    }
                }
            }
            return false;
        }

        private bool InteractWithMovement()
        {
            if (RaycastNavMesh(out Vector3 target))
            {
                if (Input.GetMouseButton(0))
                {
                    _mover.StartMoveAction(target, 1f);
                }

                SetCursor(CursorType.Movement);
                return true;
            }
            return false;
        }

        private RaycastHit[] RaycastAllSorted()
        {
            // Draw the ray GET ALL, WON'T GET BLOCK & SORTED with Hit Distance
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());

            float[] distances = new float[hits.Length];
            for (int i = 0; i < distances.Length; i++)
            {
                distances[i] = hits[i].distance;
            }

            Array.Sort(distances, hits);

            return hits;
        }

        private bool RaycastNavMesh(out Vector3 target)
        {
            // Raycast to Terrain
            if (Physics.Raycast(GetMouseRay(), out RaycastHit hitInfo))
            {
                // Only Navigate where there is NavMesh
                if (NavMesh.SamplePosition(hitInfo.point, out NavMeshHit navMeshHit, _maxNavMeshDetectionRange, NavMesh.AllAreas))
                {
                    target = navMeshHit.position;

                    // Only Navigate where the NavMeshPath is not Cut (like NavMesh on the roof) & Not Too Far Away
                    NavMeshPath path = new NavMeshPath();
                    if (NavMesh.CalculatePath(transform.position, target, NavMesh.AllAreas, path))
                    {
                        if (path.status == NavMeshPathStatus.PathComplete && GetPathLength(path) < _maxNavMeshPathLength)
                            return true;
                    }
                }
            }

            target = Vector3.zero;
            return false;
        }

        private float GetPathLength(NavMeshPath path)
        {
            // SUM All Distance Between each Corners Pair that Player has to travel

            Vector3[] pathCorners = path.corners;
            float totalLength = 0f;
            if (pathCorners.Length < 2) return totalLength;

            for (int i = 1; i < pathCorners.Length; i++)
            {
                totalLength += Vector3.Distance(pathCorners[i - 1], pathCorners[i]);
            }
            
            return totalLength;
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