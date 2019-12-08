using System.Collections.Generic;
using System.IO;

namespace AdventOfCode2019.DayEight
{
    public class ImageDecoder
    {
        private readonly string _encrypted;
        private readonly int _width;
        private readonly int _height;

        public ImageDecoder(string encrypted, int width, int height)
        {
            _encrypted = encrypted;
            _width = width;
            _height = height;
        }

        public string Decode()
        {
            var layerSize = _width * _height;
            var message = new List<char>();

            for (var j = 0; j < layerSize; j++)
            {
                for (var i = 0; i < _encrypted.Length; i += layerSize)
                {
                    if (_encrypted[i + j] == '2') continue;
                    message.Add(_encrypted[i + j]);
                    break;
                }
            }

            return new string(message.ToArray());
        }

        public void WriteMessage()
        {
            var message = Decode();

            for (int i = 0; i < message.Length; i += _width)
            {
                File.AppendAllLines("output", new[] { message.Substring(i, _width).Replace('0',' ').Replace('1', '@') });
            }
        }
    }
}