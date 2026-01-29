using System.Collections;

namespace GimGim.TimelineGame.UI {
    public interface ICardHolder {
        IEnumerator AddCard(CardView card);
        IEnumerator RemoveCard(int cardIndex);
        IEnumerator UpdateCardPositions();
    }
}