using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Appserver.TextractDocument
{
    public class Coordinate
    {
        public Coordinate(JToken block) => (X, Y) = (block["X"].ToObject<float>(), block["Y"].ToObject<float>());
        public float X;
        public float Y;
    }
}
