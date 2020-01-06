using System;
using UnityEngine;
using Object = System.Object;

namespace TransformManipulation {

    public struct RotationAxis {
        private readonly Vector3 forwardLocal;
        private readonly Vector3 upLocal;
        private readonly Transform target;

        public Vector3 Up => Vector3.Cross(Forward, TargetDirection(this.upLocal));
        public Vector3 Forward => TargetDirection(this.forwardLocal);
        public Transform Target => this.target;

        private Vector3 TargetDirection(Vector3 direction) => this.target.TransformDirection(direction);

        public RotationAxis(Transform target, Vector3 forward, Vector3 up) {
            this.target = target;
            this.upLocal = up;
            this.forwardLocal = forward;
        }
    }
}