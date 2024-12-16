using System;
using DG.Tweening;
using NaughtyAttributes;
using Settings;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UiController : MonoBehaviour
    {
        public GameSettings gameSettings;

        [BoxGroup("Money")] public TextMeshProUGUI moneyText;
        [BoxGroup("Gems")] public TextMeshProUGUI gemsText;
        [BoxGroup("Energy")] public TextMeshProUGUI energyText;

        [BoxGroup("Money")] public Image moneyImage;
        [BoxGroup("Gems")] public Image gemsImage;
        [BoxGroup("Energy")] public Image energyImage;

        [BoxGroup("Money")] public Button addMoneyButton;
        [BoxGroup("Gems")] public Button addGemsButton;
        [BoxGroup("Energy")] public Button addEnergyButton;

        public float textUpdateSpeed = 0.5f;

        private int _currentMoney;
        private int _currentGems;
        private int _currentEnergy;

        private int _targetMoney;
        private int _targetGems;
        private int _targetEnergy;

        private void Start()
        {
            Init();
        }

        private void OnEnable()
        {
            addMoneyButton.onClick.AddListener(AddMoney);
            addGemsButton.onClick.AddListener(AddGems);
            addEnergyButton.onClick.AddListener(AddEnergy);
        }

        private void OnDisable()
        {
            addMoneyButton.onClick.RemoveListener(AddMoney);
            addGemsButton.onClick.RemoveListener(AddGems);
            addEnergyButton.onClick.RemoveListener(AddEnergy);
        }

        private void Init()
        {
            _currentMoney = gameSettings.startMoney;
            _currentGems = gameSettings.startGems;
            _currentEnergy = gameSettings.startEnergy;

            SetMoneyText(_currentMoney);
            SetGemsText(_currentGems);
            SetEnergyText(_currentEnergy);

            _targetMoney = _currentMoney;
            _targetGems = _currentGems;
            _targetEnergy = _currentEnergy;
        }

        private void SetMoneyText(int money)
        {
            SetText(money, moneyText);
        }

        private void SetGemsText(int gems)
        {
            SetText(gems, gemsText);
        }

        private void SetEnergyText(int energy)
        {
            SetText(energy, energyText);
        }

        private void AnimateIcon(Image icon)
        {
            if (!DOTween.IsTweening(icon.transform))
            {
                icon.transform.DOPunchScale(Vector3.one * 0.2f, 1f, 5, 1f);
            }
        }

        private void SetText(int amount, TextMeshProUGUI text)
        {
            text.text = NumberFormatter.FormatNumber(amount);
        }

        private void UpdateTexts(float dt)
        {
            SetMoneyText(_currentMoney);
            SetGemsText(_currentGems);
            SetEnergyText(_currentEnergy);
        }

        public void AddMoney()
        {
            addMoneyButton.interactable = false;
            _targetMoney += gameSettings.addMoney;
            DOTween.To(() => _currentMoney, x => _currentMoney = x, _targetMoney, textUpdateSpeed)
                .SetEase(Ease.OutSine).OnComplete(() =>
                {
                    _currentMoney = _targetMoney;
                    addMoneyButton.interactable = true;
                });

            AnimateIcon(moneyImage);
        }

        public void AddGems()
        {
            addGemsButton.interactable = false;
            _targetGems += gameSettings.addGems;
            DOTween.To(() => _currentGems, x => _currentGems = x, _targetGems, textUpdateSpeed).SetEase(Ease.OutQuad)
                .OnComplete(
                    () =>
                    {
                        _currentGems = _targetGems;
                        addGemsButton.interactable = true;
                    });

            AnimateIcon(gemsImage);
        }

        public void AddEnergy()
        {
            addEnergyButton.interactable = false;
            _targetEnergy += gameSettings.addEnergy;
            DOTween.To(() => _currentEnergy, x => _currentEnergy = x, _targetEnergy, textUpdateSpeed)
                .SetEase(Ease.OutQuad).OnComplete(() =>
                {
                    _currentEnergy = _targetEnergy;
                    addEnergyButton.interactable = true;
                });

            AnimateIcon(energyImage);
        }

        private void Update()
        {
            UpdateTexts(Time.deltaTime);
        }
    }
}