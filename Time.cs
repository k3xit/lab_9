using System;

namespace lab_9
{
    public class Time
    {
        private byte _hours;
        private byte _minutes;

        public Time()
        {
            _hours = 0;
            _minutes = 0;
        }

        public Time(byte hours, byte minutes)
        {
            Hours = hours;
            Minutes = minutes;
        }

        public Time(uint totalMinutes)
        {
            _hours = (byte)((totalMinutes / 60) % 24);
            _minutes = (byte)(totalMinutes % 60);
        }

        public Time(Time other)
        {
            if (other != null)
            {
                _hours = other._hours;
                _minutes = other._minutes;
            }
        }

        public byte Hours
        {
            get { return _hours; }
            set { _hours = (byte)(value % 24); }
        }

        public byte Minutes
        {
            get { return _minutes; }
            set
            {
                if (value < 60)
                {
                    _minutes = value;
                }
                else
                {
                    _hours = (byte)((_hours + value / 60) % 24);
                    _minutes = (byte)(value % 60);
                }
            }
        }

        public Time AddMinutes(uint mins)
        {
            uint total = (uint)(_hours * 60 + _minutes + mins);
            return new Time(total);
        }

        public static Time operator ++(Time t)
        {
            if (t == null) return new Time();
            return t.AddMinutes(1);
        }

        public static Time operator --(Time t)
        {
            if (t == null) return new Time();
            int total = t._hours * 60 + t._minutes - 1;
            while (total < 0) total += 24 * 60;
            return new Time((uint)total);
        }

        public static explicit operator byte(Time t) => t?._hours ?? 0;

        public static implicit operator bool(Time t)
        {
            if (t == null) return false;
            return t._hours != 0 || t._minutes != 0;
        }

        public static Time operator +(Time t, uint mins) => t?.AddMinutes(mins) ?? new Time(mins);
        public static Time operator +(uint mins, Time t) => t?.AddMinutes(mins) ?? new Time(mins);

        public static Time operator -(Time t, uint mins)
        {
            if (t == null) return new Time();
            int total = t._hours * 60 + t._minutes - (int)mins;
            while (total < 0) total += 24 * 60;
            return new Time((uint)total);
        }

        public static Time operator -(uint mins, Time t)
        {
            if (t == null) return new Time();
            int total = (int)mins - (t._hours * 60 + t._minutes);
            while (total < 0) total += 24 * 60;
            return new Time((uint)total);
        }

        public override string ToString()
        {
            return $"{_hours:D2}:{_minutes:D2}";
        }
    }
}