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

        private readonly List<CorvettePlacement> _corvettePlacements = new List<CorvettePlacement>();
        private readonly object _myLock = new object();

        protected bool IsDirty { get; private set; } = true;

        public CorvetteFactory CorvetteFactory { get; set; }
        public string Filename { get; set; }

        public bool Draw(Graphics graphics, bool redrawEvenIfNotDirty = false)
        {
            lock (_myLock)
            {
                if (!IsDirty && !redrawEvenIfNotDirty) return false;
                graphics.Clear(Color.White);
                foreach (var placement in _corvettePlacements)
                    placement.Corvette.Render(graphics, placement.ExtrinsicState);
                IsDirty = false;
            }
            return true;
        }

        internal List<CorvettePlacement> GetCorvettePlacements()
        {
            lock (_myLock)
            {
                return new List<CorvettePlacement>(_corvettePlacements);
            }
        }

        internal void Clear()
        {
            lock (_myLock)
            {
                _corvettePlacements.Clear();
                IsDirty = true;
            }
        }

        internal void LoadFromStream(Stream stream)
        {
            var extrinsicStates = JsonSerializer.ReadObject(stream) as List<CorvetteExtrinsicState>;
            if (extrinsicStates == null) return;

            lock (_myLock)
            {
                _corvettePlacements.Clear();
                foreach (var extrinsicState in extrinsicStates)
                {
                    var corvette = CorvetteFactory.CreateCorvette(extrinsicState.CorvetteType);
                    _corvettePlacements.Add(new CorvettePlacement(corvette, extrinsicState));
                }
                IsDirty = true;
            }

        }

        internal void SaveToStream(Stream stream)
        {
            var extrinsicStates = new List<CorvetteExtrinsicState>();
            lock (_myLock)
            {
                extrinsicStates.AddRange(_corvettePlacements.Select(corvette => corvette.ExtrinsicState));
            }
            JsonSerializer.WriteObject(stream, extrinsicStates);
        }

        internal void SaveScreanshotStream(Stream stream)
        {
            var extrinsicStates = new List<CorvetteExtrinsicState>();
            lock (_myLock)
            {
                extrinsicStates.AddRange(_corvettePlacements.Select(corvette => corvette.ExtrinsicState));
            }
            Image.FromStream(stream, extrinsicStates);
        }

        internal void Add(CorvettePlacement corvettePlacement)
        {
            if (corvettePlacement == null) return;
            lock (_myLock)
            {
                _corvettePlacements.Add(corvettePlacement);
                IsDirty = true;
            }
        }

        internal CorvettePlacement FindCorvetteAtPosition(Point location)
        {
            CorvettePlacement result;
            lock (_myLock)
            {
                result = _corvettePlacements.FindLast(placement => location.X >= placement.ExtrinsicState.Location.X &&
                                              location.X < placement.ExtrinsicState.Location.X + placement.ExtrinsicState.Size.Width &&
                                              location.Y >= placement.ExtrinsicState.Location.Y &&
                                              location.Y < placement.ExtrinsicState.Location.Y + placement.ExtrinsicState.Size.Height);
            }
            return result;
        }

        public void DeleteCorvette(CorvettePlacement corvettePlacement)
        {
            lock (_myLock)
            {
                _corvettePlacements.Remove(corvettePlacement);
                IsDirty = true;
            }
        }

        /// <summary>
        /// Changes the selection state of corvette at the specified position, but eitehr toggling the
        /// current value or by setting it to a specific value.
        /// </summary>
        /// <param name="location">Location at which to look for a true.</param>
        /// <param name="forceToSelectionState">If null, then the selection state of the corvette at the
        /// specified position is toggled.  Otherwise, the selection state is set to this value.</param>
        /// <returns>The previous state of the corvette at the specified position; null if no corvette exists
        /// at that position</returns>
        internal bool? ChangeSelectionAtPosition(Point location, bool? forceToSelectionState = null)
        {
            var corvette = FindCorvetteAtPosition(location);

            if (corvette == null) return null;

            var oldSelectionState = corvette.ExtrinsicState.IsSelected;
            corvette.ExtrinsicState.IsSelected = !oldSelectionState;
            IsDirty = true;

            return oldSelectionState;
        }

        /// <summary>
        /// Changes the selection state of corvette at the specified position, but eitehr toggling the
        /// current value or by setting it to a specific value.
        /// </summary>
        /// <param name="placement">The placement whose selection should be changed.</param>
        /// <param name="forceToSelectionState">If null, then the selection state of the corvette at the
        /// specified position is toggled.  Otherwise, the selection state is set to this value.</param>
        /// <returns>The previous state of the corvette at the specified position; null if no corvette exists
        /// at that position</returns>
        internal void ChangeSelection(CorvettePlacement placement, bool? forceToSelectionState = null)
        {
            lock (_myLock)
            {
                if (placement == null || !_corvettePlacements.Contains(placement)) return;

                placement.ExtrinsicState.IsSelected = !placement.ExtrinsicState.IsSelected;
                IsDirty = true;
            }
        }

        internal List<CorvettePlacement> DeleteAllSelected()
        {
            List<CorvettePlacement> placementsDeleted;

            lock (_myLock)
            {
                placementsDeleted = _corvettePlacements.FindAll(t => t.ExtrinsicState.IsSelected);
                _corvettePlacements.RemoveAll(t => t.ExtrinsicState.IsSelected);
                IsDirty = true;
            }

            return placementsDeleted;
        }

        internal List<CorvettePlacement> DeselectAll()
        {
            var selectedCorvettes = new List<CorvettePlacement>();
            lock (_myLock)
            {
                lock (_myLock)
                {
                    foreach (var t in _corvettePlacements.Where(t => t.ExtrinsicState.IsSelected))
                    {
                        selectedCorvettes.Add(t);
                        t.ExtrinsicState.IsSelected = false;
                    }
                    IsDirty = true;
                }
                return selectedCorvettes;
            }
        }

    }
}
