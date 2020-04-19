using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Appserver.TextractDocument
{
    public class Cell: Block
    {
        public Cell(JToken block)
        {
            _geometry = new Geometry(block["Geometry"]);
            _Id = block["Id"].ToString();
            Confidence = block["Confidence"].ToObject<float>();
            RowIndex = block["RowIndex"].ToObject<int>();
            ColumnIndex = block["ColumnIndex"].ToObject<int>();
            RowSpan = block["RowSpan"].ToObject<int>();
            ColumnSpan = block["ColumnSpan"].ToObject<int>();
            try
            {
                var children = block["Relationships"].ToList<JToken>()[0]["Ids"].ToList<JToken>();

                foreach (var child in children)
                {
                    _childIds.Add(child.ToString());
                }
            }
            catch (System.ArgumentNullException e)
            {

            }
        }
        public override Appserver.TextractDocument.BlockType GetBlockType() 
            => Appserver.TextractDocument.BlockType.CELL;
        public override Geometry GetGeometry() => _geometry;
        public override string GetId() => _Id;
        public override List<Block> GetRelationships() => _children;
        public override int GetPage() => _page;

        ////////////////////////
        /// Properties of a Cell
        ////////////////////////
        ///

        float Confidence;
        private int RowIndex;
        private int ColumnIndex;
        private int RowSpan;
        private int ColumnSpan;

        private Geometry _geometry;
        private int _page;
        private string _Id;

        private List<Block> _children = new List<Block>();
        private Dictionary<string, Block> _childMap = new Dictionary<string, Block>();
        private List<string> _childIds = new List<string>();
    }
}
