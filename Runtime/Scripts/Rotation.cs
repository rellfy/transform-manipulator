using System;
using UnityEngine;
using Object = System.Object;

namespace TransformManipulation {

    [Serializable]
    public class Rotation : ITransformManipulator {

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
        private Transform transform;
        private RotationAxis upAxis;
        private RotationAxis rightAxis;
        private RotationAxis forwardAxis;
        private Rotator up;
        private Rotator right;
        private Rotator forward;

        public Transform Transform => this.transform;
        protected Collider PrefabInstance => UnityEngine.Object.Instantiate(this.rotationPrefab, this.transform);
        protected float RadiusFromOrigin => this.transform.localScale.magnitude * this.radiusFromOrigin;

        public void Start(Transform target) {
            this.transform = target;

            this.upAxis = new RotationAxis(target, Vector3.right, Vector3.up);
            this.rightAxis = new RotationAxis(target, Vector3.forward, Vector3.right);
            this.forwardAxis = new RotationAxis(target, Vector3.up, Vector3.forward);

            this.up = new Rotator(PrefabInstance, PrefabInstance, this.upAxis, this.upAxisColor);
            this.right = new Rotator(PrefabInstance, PrefabInstance, this.rightAxis, this.rightAxisColor);
            this.forward = new Rotator(PrefabInstance, PrefabInstance, this.forwardAxis, this.forwardAxisColor);

            //this.upCW.AddComponent<ClickDelegate>().MouseDown += (sender, args) => { Rotate(Vector3.up); };
            //this.upCCW.AddComponent<ClickDelegate>().MouseDown += (sender, args) => { Rotate(-Vector3.up); };
            //this.rightCW.AddComponent<ClickDelegate>().MouseDown += (sender, args) => { Rotate(Vector3.right); };
            //this.rightCCW.AddComponent<ClickDelegate>().MouseDown += (sender, args) => { Rotate(-Vector3.right); };
            //this.forwardCW.AddComponent<ClickDelegate>().MouseDown += (sender, args) => { Rotate(Vector3.forward); };
            //this.forwardCCW.AddComponent<ClickDelegate>().MouseDown += (sender, args) => { Rotate(-Vector3.forward); };
        }

        public void Update() {
            this.up.Update(RadiusFromOrigin);
            this.right.Update(RadiusFromOrigin);
            this.forward.Update(RadiusFromOrigin);
        }

        private void Rotate(Vector3 rotation) {

        }
    }
}