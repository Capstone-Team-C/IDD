using Amazon.Textract.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Appserver.TextractDocument
{
    public class Page: Block
    {
        ///////////////////////////////////////////////
        // Inherited Members
        ///////////////////////////////////////////////
        public override Appserver.TextractDocument.BlockType GetBlockType() 
            => Appserver.TextractDocument.BlockType.PAGE;
        public override Geometry GetGeometry() => _geometry;
        public override int GetPage() => _page;
        public override string GetId() => _Id;
        public override List<Block> GetRelationships() => _children;

        ///////////////////////////////////////////////
        /// Properties
        ///////////////////////////////////////////////
        private List<Block> _children = new List<Block>();
        private Dictionary<string, Block> _childMap = new Dictionary<string, Block>();
        private readonly Geometry _geometry;
        private int _page;
        private string _Id;

        public Page(Geometry geometry, string Id, int page) => (_geometry, _Id, _page) = (geometry, Id, page);
        public Page(JToken block)
        {
            this._geometry = new Geometry(block["Geometry"]);
            this._Id = block["Id"].ToString();
            this._page = block["Page"].ToObject<int>();
        }
        
        public void addBlocks(List<Block> blocks)
        {
            foreach( var b in blocks)
            {
                addBlock(b);
            }
        }
        public void addBlock(Block block)
        {
            _children.Add(block);
            _childMap.Add(block.GetId(), block);
        }
    }
}
