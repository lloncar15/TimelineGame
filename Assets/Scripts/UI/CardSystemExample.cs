using UnityEngine;
using System.Collections.Generic;

namespace GimGim.TimelineGame.UI {
    /// <summary>
    /// Example script showing how to use the card system.
    /// This demonstrates wiring up the view events to your model.
    /// </summary>
    public class CardSystemExample : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private HandView handView;
        [SerializeField] private CardPlayArea playArea;
        [SerializeField] private CardView cardPrefab;
        
        [Header("Test Settings")]
        [SerializeField] private int initialCardCount = 5;

        private void Start()
        {
            // Subscribe to hand events
            handView.OnCardsSwapped += HandleCardsSwapped;
            handView.OnCardPlayed += HandleCardPlayed;
            handView.OnCardAdded += HandleCardAdded;
            handView.OnCardRemoved += HandleCardRemoved;
            
            // Subscribe to play area events
            if (playArea != null)
            {
                playArea.OnCardDroppedEvent += HandleCardDroppedOnPlayArea;
            }
            
            // Initialize with some cards
            for (int i = 0; i < initialCardCount; i++)
            {
                AddCardToHand();
            }
        }

        private void OnDestroy()
        {
            // Unsubscribe from events
            if (handView != null)
            {
                handView.OnCardsSwapped -= HandleCardsSwapped;
                handView.OnCardPlayed -= HandleCardPlayed;
                handView.OnCardAdded -= HandleCardAdded;
                handView.OnCardRemoved -= HandleCardRemoved;
            }
            
            if (playArea != null)
            {
                playArea.OnCardDroppedEvent -= HandleCardDroppedOnPlayArea;
            }
        }

        #region Model Synchronization

        private void HandleCardsSwapped(int fromIndex, int toIndex)
        {
        }

        private void HandleCardPlayed(CardView card)
        {
            Debug.Log($"Card played: {card.name}");
            // The card has already been removed from the hand view
            // Handle game logic here (e.g., apply card effects)
            
            // Destroy the card view after any play animation
            Destroy(card.gameObject, 0.5f);
        }

        private void HandleCardAdded(CardView card, int index)
        {
            Debug.Log($"Card added at index {index}");
        }

        private void HandleCardRemoved(CardView card, int index)
        {
        }

        private void HandleCardDroppedOnPlayArea(CardView card)
        {
            // This is called by the play area when a card is dropped on it
            // The HandView.OnCardPlayed event is also fired
            Debug.Log($"Card dropped on play area: {card.name}");
        }

        #endregion

        #region Public API Examples

        /// <summary>
        /// Example: Add a new card to the hand
        /// </summary>
        public void AddCardToHand()
        {
            if (cardPrefab == null) return;
            
            // Create view
            CardView newCard = Instantiate(cardPrefab);
            newCard.name = $"Card";
            
            // Add to view
            handView.AddCard(newCard);
            
            // Here you would bind the card data to the view
            // e.g., newCard.BindData(cardData);
        }

        /// <summary>
        /// Example: Remove a card from the hand by index
        /// </summary>
        public void RemoveCardFromHand(int index)
        {
            var card = handView.GetCardAt(index);
            if (card != null)
            {
                handView.RemoveCard(card, true);
            }
        }

        /// <summary>
        /// Example: Get all selected cards
        /// </summary>
        public List<CardView> GetSelectedCards()
        {
            return null;
        }

        /// <summary>
        /// Example: Play all selected cards
        /// </summary>
        public void PlaySelectedCards()
        {
            var selected = GetSelectedCards();
            foreach (var card in selected)
            {
                // Remove from hand and handle play logic
                handView.RemoveCard(card, false);
                HandleCardPlayed(card);
            }
        }

        /// <summary>
        /// Example: Disable selection (for games that don't use it)
        /// </summary>
        public void DisableSelection()
        {
            // handView.SetSelectionEnabled(false);
        }

        #endregion

        #region Debug Controls

        private void Update()
        {
            // Debug controls for testing
            if (Input.GetKeyDown(KeyCode.A))
            {
                AddCardToHand();
            }
            
            if (Input.GetKeyDown(KeyCode.R) && handView.CardCount > 0)
            {
                RemoveCardFromHand(handView.CardCount - 1);
            }
            
            if (Input.GetKeyDown(KeyCode.P))
            {
                PlaySelectedCards();
            }
            
            if (Input.GetKeyDown(KeyCode.D))
            {
                // handView.DeselectAll();
            }
        }

        #endregion
    }
}