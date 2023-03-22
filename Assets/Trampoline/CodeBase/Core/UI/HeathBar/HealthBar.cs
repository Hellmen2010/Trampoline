using Trampoline.CodeBase.Services.Factories.UIFactory;
using Trampoline.CodeBase.Services.StaticData;
using UnityEngine;

namespace Trampoline.CodeBase.Core.UI.HeathBar
{
    public class HealthBar
    {
        private readonly Sprite _healthOn;
        private readonly Sprite _healthOff;
        private readonly HealthBarView _view;
        private readonly int _maxHealth;
        public int CurrentHealth { get; private set; }

        public HealthBar(HealthBarView view, IStaticData staticData)
        {
            _maxHealth = staticData.GameConfig.MaxHealth;
            _healthOn = staticData.SpritesData.OnHealth;
            _healthOff = staticData.SpritesData.OffHealth;
            _view = view;
        }

        public int DecreaseHealth()
        {
            if (CurrentHealth <= 0) return 0;
            CurrentHealth--;
            _view.SetHealthSprite(_healthOff, CurrentHealth);
            return CurrentHealth;
        }

        public void ResetHealth()
        {
            CurrentHealth = _maxHealth;
            for (int i = 0; i < CurrentHealth; i++) 
                _view.SetHealthSprite(_healthOn, i);
        }
    }
}