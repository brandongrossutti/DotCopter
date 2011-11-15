using System;
using Microsoft.SPOT;

namespace DotCopter.Commons.Utilities
{
    public class RunningAverage
    {
        private float _acculmulator;
        private float[] _buffer;
        private int _bufferSize;
        private int _back;
        private int _front;

        public RunningAverage(int size)
        {
            _buffer = new float[size];
            _bufferSize = size;
            _front = 0;
            _back = 1;
        }

        public float Average(float value)
        {
            _buffer[_front] = value;
            _acculmulator += _buffer[_front] - _buffer[_back];
            _front = _front == _bufferSize - 1 ? 0 : _front + 1;
            _back = _back == _bufferSize - 1 ? 0 : _back + 1;
            return _acculmulator/_bufferSize;
        }
    }
}
