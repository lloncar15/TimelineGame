using System;
using UnityEngine;

namespace GimGim.TimelineGame.UI {
    public class CardPlayArea : MonoBehaviour {
        /// <summary>
        /// Called when a card is dropped on this play area.
        /// </summary>
        public event Action<CardView> OnCardDroppedEvent;

        /// <summary>
        /// Called by CardDragHandler when a card is released over this area.
        /// </summary>
        public void OnCardDropped(CardView card) {
            OnCardDroppedEvent?.Invoke(card);
        }
    }
}