using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Appserver.TextractDocument
{
    public abstract class Block
    {
        public abstract string GetBlockType();
        public abstract Geometry GetGeometry();
        public string Id;
        public List<Block> Relationships;
        public int Page;
    }
}
