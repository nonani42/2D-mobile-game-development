using System.Collections.Generic;

namespace BattleScripts
{
    internal class PlayerData
    {
        private readonly List<IPlayerDataObserver> _investors;
        private int _value;

        public DataType DataType { get; }

        public int Value
        {
            get => _value;
            set => SetValue(value);
        }

        public PlayerData(DataType dataType)
        {
            DataType = dataType;
            _investors = new List<IPlayerDataObserver>();
        }

        public void Attach(IPlayerDataObserver investor) => _investors.Add(investor);
        public void Detach(IPlayerDataObserver investor) => _investors.Remove(investor);

        protected void Notify()
        {
            foreach (var investor in _investors)
                investor.Update(this);
        }

        private void SetValue(int value)
        {
            if (_value == value)
                return;

            _value = value;
            Notify();
        }
    }
}