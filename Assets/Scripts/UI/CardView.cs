using System;
using NUnit.Framework;
using PrimeTween;
using UnityEngine;
using UnityEngine.EventSystems;

namespace GimGim.TimelineGame.UI {
    public class CardView  : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, 
        IPointerDownHandler, IPointerUpHandler, IPointerClickHandler {
        [Header("References")]
        [SerializeField] private Transform shakeParent;
        [SerializeField] private Transform shadow;
        [SerializeField] private Transform tiltParent;
        [SerializeField] private Canvas sortingCanvas;
        
        [Header("Configuration")]
        [SerializeField] private CardViewConfig config;
        
        [Header("State")]
        [SerializeField] private int cardIndex;
        [SerializeField] private bool selectionEnabled = true;

        // Properties
        public int CardIndex {
            get => cardIndex;
            set {
                cardIndex = value;
                _idlePhaseOffset = value * config.idlePhaseOffsetPerCard;
            }
        }
        
        public bool IsSelected { get; private set; }
        public bool IsHovered { get; private set; }
        public bool IsDragging { get; private set; }
        public bool SelectionEnabled {
            get => selectionEnabled;
            set => selectionEnabled = value;
        }
        
        public HandView ParentHand { get; set; }
        public CardDragHandler DragHandler { get; private set; }
        
        // Events
        public event Action<CardView> OnSelected;
        public event Action<CardView> OnDeselected;
        public event Action<CardView> OnClicked;
        
        // Internal state
        private float _idlePhaseOffset;
        private Vector3 _basePosition;
        private Vector3 _shadowBaseLocalPosition;
        private Quaternion _baseTiltRotation;
        private float _selectionYOffset;
        
        // Tween handles for cleanup
        private Tween _scaleTween;
        private Tween _positionTween;
        private Tween _shakeTween;
        private Tween _shadowTween;
        
        // Mouse tilt state
        private Vector2 _targetMouseTilt;
        private Vector2 _currentMouseTilt;
        private RectTransform _rectTransform;
        private Vector3 _lastPosition;
        private Vector3 _velocity;
        private float _currentMovementTilt;
        private float _targetMovementTilt;
        
        // base card rotation
        private Quaternion _baseCardRotation = Quaternion.identity;

        private const float POSITION_TWEEN_DURATION_HAND = 0.2f;
        private const float POSITION_TWEEN_DURATION = 0.15f;

#if UNITY_EDITOR
        private void OnValidate() {
            Assert.IsNotNull(shakeParent, "CardView: Shake parent is null!");
            Assert.IsNotNull(tiltParent, "CardView: Tilt parent is null!");
            Assert.IsNotNull(config, "CardView: Config object is null!");
            Assert.IsNotNull(shadow, "CardView: Shadow object is null!");
            Assert.IsNotNull(sortingCanvas, "CardView: Sorting canvas is null!");
        }
#endif

        private void Awake() {
            _rectTransform = GetComponent<RectTransform>();
            DragHandler = GetComponent<CardDragHandler>();

            if (DragHandler == null) {
                DragHandler = gameObject.AddComponent<CardDragHandler>();
            }

            DragHandler.Initialize(this, config);

            _shadowBaseLocalPosition = shadow.localPosition;
            _baseTiltRotation = tiltParent.localRotation;
            _lastPosition = transform.localPosition;
        }

        private void Start() {
            _idlePhaseOffset = cardIndex * config.idlePhaseOffsetPerCard;
        }

        private void Update() {
            UpdateVelocityTracking();

            UpdateMovementTilt();
            
            if (IsDragging)
                return;

            if (IsHovered) {
                // UpdateMouseDrivenTilt();
            }
            else {
                UpdateIdleTilt();
            }
        }

        #region Movement Tilt

        /// <summary>
        /// Tracks velocity and last position for applying movement tilt.
        /// </summary>
        private void UpdateVelocityTracking() {
            
        }

        /// <summary>
        /// Applies movement-based Z rotation 
        /// </summary>
        private void UpdateMovementTilt() {
            
        }

        #endregion

        #region Idle Tilt Animation

        /// <summary>
        /// Lerp the card rotation in a circular manner while idle.
        /// </summary>
        private void UpdateIdleTilt() {
            float time = Time.time * config.idleTiltSpeed * _idlePhaseOffset;

            float xTilt = Mathf.Sin(time) * config.idleTiltAmplitude;
            float yTilt = Mathf.Cos(time) * config.idleTiltAmplitude;
            
            Quaternion targetRotation = _baseTiltRotation * Quaternion.Euler(xTilt, yTilt, 0);
            tiltParent.localRotation = Quaternion.Lerp(tiltParent.localRotation, targetRotation,
                Time.deltaTime * config.idleTiltTimeModifier);
        }

        #endregion
        
        public void OnPointerEnter(PointerEventData eventData) {
            throw new NotImplementedException();
        }

        public void OnPointerExit(PointerEventData eventData) {
            throw new NotImplementedException();
        }

        public void OnPointerDown(PointerEventData eventData) {
            throw new NotImplementedException();
        }

        public void OnPointerUp(PointerEventData eventData) {
            throw new NotImplementedException();
        }

        public void OnPointerClick(PointerEventData eventData) {
            throw new NotImplementedException();
        }
        
        #region Drag Support

        public void SetDragging(bool isDragging) {
            
        }
        
        #endregion
        
        #region Position Management

        public void SetBasePosition(Vector3 position, bool shouldAnimate = true, float duration = -1f) {
            _basePosition = position;
            if (IsDragging) 
                return;
            
            Vector3 targetPos = _basePosition + Vector3.up * _selectionYOffset;

            if (shouldAnimate) {
                _positionTween.Stop();
                float dur = duration > 0 ? duration : (ParentHand != null ? 
                    POSITION_TWEEN_DURATION_HAND : POSITION_TWEEN_DURATION);
                _positionTween = Tween.LocalPosition(transform, targetPos, dur, Ease.OutCubic);
            }
            else {
                transform.localPosition = targetPos;
            }
        }
        
        public Vector3 GetBasePosition() => _basePosition;

        public void SnapToBasePosition() {
            _positionTween.Stop();
            Vector3 targetPos = _basePosition + Vector3.up * _selectionYOffset;
            _positionTween = Tween.LocalPosition(transform, targetPos, config.dragSnapBackDuration, config.dragSnapBackEase);
        }
        
        /// <summary>
        /// Sets the base rotation for the card (e.g. fan rotation in the hand view)
        /// </summary>
        public void SetBaseRotation(Quaternion rotation, bool shouldAnimate = true, float duration = -1) {
            _baseCardRotation = rotation;
            
            if (!shouldAnimate) {
                _currentMovementTilt = 0f;
                _targetMovementTilt = 0f;
                transform.localRotation = rotation;
            }
        }
        
        #endregion

        #region Utiltiy

        public void SetSortingOrder(int order) {
            sortingCanvas.sortingOrder = order;
        }

        public int GetDefaultSortingOrder() {
            return config.defaultSortingOrder + cardIndex;
        }

        public void ResetVisualState() {
            _scaleTween.Stop();
            _positionTween.Stop();
            _shakeTween.Stop();
            _shadowTween.Stop();
            
            transform.localScale = Vector3.one;
            transform.localPosition = _basePosition;
            transform.localRotation = _baseCardRotation;
            
            shadow.localPosition = _shadowBaseLocalPosition;
            
            tiltParent.localRotation = _baseTiltRotation;

            IsSelected = false;
            IsHovered = false;
            IsDragging = false;
            _selectionYOffset = 0f;
            _currentMouseTilt = Vector2.zero;
            _targetMouseTilt = Vector2.zero;
            _currentMovementTilt = 0f;
            _targetMovementTilt = 0f;
            _velocity = Vector3.zero;
        }

        #endregion
    }
}