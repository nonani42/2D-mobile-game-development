using UnityEngine;

namespace CarGame
{
    public class KeyboardInputView : BaseInputView
    {

        protected override void Move()
        {
            float axisOffset = Input.GetAxis("Horizontal");
            float moveValue = _speed * Time.deltaTime * axisOffset;

            float abs = Mathf.Abs(moveValue);
            float sign = Mathf.Sign(moveValue);

            if (sign > 0)
                OnRightMove(abs);
            else if (sign < 0)
                OnLeftMove(abs);
        }
    }
}
