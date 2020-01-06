using System;
using UnityEngine;

namespace TransformManipulation {

    [RequireComponent(typeof(Collider))]
    public class ClickDelegate : MonoBehaviour {

        public event EventHandler MouseDown;

        private void OnMouseDown() {
            MouseDown?.Invoke(this, null);
        }
    }
}