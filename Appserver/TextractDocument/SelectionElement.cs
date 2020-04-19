﻿using System;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Appserver.TextractDocument
{
    public class SelectionElement: Block
    {
        public SelectionElement(JToken block)
        {
            _geometry = new Geometry(block["Geometry"]);
            _Id = block["Id"].ToString();
            Confidence = block["Confidence"].ToObject<float>();

            switch(block["SelectionStatus"].ToString())
            {
                case "NOT_SELECTED":
                    SelectionStatus = false;
                    break;
                case "SELECTED":
                    SelectionStatus = true;
                    break;
            }
            
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
            => Appserver.TextractDocument.BlockType.SELECTION_ELEMENT;
        public override Geometry GetGeometry() => _geometry;
        public override string GetId() => _Id;
        public override List<Block> GetRelationships() => _children;
        public override int GetPage() => _page;

        ////////////////////////
        /// Properties of a Word
        ////////////////////////
        ///

        public float Confidence;
        public bool SelectionStatus;


        private Geometry _geometry;
        private int _page;
        private string _Id;

        private List<Block> _children = new List<Block>();
        private Dictionary<string, Block> _childMap = new Dictionary<string, Block>();
        private List<string> _childIds = new List<string>();

    }
}
