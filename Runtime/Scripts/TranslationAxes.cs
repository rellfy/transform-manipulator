using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TransformManipulation {

    public class TranslationAxes : MonoBehaviour {

        [SerializeField]
        private Collider up;
        [SerializeField]
        private Collider right;
        [SerializeField]
        private Collider forward;

        public Collider Up => this.up;
        public Collider Right => this.right;
        public Collider Forward => this.forward;
    }
}