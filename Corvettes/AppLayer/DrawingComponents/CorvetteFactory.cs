using System;
using System.Collections.Generic;
using System.Linq;

namespace AppLayer.DrawingComponents
{
    /// <summary>
    /// Corvette CorvetteFactory
    /// 
    /// To use this factory, simply create an instance and then call LoadCorvetteTypes.  Then, use that instance
    /// of the factory to create new creates by calling the CreateCorvette method.
    /// 
    /// This class is serve as the factory in an instance of the Flyweight pattern.
    /// 
    /// This class applies a variation of the CorvetteFactory Method pattern.  It can be extended by specializing
    /// and overriding the LoadCorvetteTypes method.  In most cases, the code for the LoadCorvetteTypes method would
    /// include a call to the base.LoadCorvetteTypes().
    /// 
    /// </summary>
    public class CorvetteFactory
    {
        protected Dictionary<string, Type> CorvetteTypes = new Dictionary<string, Type>();

        public string ResourceNamePattern { get; set; }
        public Type ReferenceType { get; set; }

        private readonly Dictionary<string, Corvette> _sharedCorvettes = new Dictionary<string, Corvette>();

        /// <summary>
        /// Return a list of corvette-type names which can be valid parameters two Createcorvette
        /// 
        /// </summary>
        /// <returns></returns>
        public List<Corvette> Corvettes => _sharedCorvettes.Values.ToList();

        /// <summary>
        /// Create a new corvette give a corvette name
        /// </summary>
        /// <param name="corvetteName">A valid corvette name</param>
        /// <returns>A corvette flyweight of the specified kind or a null if corvette name is not valid</returns>
        public Corvette CreateCorvette(string corvetteName)
        {
            Corvette corvette = null;
            if (_sharedCorvettes.ContainsKey(corvetteName))
                corvette = _sharedCorvettes[corvetteName];
            else if (CorvetteTypes.ContainsKey(corvetteName))
            {
                corvette = (Corvette) Activator.CreateInstance(CorvetteTypes[corvetteName]);
                var resourceName = string.Format(ResourceNamePattern, corvette.ResourceName);
                corvette.LoadFromResource(resourceName, ReferenceType);
                _sharedCorvettes.Add(corvetteName, corvette);
            }

            return corvette;
        }

        public void Initialize()
        {
            LoadCorvetteTypes();
            LoadSharedCorvettes();
        }

        protected virtual void LoadCorvetteTypes()
        {
            CorvetteTypes.Add(C6_Coupe2.Name, typeof(C6_Coupe2));
            CorvetteTypes.Add(C6_Convertible.Name, typeof(C6_Convertible));
            CorvetteTypes.Add(C6_Coupe.Name, typeof(C6_Coupe));
            CorvetteTypes.Add(C7_Convertible.Name, typeof(C7_Convertible));
            CorvetteTypes.Add(C7_Coupe.Name, typeof(C7_Coupe));
            CorvetteTypes.Add(C7_Coupe2.Name, typeof(C7_Coupe2));
            CorvetteTypes.Add(C7_Z06.Name, typeof(C7_Z06));
            CorvetteTypes.Add(C7_ZR1.Name, typeof(C7_ZR1));
            CorvetteTypes.Add(C8_Convertible.Name, typeof(C8_Convertible));
        }

        private void LoadSharedCorvettes()
        {
            var typeEnumerator = CorvetteTypes.GetEnumerator();
            while (typeEnumerator.MoveNext())
                CreateCorvette(typeEnumerator.Current.Key);
        }

    }
}
