using System;
using Kudos.Databasing.ORMs.GefyraModule.Descriptors;
using Kudos.Databasing.ORMs.GefyraModule.Entities;

namespace Kudos.Databasing.ORMs.GefyraModule.Types
{
	internal class GefyraExpressionResult
	{
        private static GefyraColumnDescriptor?
            __gcdNull;
        private static GefyraTableDescriptor?
            __gtdNull;
        private static Object?
            __vNull;

        static GefyraExpressionResult()
        {
            __gcdNull = null;
            __gtdNull = null;
            __vNull = null;
        }

        internal readonly Boolean HasTableDescriptor;
        internal readonly GefyraTableDescriptor? TableDescriptor;

        internal readonly Boolean HasColumnDescriptor;
        internal readonly GefyraColumnDescriptor? ColumnDescriptor;

        internal readonly Boolean HasValue;
        internal readonly Object? Value;
    }
}

