using System;
using UnityEngine;

namespace Cards
{
    public abstract class PlayerCard : Card
    {
        public delegate void PlayerCardSubmitted(PlayerCard card);
        public static event PlayerCardSubmitted OnPlayerCardSubmitted;
        
        public delegate void PlayerCardRetracted(PlayerCard card);
        public static event PlayerCardRetracted OnPlayerCardRetracted;
        
        private Vector3 _inHandPosition;
        public HandManager hand;
        private bool _isPreSubmitted;
        private const float DistanceToExtractCard = 7.6f;
        private bool _hasBeenTouched;

        private void OnMouseEnter()
        {
            if (!_hasBeenTouched)
            {
                _inHandPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
                _hasBeenTouched = true;
            }
            
            transform.position = new Vector3(_inHandPosition.x, _inHandPosition.y + DistanceToExtractCard, _inHandPosition.z);
        }

        private void OnMouseExit()
        {
            if (!isDiscarded && !_isPreSubmitted)
            {
                transform.position = _inHandPosition;    
            }
            else if (_isPreSubmitted)
            {
                transform.position = new Vector3(_inHandPosition.x, _inHandPosition.y + (DistanceToExtractCard / 2), _inHandPosition.z);
            }
        }

        private void OnMouseDown()
        {
            if (_isPreSubmitted)
            {
                _isPreSubmitted = false;
                OnPlayerCardRetracted?.Invoke(this);
            }
            else
            {
                _isPreSubmitted = true;
                OnPlayerCardSubmitted?.Invoke(this);
            }
        }
    }
}