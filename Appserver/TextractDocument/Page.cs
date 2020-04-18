using Amazon.Textract.Model;
using Newtonsoft.Json;
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
        public override string GetBlockType() => "PAGE";
        public override Geometry GetGeometry()
        {
            return geometry;
        }

        ///////////////////////////////////////////////
        /// Properties
        ///////////////////////////////////////////////
        private List<Block> _blocks;
        private Geometry geometry;

        public Page()
        {
            _blocks = new List<Block>();
        }
        public void addBlocks(List<Block> blocks)
        {
            _blocks.AddRange(blocks);
        }

    }
}
