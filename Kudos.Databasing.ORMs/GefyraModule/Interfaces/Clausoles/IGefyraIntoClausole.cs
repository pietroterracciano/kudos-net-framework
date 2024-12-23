﻿using System;
using Kudos.Databasing.ORMs.GefyraModule.Interfaces.Builders;
using Kudos.Databasing.ORMs.GefyraModule.Interfaces.Entities;


namespace Kudos.Databasing.ORMs.GefyraModule.Interfaces.Clausoles
{
	public interface IGefyraIntoClausole
	{
        IGefyraIntoClausoleBuilder Into(IGefyraTable? gt, IGefyraColumn? gc, params IGefyraColumn?[]? gca);
    }
}