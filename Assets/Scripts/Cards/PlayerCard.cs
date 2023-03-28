using System;
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

        public Player player;
        private Vector3 _inHandPosition;
        public HandManager hand;
        private bool _isPreSubmitted;
        private const float DistanceToExtractCard = 5.3f;
        private bool _hasBeenTouched;
        public int baseCost;
        public AudioSource cardChooseAudioSource;
        public AudioSource cardHoverAudioSource;

        private void Start()
        {
            player = GameObject.FindWithTag("Player").GetComponent<Player>();
        }
        
        private void OnEnable()
        {
            RoundManager.OnRoundEnd += ResetCard;
        }

        private void OnDisable()
        {
            RoundManager.OnRoundEnd -= ResetCard;
        }

        public override void Play()
        {
            base.Play();
            player.TakeDamage(baseCost, false);
        }

        private void OnMouseEnter()
        {
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