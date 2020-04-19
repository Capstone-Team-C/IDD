using Amazon.SimpleWorkflow;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Appserver.TextractDocument
{
    public class Table : Block
    { 
        public Table(JToken block)
        {
            _geometry = new Geometry(block["Geometry"]);
            _Id = block["Id"].ToString();
            Confidence = block["Confidence"].ToObject<float>();
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
            => Appserver.TextractDocument.BlockType.TABLE;
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

        private List<Block> _children = new List<Block>();
        private Dictionary<string, Block> _childMap = new Dictionary<string, Block>();
        private List<string> _childIds = new List<string>();
        private Page _parent;
        public override void SetPage(Page page)
        {
            _parent = page;
        }
        public override void CreateStructure()
        {

            if (_parent == null)
            {
                throw new System.ArgumentException("No Parent");
            }
            foreach (var child in _childIds)
            {
                _childMap[child] = _parent.GetChildById(child);
            }
        }
        public override void PrintSummary()
        {
            Console.WriteLine(String.Format("Table-id: {0}", _Id));
            Console.WriteLine(String.Format("Child count: {0}", _childIds.Count));
            foreach ( var child in _childMap.Values)
            {
                Console.WriteLine(String.Format("Child-type: {0}",child.GetBlockType()));
            }
            Console.WriteLine(String.Format("Child count: {0}", _childMap.Count));
        }
    }
}
