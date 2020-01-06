using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TransformManipulation {

    public class TransformManipulator : MonoBehaviour {

        [SerializeField]
        private Transform target;
        [SerializeField]
        private Rotation rotation;
        [SerializeField]
        private Translation translation;
        [SerializeField]
        private Camera camera;

        /// <summary>
        /// Hide this MonoBehaviour's transform as it will not be used in any scenario.
        /// </summary>
        public new Transform transform => this.target;
        public Camera Camera {
            get => this.camera;
            set {
                this.camera = value;
                this.translation.Camera = value;
            }
        }

        private void Start() {
            if (this.camera == null)
                this.camera = Camera.main;
        }

        private void Update() {
            if (this.target == null) {
                ClearTarget();
                return;
            }

            if (this.rotation.Transform == null)
                this.rotation.Start(this.target);

            if (this.translation.Transform == null) {
                this.translation.Start(this.target);
                this.translation.Camera = this.camera;
            }

            this.rotation.Update();
            this.translation.Update();
        }

        /// <summary>
        /// Set this manipulator's target Transform.
        /// </summary>
        /// <param name="target"></param>
        public void SetTarget(Transform target) {
            this.target = target;
        }

        /// <summary>
        /// Removes target and widgets.
        /// </summary>
        private void ClearTarget() {
            this.target = null;
            this.translation.ClearTransform();
            this.rotation.ClearTransform();
        }
    }

}