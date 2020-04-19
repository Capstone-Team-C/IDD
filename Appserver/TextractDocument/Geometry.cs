using Amazon.ElasticTranscoder.Model;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Appserver.TextractDocument
{
    public class Geometry
    {
        public Geometry(JToken block)
        {
            box = new BoundingBox(block["BoundingBox"]);
            //polygon = new Polygon(block["Pologyon"]);
            var poly = block["Polygon"].ToList();

            polygon = new Polygon {
                bottomLeft = new Coordinate(poly[0]),
                bottomRight = new Coordinate(poly[1]),
                topRight = new Coordinate(poly[2]),
                topLeft = new Coordinate(poly[3])
            };

        }
        public BoundingBox box;
        public Polygon polygon;
    }
}
