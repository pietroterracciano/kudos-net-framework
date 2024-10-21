
using System;
using System.Reflection;
using Google.Protobuf.Reflection;
using Kudos.Databasing.ORMs.GefyraModule.Entities;
using Kudos.Databasing.ORMs.GefyraModule.Interfaces.Descriptors;
using Kudos.Databasing.ORMs.GefyraModule.Interfaces.Entities;

namespace Kudos.Databasing.ORMs.GefyraModule.Entity
{
	public sealed class
		GefyraJoin
	:
		AGefyraSimplexizedEntity<GefyraJoin>,
        IGefyraJoin
	{
        #region DeclaringColumn

        public IGefyraColumn DeclaringColumn { get; private set; }

        #endregion

        #region DestinatingTable

        public IGefyraTableDescriptor DestinatingTable { get; private set; }

        #endregion

        private readonly String _shk;

        #region HashKey

        public override string HashKey { get { return _shk; } }

        #endregion

        internal GefyraJoin(ref String shk, ref GefyraColumn gc, ref GefyraTable gt)
        {
            _shk = shk;
            DeclaringColumn = gc;
            DestinatingTable = gt;
        }

    }
}

