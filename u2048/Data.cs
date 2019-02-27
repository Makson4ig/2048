using System;

namespace u2048
{
    class Data
    {
        private static Data _oData;

        private Data() { }

        public static Data GetInstance()
        {
            if (_oData != null) return _oData;
            _oData = new Data();

            return _oData;
        }

        private const int Width = 396, Height = 640;
        private static long _lTime = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;

        public int GetWidth()
        {
            return Width;
        }

        public int GetHeight()
        {
            return Height;
        }

        public long GetCurrentTime()
        {
            return DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
        }

        public long GetTime()
        {
            return _lTime;
        }

        public void SetTime(long lTime)
        {
            _lTime = lTime;
        }
    }
}
