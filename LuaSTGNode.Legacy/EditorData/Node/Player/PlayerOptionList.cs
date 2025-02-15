﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

using LuaSTGEditorSharp.EditorData.Node.NodeAttributes;
using LuaSTGEditorSharp.EditorData.Message;

namespace LuaSTGEditorSharp.EditorData.Node.Object
{
    [Serializable, NodeIcon("playeroptionlist.png")]
    [RequireAncestor(typeof(CodeAlikeTypes))]
    [LeafNode]
    //[CreateInvoke(0), RCInvoke(3)]
    public class PlayerOptionList : TreeNode
    {
        [JsonConstructor]
        private PlayerOptionList() : base() { }

        public PlayerOptionList(DocumentData workSpaceData)
            : base(workSpaceData) { }

        public override IEnumerable<string> ToLua(int spacing)
        {
            string sp = Indent(spacing);
            yield return sp + "self.slist = {\n" + sp + Indent(1) + "{nil, nil, nil, nil},\n";
            foreach (var a in base.ToLua(spacing + 1))
            {
                yield return a;
            }
            foreach (var housama in Children[Children.Count - 1].ToLua(spacing + 1)) yield return housama;
            yield return sp + "}\n";
        }

        public override string ToString()
        {
            return "Create player option list";
        }

        public override object Clone()
        {
            var n = new PlayerOptionList(parentWorkSpace);
            n.DeepCopyFrom(this);
            return n;
        }

        public override IEnumerable<Tuple<int, TreeNode>> GetLines()
        {
            yield return new Tuple<int, TreeNode>(2, this);
        }
    }
}
