using System;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

namespace GimGim.TimelineGame.UI {
    public class HandView : MonoBehaviour {
        [Header("Configuration")]
        [SerializeField] private HandViewConfig config;
        [SerializeField] private CardViewConfig cardViewConfig;
        
        [Header("Layout")]
        [SerializeField] private RectTransform handContainer;
        
        // Card management
        private List<CardView> _cards = new();
        private List<int> _cardSlots = new();
        
        // Drag state
        private CardView _currentlyDraggedCard;
        private int _draggedCardOriginalSlot;
        
        // Events
        public event Action<int, int> OnCardsSwapped;
        public event Action<CardView> OnCardPlayed;
        public event Action<CardView, int> OnCardAdded;
        public event Action<CardView, int> OnCardRemoved;
        
#if UNITY_EDITOR
        private void OnValidate() {
            Assert.IsNotNull(handContainer, "HandView: HandContainer object is null!");
            Assert.IsNotNull(config, "HandView: HandViewConfig object is null!");
            Assert.IsNotNull(cardViewConfig, "CardView: CardViewConfig object is null!");
        }
#endif
        
        #region Card Management

        /// <summary>
        /// Adds a card to the hand at the specified index (or at the end if index is -1)
        /// </summary>
        public void AddCard(CardView card, int index = -1) {
            if (card == null)
                return;

            if (index < 0 || index >= _cards.Count) {
                index = _cards.Count;
            }
            
            _cards.Insert(index, card);
            RebuildSlotIndices();
            
            card.transform.SetParent(handContainer, false);
            card.ParentHand = this;
            card.CardIndex = index;
            
            UpdateCardPositions();
            
            OnCardAdded?.Invoke(card, index);
        }

        /// <summary>
        /// Removes a card from the hand
        /// </summary>
        public void RemoveCard(CardView card, bool destroy = false) {
            int index = _cards.IndexOf(card);
            
            
            OnCardRemoved?.Invoke(card, index);
        }
        
        /// <summary>
        /// Gets the current card at the specified index
        /// </summary>
        public CardView GetCardAt(int index) {
            if (index < 0 || index >= _cards.Count) 
                return null;
            return _cards[index];
        }
        
        /// <summary>
        /// Gets all cards in hand order
        /// </summary>
        public IReadOnlyList<CardView> GetCards() => _cards.AsReadOnly();
        
        /// <summary>
        /// Gets the number of cards in the hand
        /// </summary>
        public int CardCount => _cards.Count;
        
        private void RebuildSlotIndices() {
            _cardSlots.Clear();
            for (int i = 0; i < _cards.Count; i++) {
                _cardSlots.Add(i);
                _cards[i].CardIndex = i;
            }
        }
        
        #endregion
        
        #region Layout

        /// <summary>
        /// Recalculates and animates all card positions
        /// </summary>
        public void UpdateCardPositions(bool shouldAnimate = true) {
            int cardsCount = _cards.Count;
            if (cardsCount == 0)
                return;

            float spacing = CalculateCardSpacing();

            float totalWidth = (cardsCount - 1) * spacing;
            float startX = -totalWidth / 2;

            for (int i = 0; i < cardsCount; i++) {
                CardView card = _cards[i];
                if (card == null)
                    continue;

                if (card == _currentlyDraggedCard)
                    continue;

                float normalizedPos = cardsCount > 1 ? (float)i / (cardsCount - 1) : 0.5f;

                float xPos = startX + i * spacing;
                float yPos = config.fanYCurve.Evaluate(normalizedPos);
                Vector3 targetPos = new(xPos, yPos, 0);

                float zRotation = config.fanRotationCurve.Evaluate(normalizedPos);
                Quaternion targetRot = Quaternion.Euler(0f, 0f, zRotation);

                card.CardIndex = i;
                card.SetSortingOrder(cardViewConfig.defaultSortingOrder + i);

                float duration = shouldAnimate ? config.cardMoveDuration : 0f;
                card.SetBasePosition(targetPos, shouldAnimate, duration);
                card.SetBaseRotation(targetRot, shouldAnimate, duration);
            }
        }

        private float CalculateCardSpacing() {
            float spacing = config.preferredCardSpacing;

            if (_cards.Count <= 1)
                return spacing;

            int cardCountForSpacing = _cards.Count - 1;
            float availableSpace = handContainer.rect.width;
            float requiredSpace = cardCountForSpacing * config.preferredCardSpacing;

            if (requiredSpace > availableSpace) {
                spacing = Mathf.Max(config.minCardSpacing, availableSpace / cardCountForSpacing);
            }
            else if (requiredSpace < availableSpace) {
                spacing = Mathf.Min(config.maxCardSpacing, availableSpace / cardCountForSpacing);
            }

            return spacing;
        }
        
        #endregion

        #region Utility

        /// <summary>
        /// Forces an immediate layout update without animation
        /// </summary>
        public void ForceLayoutUpdate() {
            UpdateCardPositions(false);
        }

        #endregion
    }
}