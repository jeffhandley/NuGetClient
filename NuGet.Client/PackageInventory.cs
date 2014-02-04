using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NuGet.Client
{
    public class PackageInventory : IList<IPackageIdentity>
    {
        private List<IPackageIdentity> _packagesInstalled;

        public PackageInventory()
        {
            this._packagesInstalled = new List<IPackageIdentity>();
        }

        public int IndexOf(IPackageIdentity item)
        {
            throw new NotImplementedException();
        }

        public void Insert(int index, IPackageIdentity item)
        {
            throw new NotImplementedException();
        }

        public void RemoveAt(int index)
        {
            throw new NotImplementedException();
        }

        public IPackageIdentity this[int index]
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public void Add(IPackageIdentity item)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public bool Contains(IPackageIdentity item)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(IPackageIdentity[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public int Count
        {
            get { throw new NotImplementedException(); }
        }

        public bool IsReadOnly
        {
            get { throw new NotImplementedException(); }
        }

        public bool Remove(IPackageIdentity item)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<IPackageIdentity> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
