using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;

namespace AppLayer.DrawingComponents
{
    // NOTE: This class at least one  problem that is hurting coupling and lowering cohesion

    public class Drawing
    {
        private static readonly DataContractJsonSerializer JsonSerializer = new DataContractJsonSerializer(typeof(List<CorvetteExtrinsicState>));

        private readonly List<TreePlacement> _treePlacements = new List<TreePlacement>();
        private readonly object _myLock = new object();

        protected bool IsDirty { get; private set; } = true;

        public TreeFactory TreeFactory { get; set; }
        public string Filename { get; set; }

        public bool Draw(Graphics graphics, bool redrawEvenIfNotDirty = false)
        {
            lock (_myLock)
            {
                if (!IsDirty && !redrawEvenIfNotDirty) return false;
                graphics.Clear(Color.White);
                foreach (var placement in _treePlacements)
                    placement.Tree.Render(graphics, placement.ExtrinsicState);
                IsDirty = false;
            }
            return true;
        }

        internal List<TreePlacement> GetTreePlacements()
        {
            lock (_myLock)
            {
                return new List<TreePlacement>(_treePlacements);
            }
        }

        internal void Clear()
        {
            lock (_myLock)
            {
                _treePlacements.Clear();
                IsDirty = true;
            }
        }

        internal void LoadFromStream(Stream stream)
        {
            var extrinsicStates = JsonSerializer.ReadObject(stream) as List<CorvetteExtrinsicState>;
            if (extrinsicStates == null) return;

            lock (_myLock)
            {
                _treePlacements.Clear();
                foreach (var extrinsicState in extrinsicStates)
                {
                    var tree = TreeFactory.CreateTree(extrinsicState.CorvetteType);
                    _treePlacements.Add(new TreePlacement(tree, extrinsicState));
                }
                IsDirty = true;
            }

        }

        internal void SaveToStream(Stream stream)
        {
            var extrinsicStates = new List<CorvetteExtrinsicState>();
            lock (_myLock)
            {
                extrinsicStates.AddRange(_treePlacements.Select(tree => tree.ExtrinsicState));
            }
            JsonSerializer.WriteObject(stream, extrinsicStates);
        }

        internal void Add(TreePlacement treePlacement)
        {
            if (treePlacement == null) return;
            lock (_myLock)
            {
                _treePlacements.Add(treePlacement);
                IsDirty = true;
            }
        }

        internal TreePlacement FindTreeAtPosition(Point location)
        {
            TreePlacement result;
            lock (_myLock)
            {
                result = _treePlacements.FindLast(placement => location.X >= placement.ExtrinsicState.Location.X &&
                                              location.X < placement.ExtrinsicState.Location.X + placement.ExtrinsicState.Size.Width &&
                                              location.Y >= placement.ExtrinsicState.Location.Y &&
                                              location.Y < placement.ExtrinsicState.Location.Y + placement.ExtrinsicState.Size.Height);
            }
            return result;
        }

        public void DeleteTree(TreePlacement treePlacement)
        {
            lock (_myLock)
            {
                _treePlacements.Remove(treePlacement);
                IsDirty = true;
            }
        }

        /// <summary>
        /// Changes the selection state of tree at the specified position, but eitehr toggling the
        /// current value or by setting it to a specific value.
        /// </summary>
        /// <param name="location">Location at which to look for a true.</param>
        /// <param name="forceToSelectionState">If null, then the selection state of the tree at the
        /// specified position is toggled.  Otherwise, the selection state is set to this value.</param>
        /// <returns>The previous state of the tree at the specified position; null if no tree exists
        /// at that position</returns>
        internal bool? ChangeSelectionAtPosition(Point location, bool? forceToSelectionState = null)
        {
            var tree = FindTreeAtPosition(location);

            if (tree == null) return null;

            var oldSelectionState = tree.ExtrinsicState.IsSelected;
            tree.ExtrinsicState.IsSelected = !oldSelectionState;
            IsDirty = true;

            return oldSelectionState;
        }

        /// <summary>
        /// Changes the selection state of tree at the specified position, but eitehr toggling the
        /// current value or by setting it to a specific value.
        /// </summary>
        /// <param name="placement">The placement whose selection should be changed.</param>
        /// <param name="forceToSelectionState">If null, then the selection state of the tree at the
        /// specified position is toggled.  Otherwise, the selection state is set to this value.</param>
        /// <returns>The previous state of the tree at the specified position; null if no tree exists
        /// at that position</returns>
        internal void ChangeSelection(TreePlacement placement, bool? forceToSelectionState = null)
        {
            lock (_myLock)
            {
                if (placement == null || !_treePlacements.Contains(placement)) return;

                placement.ExtrinsicState.IsSelected = !placement.ExtrinsicState.IsSelected;
                IsDirty = true;
            }
        }

        internal List<TreePlacement> DeleteAllSelected()
        {
            List<TreePlacement> placementsDeleted;

            lock (_myLock)
            {
                placementsDeleted = _treePlacements.FindAll(t => t.ExtrinsicState.IsSelected);
                _treePlacements.RemoveAll(t => t.ExtrinsicState.IsSelected);
                IsDirty = true;
            }

            return placementsDeleted;
        }

        internal List<TreePlacement> DeselectAll()
        {
            var selectedTrees = new List<TreePlacement>();
            lock (_myLock)
            {
                lock (_myLock)
                {
                    foreach (var t in _treePlacements.Where(t => t.ExtrinsicState.IsSelected))
                    {
                        selectedTrees.Add(t);
                        t.ExtrinsicState.IsSelected = false;
                    }
                    IsDirty = true;
                }
                return selectedTrees;
            }
        }

    }
}
