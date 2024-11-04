using System;
namespace Kudos.Databasing.ORMs.GefyraModule.Interfaces.Entities.Actions
{
    public interface
		IGefyraEntityGetColumnsAction

    {
        public IGefyraColumn[]? GetColumns();
    }
}

