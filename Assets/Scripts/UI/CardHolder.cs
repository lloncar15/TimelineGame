using System.Collections;
using UnityEngine;

namespace GimGim.TimelineGame.UI {
    public class CardHolder : MonoBehaviour, ICardHolder {
        public IEnumerator AddCard(CardView card) {
            yield return null;
        }

        public IEnumerator RemoveCard(int cardIndex) {
            yield return null;
        }

        public IEnumerator UpdateCardPositions() {
            yield return null;
        }
    }
}