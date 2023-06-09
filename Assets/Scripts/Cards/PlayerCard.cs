﻿using System;
using GameLogic;
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
        private const float DistanceToExtractCard = 5.3f;
        private bool _hasBeenTouched;
        public int baseCost;
        public AudioSource cardChooseAudioSource;
        public AudioSource cardHoverAudioSource;
        public bool isDisabled;

        private void OnEnable()
        {
            RoundManager.OnRoundEnd += ResetCard;
        }

        private void OnDisable()
        {
            RoundManager.OnRoundEnd -= ResetCard;
        }

        private void OnMouseEnter()
        {
            if (isDisabled) return;
            
            if (!_hasBeenTouched)
            {
                _inHandPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
                _hasBeenTouched = true;
            }
            
            transform.position = new Vector3(_inHandPosition.x, _inHandPosition.y + DistanceToExtractCard, _inHandPosition.z);
            
            cardHoverAudioSource.Play();
        }

        private void OnMouseExit()
        {
            if (isDisabled) return;
            
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
            if (isDisabled) return;
            cardChooseAudioSource.Play();
            
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
        
        private void ResetCard()
        {
            _hasBeenTouched = false;
        }
    }
}