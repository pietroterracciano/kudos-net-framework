﻿using Kudos.Databases.Controllers;
using Kudos.Databases.Handlers;
using Kudos.Databases.Interfaces.Chains;
using Kudos.Databases.Interfaces;

namespace Kudos.Databases.Chains
{
    public abstract class ABuildableDatabaseChain : DatabaseChain, IBuildableDatabaseChain
    {
        internal ABuildableDatabaseChain(DatabaseChain? o) : base(o) { }

        public abstract IDatabaseHandler BuildHandler();

        public DatabasePoolizedHandler BuildPoolizedHandler(int i)
        {
            return new DatabasePoolizedHandler(this, i);
        }
    }
}