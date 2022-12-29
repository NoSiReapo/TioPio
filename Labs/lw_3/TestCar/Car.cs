using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lw_3.TestCar
{
    internal class Car
    {
        public enum Direction
        { back, standing, forward };

        public Car()
        {
            m_statusEngine = false;
            m_direction = Direction.standing;
            m_speed = 0;
            m_gear = 0;
        }

        public bool IsTurnedOn() => m_statusEngine;

        public Direction GetDirection() => m_direction;

        public int GetSpeed() => m_speed;

        public int GetGear() => m_gear;

        public bool TurnOnEngine()
        {
            if (IsTurnedOn())
                return false;
            m_statusEngine = true;
            return true;
        }

        public bool TurnOffEngine()
        {
            if (!IsTurnedOn())
                return false;
            if ((m_gear != 0) || (m_speed != 0))
                return false;
            m_statusEngine = false;
            return true;
        }

        public bool SetGear(int gear)
        {
            if (!IsSetGear(gear) || !IsSetDirection(gear))
                return false;
            m_gear = gear;
            SetDirection();
            return true;
        }

        public bool SetSpeed(int speed)
        {
            if (speed < 0 || !IsTurnedOn() || !IsSetSpeed(speed))
                return false;
            m_speed = speed;
            SetDirection();
            return true;
        }

        private bool IsSetDirection(int gear)
        {
            switch (gear)
            {
                case -1:
                    return ((m_direction == Direction.back) || (m_direction == Direction.standing));

                case 0:
                    return true;

                case 1:
                    return ((m_direction == Direction.standing) || (m_direction == Direction.forward));
            }
            for (int i = 2; i < 6; i++)
                if (gear == i)
                    return m_direction == Direction.forward;

            return false;
        }

        private void SetDirection()
        {
            if ((m_gear == -1) && (m_speed > 0))
                m_direction = Direction.back;
            else if ((m_gear > 0) && (m_speed > 0))
                m_direction = Direction.forward;
            else if (m_speed == 0)
                m_direction = Direction.standing;
        }

        private bool IsSetGear(int gear)
        {
            switch (gear)
            {
                case -1:
                    return (m_speed == Limit[0, 0] && IsTurnedOn());

                case 0:
                    return true;
            }
            for (int i = 1; i < 6; i++)
                if (i == gear)
                    return ((m_speed >= Limit[i, 0]) && (m_speed <= Limit[i, 1]) && IsTurnedOn());

            return false;
        }

        private bool IsSetSpeed(int speed)
        {
            switch (m_gear)
            {
                case -1:
                    return ((speed >= Limit[0, 0]) && (speed <= Limit[0, 1]));

                case 0:
                    return speed <= m_speed;
            }
            for (int i = 1; i < 6; i++)
                if (i == m_gear)
                    return ((speed >= Limit[i, 0]) && (speed <= Limit[i, 1]));

            return false;
        }

        private int[,] Limit =
            {
                {0, 20},
                {0, 30},
                {20, 50},
                {30, 60},
                {30, 90},
                {50, 120}
            };

        private bool m_statusEngine;
        private Direction m_direction;
        private int m_speed;
        private int m_gear;
    }
}