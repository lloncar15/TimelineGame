using UnityEngine;
using PrimeTween;

namespace GimGim.TimelineGame.UI {
    [CreateAssetMenu(fileName = "HandViewConfig", menuName = "TimelineGame/Hand View Config")]
    public class HandViewConfig : ScriptableObject {
        [Header("Fan Layout")]
        [Tooltip("Curve for Y position offset based on normalized position (0-1) in hand. Center should be highest.")]
        public AnimationCurve fanYCurve = new(
            new Keyframe(0f, 0f),
            new Keyframe(0.5f, 20f),
            new Keyframe(1f, 0f)
        );
        
        [Tooltip("Curve for Z rotation based on normalized position (0-1) in hand. Left cards rotate positive, right cards negative.")]
        public AnimationCurve fanRotationCurve = new(
            new Keyframe(0f, 10f),
            new Keyframe(0.5f, 0f),
            new Keyframe(1f, -10f)
        );

        [Header("Card Spacing")]
        [Tooltip("Minimum spacing between card centers")]
        public float minCardSpacing = 40f;
        
        [Tooltip("Maximum spacing between card centers")]
        public float maxCardSpacing = 120f;
        
        [Tooltip("Preferred spacing when there's enough room")]
        public float preferredCardSpacing = 100f;

        [Header("Animation")]
        [Tooltip("Duration for cards to animate to their positions")]
        public float cardMoveDuration = 0.2f;
        
        [Tooltip("Easing for card position animations")]
        public Ease cardMoveEase = Ease.OutCubic;
        
        [Tooltip("Duration for card swap animations")]
        public float swapDuration = 0.15f;
        
        [Tooltip("Easing for swap animations")]
        public Ease swapEase = Ease.OutQuad;

        [Header("Hover Behavior")]
        [Tooltip("How much to raise neighboring cards when hovering")]
        public float neighborHoverRaise = 10f;
        
        [Tooltip("How many neighbors on each side to affect")]
        public int neighborHoverCount = 1;
    }
}