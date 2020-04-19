using Amazon.Textract.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Appserver.TextractDocument
{
    public enum BlockType
    {
        PAGE,
        LINE,
        WORD,
        TABLE,
        CELL,
        KEY_VALUE_SET,
        SELECTION_ELEMENT
    }
    public class TextractDocument : AbstractTextractObject
    {
        public DocumentMetadata DocumentMetadata;
        public string JobStatus;
        public List<Page> Pages = new List<Page>();

        private Dictionary<string, Block> _blockMap;
        public TextractDocument()
        {

        }
        public override void FromJson(JToken token)
        {
            // Start reading
            Console.WriteLine(String.Format("Token: {0}, Type: {1}",token.First().ToString(),token.First().Type));

            Console.WriteLine(String.Format("Pages: {0}", token["DocumentMetadata"]["Pages"]));
            JToken blocks;
            try
            {
                this.DocumentMetadata = new DocumentMetadata
                {
                    Pages = token["DocumentMetadata"]["Pages"].ToObject<int>()
                };
                this.JobStatus = token["JobStatus"].ToString();

                blocks = token["Blocks"];
            }
            catch(Exception e)
            {
                Console.WriteLine(String.Format("Error: Invalid Textract JSON. {0}",e.Message));
                throw;
            }

            Page currentPage = null;
            foreach( var b in blocks.Children())
            {
                Block block = null;
                switch (b["BlockType"].ToString())
                {
                    case "PAGE":
                        block = new Page(b);
                        break;
                    case "LINE":
                        block = new Line(b);
                        break;
                    case "WORD":
                        block = new Word(b);
                        break;
                    case "TABLE":
                        block = new Table(b);
                        break;
                    case "CELL":
                        block = new Cell(b);
                        break;
                    case "KEY_VALUE_SET":
                        block = new KeyValueSet(b);
                        break;
                    case "SELECTION_ELEMENT":
                        block = new SelectionElement(b);
                        break;
                    default:
                        Console.WriteLine(String.Format("Child Token: {0}", b.ToString()));
                        throw new System.ArgumentException(String.Format("Unknown block type: {0}", b["BlockType"].ToString()));
                }

                switch ( block.GetBlockType() )
                {
                    case Appserver.TextractDocument.BlockType.PAGE:
                        currentPage = (Page)block;
                        Pages.Add(currentPage);
                        break;
                    default:
                        currentPage.addBlock(block);
                        break;
                }
                
            }
        }

        public int PageCount() => Pages.Count();
        public void printSummary()
        {
            foreach( var p in Pages)
            {
                Console.WriteLine("Page: {0}", p.GetPage());
                Console.WriteLine("Page Id: {0}", p.GetId());
                foreach ( var b in p.GetRelationships())
                {
                    Console.WriteLine("Child-id: {0}", b.GetId());
                }
            }
        }
    }
}
