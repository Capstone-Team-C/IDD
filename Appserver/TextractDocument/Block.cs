using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Appserver.TextractDocument
{
    public abstract class Block
    {
        public abstract Appserver.TextractDocument.BlockType GetBlockType();
        public abstract Geometry GetGeometry();
        public abstract string GetId();
        public abstract List<Block> GetRelationships();
        public abstract int GetPage();
    }
}
