using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class AttackButtonView : MonoBehaviour
    {
        public event Action OnDefaultAttackButtonClick;
        public event Action OnHeavyAttackButtonClick;
    
        [SerializeField] private Button _heavyAttackButton;
        [SerializeField] private Button _defaultAttackButton;
        [SerializeField] private Image _coolDownFon;

        private void OnEnable()
        {
            _defaultAttackButton.onClick.AddListener(() => OnDefaultAttackButtonClick?.Invoke());
            _heavyAttackButton.onClick.AddListener(() => OnHeavyAttackButtonClick?.Invoke());
        }
    
        public void DisableAllButtons()
        {
            _defaultAttackButton.gameObject.SetActive(false); 
            _heavyAttackButton.gameObject.SetActive(false);
        }

        public void UpdateButtonState(bool value)
        {
            _heavyAttackButton.interactable = value;
        }

        public void OnCoolDown(bool value, float cooldown)
        {
            if (!value)
            {
                _heavyAttackButton.interactable = false;
                StartCoroutine(UpdateCooldownView(cooldown));
            }
            else _heavyAttackButton.interactable = true;
        }

        private IEnumerator UpdateCooldownView(float cooldown)
        {
            _heavyAttackButton.interactable = false;
            _coolDownFon.fillAmount = 0f;
            
            while (_coolDownFon.fillAmount < 1f)
            {
                _coolDownFon.fillAmount += Time.deltaTime / cooldown;
                yield return null;
            }
        }

        private void OnDisable()
        {
            _heavyAttackButton.onClick.RemoveAllListeners();
            _defaultAttackButton.onClick.RemoveAllListeners();
        }
    }
}