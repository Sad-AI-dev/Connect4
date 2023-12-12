using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Game {
    public class Selector : MonoBehaviour, IPointerClickHandler
    {
        public int columnID;
        public GridDirection placeDirection;

        //use Unity's build in click functionality
        public void OnPointerClick(PointerEventData eventData)
        {
            EventBus<TryPlaceEvent>.Invoke(new TryPlaceEvent { targetColumn = columnID, direction = placeDirection });
        }
    }
}
