// Accord Machine Learning Library
// The Accord.NET Framework
// http://accord.googlecode.com
//
// Copyright © César Souza, 2009-2012
// cesarsouza at gmail.com
//
//    This library is free software; you can redistribute it and/or
//    modify it under the terms of the GNU Lesser General Public
//    License as published by the Free Software Foundation; either
//    version 2.1 of the License, or (at your option) any later version.
//
//    This library is distributed in the hope that it will be useful,
//    but WITHOUT ANY WARRANTY; without even the implied warranty of
//    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
//    Lesser General Public License for more details.
//
//    You should have received a copy of the GNU Lesser General Public
//    License along with this library; if not, write to the Free Software
//    Foundation, Inc., 51 Franklin St, Fifth Floor, Boston, MA  02110-1301  USA
//

namespace Accord.MachineLearning.DecisionTrees
{
    using System;
    using System.Collections.ObjectModel;


    /// <summary>
    ///   Decision Tree (DT) Node.
    /// </summary>
    /// 
    /// <remarks>
    ///   Each node of a decision tree can play two roles. When a node is not a leaf, it
    ///   contains a <see cref="DecisionBranchNodeCollection"/> with a collection of child nodes. The
    ///   branch specifies an attribute index, indicating which column from the data set
    ///   (the attribute) should be compared against its children values. The type of the
    ///   comparison is specified by each of the children. When a node is a leaf, it will
    ///   contain the output value which should be decided for when the node is reached.
    /// </remarks>
    /// 
    /// <seealso cref="DecisionTree"/>
    /// 
    [Serializable]
    public class DecisionNode
    {
        /// <summary>
        ///   Gets or sets the value this node responds to
        ///   whenever this node acts as a child node. This
        ///   value is set only when the node has a parent.
        /// </summary>
        /// 
        public double? Value { get; set; }

        /// <summary>
        ///   Gets or sets the type of the comparison which
        ///   should be done against <see cref="Value"/>.
        /// </summary>
        /// 
        public ComparisonKind Comparison { get; set; }

        /// <summary>
        ///   If this is a leaf node, gets or sets the output
        ///   value to be decided when this node is reached.
        /// </summary>
        /// 
        public int? Output { get; set; }

        /// <summary>
        ///   If this is not a leaf node, gets or sets the collection
        ///   of child nodes for this node, together with the attribute
        ///   determining the reasoning process for those children.
        /// </summary>
        /// 
        public DecisionBranchNodeCollection Branches { get; set; }

        /// <summary>
        ///   Gets or sets the parent of this node. If this is a root
        ///   node, the parent is <c>null</c>.
        /// </summary>
        /// 
        public DecisionNode Parent { get; set; }

        /// <summary>
        ///   Gets the <see cref="DecisionTree"/> containing this node.
        /// </summary>
        /// 
        public DecisionTree Owner { get; private set; }

        /// <summary>
        ///   Creates a new decision node.
        /// </summary>
        /// 
        /// <param name="owner">The owner tree for this node.</param>
        /// 
        public DecisionNode(DecisionTree owner)
        {
            Owner = owner;
            Comparison = ComparisonKind.None;
        }

        /// <summary>
        ///   Gets a value indicating whether this instance is a root node (has no parent).
        /// </summary>
        /// 
        /// <value><c>true</c> if this instance is a root; otherwise, <c>false</c>.</value>
        /// 
        public bool IsRoot
        {
            get { return Parent == null; }
        }

        /// <summary>
        ///   Gets a value indicating whether this instance is a leaf (has no children).
        /// </summary>
        /// 
        /// <value><c>true</c> if this instance is a leaf; otherwise, <c>false</c>.</value>
        /// 
        public bool IsLeaf
        {
            get { return Branches == null || Branches.Count == 0; }
        }

        /// <summary>
        ///   Computes whether a value satisfies
        ///   the condition imposed by this node.
        /// </summary>
        /// 
        /// <param name="x">The value x.</param>
        /// 
        /// <returns><c>true</c> if the value satisfies this node's
        /// condition; otherwise, <c>false</c>.</returns>
        /// 
        public bool Compute(double x)
        {
            switch (Comparison)
            {
                case ComparisonKind.Equal:
                    return (x == Value);

                case ComparisonKind.GreaterThan:
                    return (x > Value);

                case ComparisonKind.GreaterThanOrEqual:
                    return (x >= Value);

                case ComparisonKind.LessThan:
                    return (x < Value);

                case ComparisonKind.LessThanOrEqual:
                    return (x <= Value);

                case ComparisonKind.NotEqual:
                    return (x != Value);

                default:
                    throw new InvalidOperationException();
            }
        }

        /// <summary>
        ///   Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// 
        /// <returns>
        ///   A <see cref="System.String"/> that represents this instance.
        /// </returns>
        /// 
        public override string ToString()
        {
            if (IsRoot)
                return "Root";

            String name = Owner.Attributes[Parent.Branches.AttributeIndex].Name;

            if (String.IsNullOrEmpty(name))
                name = "x" + Parent.Branches.AttributeIndex;

            String op;

            switch (Comparison)
            {
                case ComparisonKind.Equal:
                    op = "=="; break;

                case ComparisonKind.GreaterThan:
                    op = ">"; break;

                case ComparisonKind.GreaterThanOrEqual:
                    op = ">="; break;

                case ComparisonKind.LessThan:
                    op = "<"; break;

                case ComparisonKind.LessThanOrEqual:
                    op = "<="; break;

                case ComparisonKind.NotEqual:
                    op = "!="; break;

                default:
                    return "Unexpected comparison type.";
            }

            String value = Value.ToString();

            return String.Format("{0} {1} {2}", name, op, value);
        }
    }


    /// <summary>
    ///   Collection of decision nodes. A decision branch specifies the index of
    ///   an attribute whose current value should be compared against its children
    ///   nodes. The type of the comparison is specified in each child node.
    /// </summary>
    /// 
    [Serializable]
    public class DecisionBranchNodeCollection : ReadOnlyCollection<DecisionNode>
    {
        /// <summary>
        ///   Gets or sets the index of the attribute to be
        ///   used in this stage of the decisioning process.
        /// </summary>
        /// 
        public int AttributeIndex { get; set; }

        /// <summary>
        ///   Initializes a new instance of the <see cref="DecisionBranchNodeCollection"/> class.
        /// </summary>
        /// 
        /// <param name="attributeIndex">Index of the attribute to be processed.</param>
        /// 
        /// <param name="nodes">The children nodes. Each child node should be
        /// responsable for a possible value of a discrete attribute, or for
        /// a region of a continuous-valued attribute.</param>
        /// 
        public DecisionBranchNodeCollection(int attributeIndex, DecisionNode[] nodes)
            : base(nodes)
        {
            if (nodes == null)
                throw new ArgumentNullException("nodes");
            if (nodes.Length == 0)
                throw new ArgumentException("Node collection is empty.", "nodes");

            this.AttributeIndex = attributeIndex;
        }
    }

    /// <summary>
    ///   Numeric comparison category.
    /// </summary>
    /// 
    public enum ComparisonKind
    {
        /// <summary>
        ///   The node does no comparison.
        /// </summary>
        /// 
        None,

        /// <summary>
        ///   The node compares for equality.
        /// </summary>
        /// 
        Equal,

        /// <summary>
        ///   The node compares for non-equality.
        /// </summary>
        /// 
        NotEqual,

        /// <summary>
        ///   The node compares for greater-than or equality.
        /// </summary>
        GreaterThanOrEqual,

        /// <summary>
        ///   The node compares for greater-than.
        /// </summary>
        /// 
        GreaterThan,

        /// <summary>
        ///   The node compares for less-than.
        /// </summary>
        /// 
        LessThan,

        /// <summary>
        ///   The node compares for less-than or equality.
        /// </summary>
        LessThanOrEqual
    }

}
