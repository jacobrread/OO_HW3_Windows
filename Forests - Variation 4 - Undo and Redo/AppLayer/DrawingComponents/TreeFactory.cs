using System;
using System.Collections.Generic;
using System.Linq;

namespace AppLayer.DrawingComponents
{
    /// <summary>
    /// Tree TreeFactory
    /// 
    /// To use this factory, simply create an instance and then call LoadTreeTypes.  Then, use that instance
    /// of the factory to create new creates by calling the CreateTree method.
    /// 
    /// This class is serve as the factory in an instance of the Flyweight pattern.
    /// 
    /// This class applies a variation of the TreeFactory Method pattern.  It can be extended by specializing
    /// and overriding the LoadTreeTypes method.  In most cases, the code for the LoadTreeTypes method would
    /// include a call to the base.LoadTreeTypes().
    /// 
    /// </summary>
    public class TreeFactory
    {
        protected Dictionary<string, Type> TreeTypes = new Dictionary<string, Type>();

        public string ResourceNamePattern { get; set; }
        public Type ReferenceType { get; set; }

        private readonly Dictionary<string, Corvette> _sharedTrees = new Dictionary<string, Corvette>();

        /// <summary>
        /// Return a list of tree-type names which can be valid parameters two CreateTree
        /// 
        /// </summary>
        /// <returns></returns>
        public List<Corvette> Trees => _sharedTrees.Values.ToList();

        /// <summary>
        /// Create a new tree give a tree name
        /// </summary>
        /// <param name="treeName">A valid tree name</param>
        /// <returns>A tree flyweight of the specified kind or a null if tree name is not valid</returns>
        public Corvette CreateTree(string treeName)
        {
            Corvette tree = null;
            if (_sharedTrees.ContainsKey(treeName))
                tree = _sharedTrees[treeName];
            else if (TreeTypes.ContainsKey(treeName))
            {
                tree = (Corvette) Activator.CreateInstance(TreeTypes[treeName]);
                var resourceName = string.Format(ResourceNamePattern, tree.ResourceName);
                tree.LoadFromResource(resourceName, ReferenceType);
                _sharedTrees.Add(treeName, tree);
            }

            return tree;
        }

        public void Initialize()
        {
            LoadTreeTypes();
            LoadSharedTrees();
        }

        protected virtual void LoadTreeTypes()
        {
            TreeTypes.Add(AnotherBoardleafTree.Name, typeof(AnotherBoardleafTree));
            TreeTypes.Add(C6.Name, typeof(C6));
            TreeTypes.Add(C6_Convertible.Name, typeof(C6_Convertible));
            TreeTypes.Add(C6_Coupe.Name, typeof(C6_Coupe));
            TreeTypes.Add(OakTree.Name, typeof(OakTree));
            TreeTypes.Add(PineTree.Name, typeof(PineTree));
            TreeTypes.Add(SomeBroadleafTree.Name, typeof(SomeBroadleafTree));
        }

        private void LoadSharedTrees()
        {
            var typeEnumerator = TreeTypes.GetEnumerator();
            while (typeEnumerator.MoveNext())
                CreateTree(typeEnumerator.Current.Key);
        }

    }
}
