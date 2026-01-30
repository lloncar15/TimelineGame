using UnityEngine;
using PrimeTween;

namespace GimGim.TimelineGame.UI {
    [CreateAssetMenu(fileName = "CardViewConfig", menuName = "TimelineGame/Card View Config")]
    public class CardViewConfig : ScriptableObject {
        #region Idle Tilt Animation

        [Header("Idle Tilt Animation")]
        [Tooltip("Maximum tilt angle in degrees for idle animation")]
        public float idleTiltAmplitude = 3f;
        
        [Tooltip("Speed of the idle tilt oscillation")]
        public float idleTiltSpeed = 1.5f;
        
        [Tooltip("Phase offset multiplier per card index")]
        public float idlePhaseOffsetPerCard = 0.5f;

        [Tooltip("Tilt lerp time modifier")] 
        public float idleTiltTimeModifier = 10f;

        #endregion

        #region Selection

        [Header("Selection")]
        [Tooltip("Scale multiplier when card is selected")]
        public float selectedScale = 1.05f;
        
        [Tooltip("How much the card raises (Y offset) when selected")]
        public float selectedRaiseAmount = 30f;
        
        [Tooltip("Duration of selection animation")]
        public float selectionDuration = 0.15f;
        
        [Tooltip("Easing for selection animation")]
        public Ease selectionEase = Ease.OutBack;

        #endregion

        #region Hover Effect
        
        [Header("Hover Effects")]
        [Tooltip("Scale multiplier when hovering over card")]
        public float hoverScale = 1.08f;
        
        [Tooltip("Duration of hover scale animation")]
        public float hoverScaleDuration = 0.1f;
        
        [Tooltip("Shake rotation intensity in degrees")]
        public float hoverShakeIntensity = 5f;
        
        [Tooltip("Duration of the shake punch")]
        public float hoverShakeDuration = 0.2f;
        
        [Tooltip("Maximum tilt angle for mouse-driven 3D effect")]
        public float hoverMouseTiltMax = 15f;
        
        [Tooltip("How quickly the mouse-driven tilt follows the mouse")]
        public float hoverMouseTiltSpeed = 10f;

        #endregion
        
        #region Shadow Effect

        [Header("Pointer Down/Up (Shadow)")]
        [Tooltip("Shadow offset when pointer is down")]
        public Vector2 shadowPressedOffset = new Vector2(3f, -3f);
        
        [Tooltip("Duration of shadow offset animation")]
        public float shadowOffsetDuration = 0.08f;
        
        #endregion
        
        #region Drag Effect

        [Header("Drag")]
        [Tooltip("Maximum Z rotation tilt when dragging")]
        public float dragTiltMaxAngle = 15f;
        
        [Tooltip("How quickly the card follows the drag position")]
        public float dragFollowSpeed = 15f;
        
        [Tooltip("How quickly the drag tilt responds to movement")]
        public float dragTiltSpeed = 8f;
        
        [Tooltip("Sorting order to use when dragging")]
        public int dragSortingOrder = 100;
        
        [Tooltip("Duration of snap back animation when drag ends")]
        public float dragSnapBackDuration = 0.25f;
        
        [Tooltip("Easing for snap back animation")]
        public Ease dragSnapBackEase = Ease.OutBack;
        
        #endregion

        #region Movement Tilt

        [Header("Movement Tilt (Velocity-Based)")]
        [Tooltip("Maximum Z rotation angle when moving")]
        public float movementTiltMaxAngle = 15f;
        
        [Tooltip("How quickly the tilt responds to velocity changes")]
        public float movementTiltSpeed = 8f;
        
        [Tooltip("Multiplier to convert velocity to tilt influence (lower = need more speed to tilt)")]
        public float movementTiltVelocityScale = 0.002f;

        #endregion

        [Header("General")]
        [Tooltip("Default sorting order for cards")]
        public int defaultSortingOrder = 0;
    }
}