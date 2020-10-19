using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;

namespace core
{
    [DebuggerDisplay("{" + nameof(_internalCollection) + "}")]
    [CollectionDataContract]
    [JsonObject]
    public class StartupCollection
        : IList<StartupParameter>
    {
        [JsonProperty(PropertyName = "Startup Parameters")]
        private List<StartupParameter> _internalCollection;

        [JsonConstructor]
        public StartupCollection()
        {
            _internalCollection = new List<StartupParameter>();
        }

        ~StartupCollection()
        {
            
        }

        [JsonIgnore]
        public int Count => this._internalCollection.Count;

        [JsonIgnore]
        public bool IsReadOnly => false;

        public void Add(StartupParameter item)
        {
            if (!Contains(item))
                this._internalCollection.Add(item);
            else
                this[IndexOf(item)] = item;
        }

        public void Clear()
        {
            this._internalCollection.Clear();
        }

        public bool Contains(StartupParameter item)
        {
            return this._internalCollection.Contains(item);
        }

        public void CopyTo(StartupParameter[] array, int arrayIndex)
        {
            this._internalCollection.CopyTo(array, arrayIndex);
        }

        public IEnumerator<StartupParameter> GetEnumerator()
        {
            return this._internalCollection.GetEnumerator();
        }

        public int IndexOf(StartupParameter item)
        {
            return this._internalCollection.IndexOf(item);
        }

        public void Insert(int index, StartupParameter item)
        {
            this._internalCollection.Insert(index, item);
        }

        public bool Remove(StartupParameter item)
        {
            return this._internalCollection.Remove(item);
        }

        public void RemoveAt(int index)
        {
            this._internalCollection.RemoveAt(index);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)this._internalCollection).GetEnumerator();
        }
        public StartupParameter this[int index] { get => _internalCollection[index]; set => _internalCollection[index] = value; }
        public StartupParameter this[StartupParameter val]
        {
            get
            {
                var i = IndexOf(val);

                if (i == -1) return default;

                return this[i];
            }
            set
            {
                var i = IndexOf(val);

                if(i != -1)
                {
                    this[i] = value;
                }
            }
        }

    }    
}
