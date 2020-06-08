﻿using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Appserver.TextractDocument
{
    public class Line: Block
    {
        /*******************************************************************************
        /// Fields
        *******************************************************************************/
        private float Confidence;
        private string Text;

        private Geometry _geometry;
        private int _page;
        private string _Id;

        // Here the children represent individual words listed in document order
        private List<Block> _children = new List<Block>();

        // Quick lookup of children by id
        private Dictionary<string, Block> _childMap = new Dictionary<string, Block>();

        // List of child ids used in building the structure once everything has been parsed
        private List<string> _childIds = new List<string>();

        private Page _parent;
        /*******************************************************************************
        /// Constructors 
        *******************************************************************************/
        /// <summary>
        /// The Line constructor is constructed from a JToken object that is from an AWS
        /// Textract Response
        /// </summary>
        /// <param name="block"></param>
        public Line(JToken block)
        {
            _geometry = new Geometry(block["Geometry"]);
            _Id = block["Id"].ToString();
            Confidence = block["Confidence"].ToObject<float>();
            _page = block["Page"].ToObject<int>();
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

        /*******************************************************************************
        /// Properties
        *******************************************************************************/
        public override BlockType GetBlockType() 
            => BlockType.LINE;
        public override Geometry GetGeometry() => _geometry;
        public override string GetId() => _Id;
        public override List<Block> GetRelationships() => _children;
        public override int GetPage() => _page;
        public override float GetConfidence() => Confidence;

        public override void SetPage(Page page)
        {
            _parent = page;
        }

        /*******************************************************************************
        /// Methods
        *******************************************************************************/
        protected override void CreateStructure()
        {
            if( _parent == null)
            {
                throw new ArgumentException("No Parent");
            }
            foreach( var child in _childIds)
            {
                var b = _parent.GetChildById(child);
                _childMap[child] = b;
                _children.Add(b);
            }
        }
        public override void PrintSummary()
        {
            Console.WriteLine(string.Format("Line-id: {0}", _Id));
            foreach (var child in _childMap.Values)
            {
                Console.WriteLine(string.Format("Child-type: {0}", child.GetBlockType()));
            }
            Console.WriteLine(string.Format("Child count: {0}", _childMap.Count));
        }

        // Just returning the text, no need to reconstruct from children
        public override string ToString()
        {
            return Text;
        }
    }
}
