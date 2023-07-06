using System.Collections.Generic;

namespace Features.Battle
{
    internal class PlayerData
    {
        private readonly List<IEnemy> _investors;
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
            _investors = new List<IEnemy>();
        }

        public void Attach(IEnemy investor) => _investors.Add(investor);
        public void Detach(IEnemy investor) => _investors.Remove(investor);

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