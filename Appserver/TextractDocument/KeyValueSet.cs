using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Appserver.TextractDocument
{
    public class KeyValueSet: Block
    {
        public enum EntityType
        {
            KEY,
            VALUE
        }
        public KeyValueSet(JToken block)
        {
            _geometry = new Geometry(block["Geometry"]);
            _Id = block["Id"].ToString();
            Confidence = block["Confidence"].ToObject<float>();
            try
            {
                var relationships = block["Relationships"].ToList<JToken>();

                foreach( var r in relationships)
                {
                    if (r["Type"].ToString() == "CHILD")
                    {
                        foreach (var child in r["Ids"].ToList<JToken>())
                        {
                            _childIds.Add(child.ToString());
                        }
                    }
                    else if( r["Type"].ToString() == "VALUE")
                    {

                        foreach (var child in r["Ids"].ToList<JToken>())
                        {
                            _valueIds.Add(child.ToString());
                        }
                    }
                }
            }
            catch (System.ArgumentNullException e)
            {

            }
            var type = block["EntityTypes"].ToList<JToken>()[0].ToObject<string>();
            switch( type)
            {
                case "KEY":
                    _entityType = EntityType.KEY;
                    break;
                case "VALUE":
                    _entityType = EntityType.VALUE;
                    break;
            }
        }
        public override Appserver.TextractDocument.BlockType GetBlockType()
            => Appserver.TextractDocument.BlockType.SELECTION_ELEMENT;
        public override Geometry GetGeometry() => _geometry;
        public override string GetId() => _Id;
        public override List<Block> GetRelationships() => _children;
        public override int GetPage() => _page;

        ////////////////////////
        /// Properties of a Table
        ////////////////////////
        ///

        float Confidence;

        private Geometry _geometry;
        private int _page;
        private string _Id;
        private EntityType _entityType;

        private List<Block> _children = new List<Block>();
        private Dictionary<string, Block> _childMap = new Dictionary<string, Block>();
        private List<string> _valueIds = new List<string>();
        private List<string> _childIds = new List<string>();
        private Page _parent;
        public override void SetPage(Page page)
        {
            _parent = page;
        }
        public override void CreateStructure()
        {
            throw new NotImplementedException();
        }
        public override void PrintSummary()
        {
            throw new NotImplementedException();
        }
    }
}
