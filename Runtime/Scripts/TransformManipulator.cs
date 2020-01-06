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

        /// <summary>
        /// Hide this MonoBehaviour's transform as it will not be used in any scenario.
        /// </summary>
        public new Transform transform => this.target;

        private void Update() {
            if (this.target == null)
                return;

            if (this.rotation.Transform == null)
                this.rotation.Start(this.target);

            if (this.translation.Transform == null)
                this.translation.Start(this.target);

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
    }

}