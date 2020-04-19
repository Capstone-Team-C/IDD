﻿using Amazon.SimpleWorkflow;
using Microsoft.WindowsAzure.Storage.Blob;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Appserver.TextractDocument
{
    public class Line: Block
    {
        public Line(JToken block)
        {
            _geometry = new Geometry(block["Geometry"]);
            _Id = block["Id"].ToString();
            Confidence = block["Confidence"].ToObject<float>();
            Text = block["Text"].ToString();
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
            => Appserver.TextractDocument.BlockType.LINE;
        public override Geometry GetGeometry() => _geometry;
        public override string GetId() => _Id;
        public override List<Block> GetRelationships() => _children;
        public override int GetPage() => _page;

        ////////////////////////
        /// Properties of a Line
        ////////////////////////
        ///

        float Confidence;
        string Text;

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
            if( _parent == null)
            {
                throw new System.ArgumentException("No Parent");
            }
            foreach( var child in _childIds)
            {
                _childMap[child] = _parent.GetChildById(child);
            }
        }
        public override void PrintSummary()
        {
            Console.WriteLine(String.Format("Line-id: {0}", _Id));
            foreach (var child in _childMap.Values)
            {
                Console.WriteLine(String.Format("Child-type: {0}", child.GetBlockType()));
            }
            Console.WriteLine(String.Format("Child count: {0}", _childMap.Count));
        }
    }
}
