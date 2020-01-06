using System;
using UnityEngine;
using Object = System.Object;

namespace TransformManipulation {

    interface ITransformManipulator {
        Transform Transform { get; }
        void Start(Transform target);
        void Update();
    }
}