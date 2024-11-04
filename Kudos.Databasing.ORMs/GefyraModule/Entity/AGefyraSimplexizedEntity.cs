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
    }
}

