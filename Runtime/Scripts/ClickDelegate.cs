using System;
using UnityEngine;

namespace TransformManipulation {

    [RequireComponent(typeof(Collider))]
    public class ClickDelegate : MonoBehaviour {

        public event EventHandler MouseDown;
        public event EventHandler MouseUp;

        private void OnMouseDown() {
            MouseDown?.Invoke(this, null);
        }

        private void OnMouseUp() {
            MouseUp?.Invoke(this, null);
        }
    }
}