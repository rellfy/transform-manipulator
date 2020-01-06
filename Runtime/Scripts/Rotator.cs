using System;
using UnityEngine;
using Object = System.Object;

namespace TransformManipulation {

    public struct Rotator {
        private Collider clockwise;
        private Collider counterClockwise;
        private RotationAxis axis;

        public enum RotationType {
            Clockwise,
            CounterClockwise
        }

        public EventHandler<RotationType> Rotated;

        public Rotator(Collider clockwise, Collider counterClockwise, RotationAxis axis, Color widgetColor) {
            this.clockwise = clockwise;
            this.counterClockwise = counterClockwise;
            this.axis = axis;
            this.Rotated = null;
            SetColor(widgetColor);
        }

        private void SetColor(Color color) {
            MaterialPropertyBlock propertyBlock = new MaterialPropertyBlock();
            propertyBlock.SetColor("_BaseColor", color);
            propertyBlock.SetColor("_Color", color);
            this.clockwise.GetComponentInChildren<Renderer>().SetPropertyBlock(propertyBlock);
            this.counterClockwise.GetComponentInChildren<Renderer>().SetPropertyBlock(propertyBlock);
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
    }
}