using System;
using Kudos.Databasing.ORMs.GefyraModule.Interfaces.Entities;
using Kudos.Types;
using System.Text;

namespace Kudos.Databasing.ORMs.GefyraModule.Entity
{
    public abstract class
        AGefyraSimplexizedEntity<EntityType>
    :
        TokenizedObject,
        IGefyraSimplexizedEntity
    where
        EntityType : AGefyraSimplexizedEntity<EntityType>
    {
        #region IsInvalid

        public abstract Boolean IsInvalid { get; }

        #endregion

        #region IsIgnored

        public abstract Boolean IsIgnored { get; }

        #endregion
    }
}

