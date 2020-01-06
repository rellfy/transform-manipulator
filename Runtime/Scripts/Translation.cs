using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace TransformManipulation {

    [Serializable]
    public class Translation : ITransformManipulator {

        [SerializeField, Range(0.5f, 5f)]
        private float scale = 1f;
        [SerializeField]
        private TranslationAxes axesPrefab;
        private TranslationAxes axes;
        private Transform transform;

        public Transform Transform => this.transform;

        public EventHandler UpTranslation;
        public EventHandler RightTranslation;
        public EventHandler ForwardTranslation;

        public void Start(Transform target) {
            this.transform = target;
            this.axes = Object.Instantiate(this.axesPrefab.gameObject).GetComponent<TranslationAxes>();
            this.axes.Up.gameObject.AddComponent<ClickDelegate>().MouseDown += (sender, args) => { this.UpTranslation?.Invoke(sender, args); };
            this.axes.Right.gameObject.AddComponent<ClickDelegate>().MouseDown += (sender, args) => { this.RightTranslation?.Invoke(sender, args); };
            this.axes.Forward.gameObject.AddComponent<ClickDelegate>().MouseDown += (sender, args) => { this.ForwardTranslation?.Invoke(sender, args); };
        }

        public void Update() {
            UpdateIconTransform();
        }

        private void UpdateIconTransform() {
            this.axes.transform.position = this.transform.position;
            this.axes.transform.rotation = this.transform.rotation;
            this.axes.transform.localScale = Vector3.one * this.scale;
        }
    }
}