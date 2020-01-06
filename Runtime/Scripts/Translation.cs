using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace TransformManipulation {

    [Serializable]
    public class Translation : ITransformManipulator {

        [SerializeField, Range(0.5f, 5f)]
        private float scale = 1f;
        private Vector3 translationInput;
        private Vector3 lastMousePosition;
        [SerializeField]
        private Vector3 worldMouseVelocity;
        [SerializeField]
        private TranslationAxes axesPrefab;
        private TranslationAxes axes;
        private Transform transform;
        private Camera camera;

        protected Vector3 MousePosition => new Vector3(Input.mousePosition.x, Input.mousePosition.y, this.camera.transform.position.z);
        public Transform Transform => this.transform;
        public Camera Camera {
            get => this.camera;
            set => this.camera = value;
        }

        public void Start(Transform target) {
            this.transform = target;
            this.translationInput = Vector3.zero;
            this.axes = Object.Instantiate(this.axesPrefab.gameObject, this.transform).GetComponent<TranslationAxes>();

            Listen();
        }

        private void Listen() {
            ClickDelegate up = this.axes.Up.gameObject.AddComponent<ClickDelegate>();
            ClickDelegate right = this.axes.Right.gameObject.AddComponent<ClickDelegate>();
            ClickDelegate forward = this.axes.Forward.gameObject.AddComponent<ClickDelegate>();

            up.MouseDown += (sender, args) => { this.translationInput = Vector3.up; };
            right.MouseDown += (sender, args) => { this.translationInput = Vector3.right; };
            forward.MouseDown += (sender, args) => { this.translationInput = Vector3.forward; };

            up.MouseUp += (sender, args) => { ClearTranslation(); };
            right.MouseUp += (sender, args) => { ClearTranslation(); };
            forward.MouseUp += (sender, args) => { ClearTranslation(); };
        }

        public void Update() {
            if (this.camera == null) {
                Debug.LogWarning("Cannot execute TransformManipulation.Translation: camera was not passed to object.");
                return;
            }

            UpdateIconTransform();
            UpdateMouseVelocity();
            Translate();
        }

        private void UpdateIconTransform() {
            this.axes.transform.position = this.transform.position;
            this.axes.transform.rotation = this.transform.rotation;
            this.axes.transform.localScale = Vector3.one * this.scale;
        }

        private void UpdateMouseVelocity() {
            this.worldMouseVelocity = Camera.main.ScreenToWorldPoint(MousePosition) -  Camera.main.ScreenToWorldPoint(this.lastMousePosition);
            this.lastMousePosition = MousePosition;
        }

        private void Translate() {
            Vector3 scaledLocalDirection = Vector3.Scale(this.translationInput, this.transform.InverseTransformDirection(-this.worldMouseVelocity));
            this.transform.position += (this.transform.rotation * scaledLocalDirection).normalized * this.worldMouseVelocity.magnitude;
        }
        
        private void ClearTranslation() {
            this.translationInput = Vector3.zero;
        }
    }
}