using System.Collections.Generic;
using UnityEngine;

namespace RPG.Dialogue
{
    public class DialogueNode : ScriptableObject
    {
        #region --Fields-- (Inspector)
        [SerializeField] private string _text;
        [SerializeField] private List<string> _children = new List<string>(); // IF has to initialize first otherwise will get null exception when try to access in GetAllChildren method
        [SerializeField] private Rect _rect = new Rect(10, 10, 200, 100);
        #endregion



        #region --Properties-- (With Backing Fields)
        public string Text { get { return _text; } set { _text = value; } }
        public List<string> Children { get { return _children; } }
        public Rect Rect { get { return _rect; } set { _rect = value; } }
        #endregion
    }
}