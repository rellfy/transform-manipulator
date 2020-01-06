using System;
using UnityEngine;
using Object = System.Object;

namespace TransformManipulation {

    public class Rotator {
        private readonly Collider clockwise;
        private readonly Collider counterClockwise;
        private readonly RotationAxis axis;

        public enum RotationType {
            Clockwise,
            CounterClockwise
        }

        public EventHandler<RotationType> RotationRequestStarted;
        public EventHandler<RotationType> RotationRequestEnded;

        public Rotator(Collider clockwise, Collider counterClockwise, RotationAxis axis, Color widgetColor) {
            this.clockwise = clockwise;
            this.counterClockwise = counterClockwise;
            this.axis = axis;

            SetColor(widgetColor);
            Listen();
        }

        private void SetColor(Color color) {
            MaterialPropertyBlock propertyBlock = new MaterialPropertyBlock();
            propertyBlock.SetColor("_BaseColor", color);
            propertyBlock.SetColor("_Color", color);
            this.clockwise.GetComponentInChildren<Renderer>().SetPropertyBlock(propertyBlock);
            this.counterClockwise.GetComponentInChildren<Renderer>().SetPropertyBlock(propertyBlock);
        }

        private void Listen() {
            ClickDelegate clockwise = this.clockwise.gameObject.AddComponent<ClickDelegate>();
            ClickDelegate counterClockwise = this.counterClockwise.gameObject.AddComponent<ClickDelegate>();

            clockwise.MouseDown += OnClockwiseWidgetClickDown;
            counterClockwise.MouseDown += OnCounterClockwiseWidgetDown;

            clockwise.MouseUp += OnClockwiseWidgetClickUp;
            counterClockwise.MouseUp += OnCounterClockwiseWidgetUp;
        }

        private void OnClockwiseWidgetClickDown(object sender, EventArgs args) {
            this.RotationRequestStarted?.Invoke(this.axis, RotationType.Clockwise);
        }

        private void OnCounterClockwiseWidgetDown(object sender, EventArgs args) {
            this.RotationRequestStarted?.Invoke(this.axis, RotationType.CounterClockwise);
        }

        private void OnClockwiseWidgetClickUp(object sender, EventArgs args) {
            this.RotationRequestEnded?.Invoke(this.axis, RotationType.Clockwise);
        }

        private void OnCounterClockwiseWidgetUp(object sender, EventArgs args) {
            this.RotationRequestEnded?.Invoke(this.axis, RotationType.CounterClockwise);
        }

        public void Update(float radiusFromOrigin) {
            UpdateWidget(radiusFromOrigin);
        }

        /// <summary>
        /// Updates the widget's position and rotation.
        /// </summary>
        /// <param name="radiusFromOrigin"></param>
        private void UpdateWidget(float radiusFromOrigin) {
            this.clockwise.transform.position = this.axis.Target.position + this.axis.Forward * radiusFromOrigin;
            this.counterClockwise.transform.position = this.axis.Target.position - this.axis.Forward * radiusFromOrigin;
                
            this.clockwise.transform.rotation = Quaternion.LookRotation(-this.axis.Forward, this.axis.Up);
            this.counterClockwise.transform.rotation = Quaternion.LookRotation(this.axis.Forward, this.axis.Up);
        }

        public void Clear() {
            if (this.clockwise != null)
                UnityEngine.Object.DestroyImmediate(this.clockwise.gameObject);
            if (this.counterClockwise != null)
                UnityEngine.Object.DestroyImmediate(this.counterClockwise.gameObject);
        }
    }
}