using System;
using UnityEngine;
using Object = System.Object;

namespace TransformManipulation {

    [Serializable]
    public class Rotation : ITransformManipulator {

        [SerializeField]
        private float rotationSpeed = 100f;
        [SerializeField]
        private float radiusFromOrigin = 2f;
        [SerializeField]
        private Color forwardAxisColor = Color.blue;
        [SerializeField]
        private Color upAxisColor = Color.green;
        [SerializeField]
        private Color rightAxisColor = Color.red;
        [SerializeField]
        private Collider rotationPrefab;

        private RotationAxis upAxis;
        private RotationAxis rightAxis;
        private RotationAxis forwardAxis;
        private Vector3 rotationInput;
        private Transform transform;
        private Rotator up;
        private Rotator right;
        private Rotator forward;

        protected float RadiusFromOrigin => this.transform.localScale.magnitude * this.radiusFromOrigin;
        public float RotationSpeed {
            get => this.rotationSpeed;
            set => this.rotationSpeed = value;
        }
        public Transform Transform => this.transform;
        protected Collider PrefabInstance => UnityEngine.Object.Instantiate(this.rotationPrefab, this.transform);

        public void Start(Transform target) {
            this.transform = target;
            this.rotationInput = Vector3.zero;

            this.upAxis = new RotationAxis(target, Vector3.right, Vector3.up);
            this.rightAxis = new RotationAxis(target, Vector3.forward, Vector3.right);
            this.forwardAxis = new RotationAxis(target, Vector3.up, Vector3.forward);

            this.up = new Rotator(PrefabInstance, PrefabInstance, this.upAxis, this.upAxisColor);
            this.right = new Rotator(PrefabInstance, PrefabInstance, this.rightAxis, this.rightAxisColor);
            this.forward = new Rotator(PrefabInstance, PrefabInstance, this.forwardAxis, this.forwardAxisColor);

            Listen();
        }

        private void Listen() {
            this.up.RotationRequestStarted += StartRotation;
            this.right.RotationRequestStarted += StartRotation;
            this.forward.RotationRequestStarted += StartRotation;

            this.up.RotationRequestEnded += EndRotation;
            this.right.RotationRequestEnded += EndRotation;
            this.forward.RotationRequestEnded += EndRotation;
        }

        public void Update() {
            this.up.Update(RadiusFromOrigin);
            this.right.Update(RadiusFromOrigin);
            this.forward.Update(RadiusFromOrigin);

            this.transform.Rotate(this.rotationInput.normalized * this.rotationSpeed * Time.deltaTime, Space.World);
        }

        private void StartRotation(object sender, Rotator.RotationType rotationType) {
            RotationAxis axis = (RotationAxis)sender;
            Vector3 rotation = Vector3.Cross(axis.Forward, axis.Up) * (rotationType == Rotator.RotationType.Clockwise ? 1f : -1f);
            this.rotationInput += rotation;
        }

        private void EndRotation(object sender, Rotator.RotationType rotationType) {
            RotationAxis axis = (RotationAxis)sender;
            Vector3 rotation = Vector3.Cross(axis.Forward, axis.Up) * (rotationType == Rotator.RotationType.Clockwise ? 1f : -1f);
            this.rotationInput = Vector3.zero;
        }
    }
}