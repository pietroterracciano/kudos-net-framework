using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Kudos.Databasing.Descriptors;
using Kudos.Databasing.Interfaces;
using Kudos.Databasing.ORMs.GefyraModule.Entities;
using Kudos.Utils;

namespace Kudos.Databasing.ORMs.GefyraModule.Descriptors
{
    internal class GefyraSocket
    {
        #region ... static ...

        private static readonly SemaphoreSlim
            __ss;

        private static readonly Dictionary<GefyraTableDescriptor, GefyraSocket>
            __d;

        static GefyraSocket()
        {
            __ss = new SemaphoreSlim(1);
            __d = new Dictionary<GefyraTableDescriptor, GefyraSocket>();
        }

        #region internal static async Task<GefyraSocket> GetAsync<...>(...)

        internal static async Task<GefyraSocket?> GetAsync<T>
        (
            IDatabaseHandler? dh
        )
        {
            GefyraTableDescriptor? gtd;
            GefyraTableDescriptor.Get<T>(out gtd);
            return await GetAsync(gtd, dh);
        }

        internal static async Task<GefyraSocket?> GetAsync
        (
            Type? t,
            IDatabaseHandler? dh
        )
        {
            GefyraTableDescriptor? gtd;
            GefyraTableDescriptor.Get(ref t, out gtd);
            return await GetAsync(gtd, dh);
        }

        internal static async Task<GefyraSocket?> GetAsync
        (
            String? sn,
            IDatabaseHandler? dh
        )
        {
            GefyraTableDescriptor? gtd;
            GefyraTableDescriptor.Get(ref sn, out gtd);
            return await GetAsync(gtd, dh);
        }

        internal static async Task<GefyraSocket?> GetAsync
        (
            String? ssn,
            String? sn,
            IDatabaseHandler? dh
        )
        {
            GefyraTableDescriptor? gtd;
            GefyraTableDescriptor.Get(ref ssn, ref sn, out gtd);
            return await GetAsync(gtd, dh);
        }

        internal static async Task<GefyraSocket?> GetAsync
        (
            GefyraTable? gt,
            IDatabaseHandler? dh
        )
        {
            return
                gt != null
                    ? await GetAsync(gt.Descriptor, dh)
                    : null;
        }

        internal static async Task<GefyraSocket?> GetAsync
        (
            GefyraTableDescriptor? gtd,
            IDatabaseHandler? dh
        )
        {
            if (dh == null || gtd == null)
                return null;

            await SemaphoreUtils.WaitSemaphoreAsync(__ss);

            GefyraSocket? gs;

            if (__d.TryGetValue(gtd, out gs))
            {
                SemaphoreUtils.ReleaseSemaphore(__ss);
                return gs;
            }

            if (!dh.IsConnectionOpened())
            {
                await dh.OpenConnectionAsync();

                if (!dh.IsConnectionOpened())
                {
                    SemaphoreUtils.ReleaseSemaphore(__ss);
                    return null;
                }
            }

            DatabaseTableDescriptor?
                dtd =
                    await
                        dh.GetTableDescriptorAsync
                        (
                            gtd.SchemaName,
                            gtd.Name
                        );

            __d[gtd] = gs = new GefyraSocket(ref gtd, ref dtd);

            SemaphoreUtils.ReleaseSemaphore(__ss);

            return gs;
        }

        #endregion

        #endregion

        internal readonly GefyraTableDescriptor TableDescriptor;

        internal readonly Boolean HasDatabaseTableDescriptor;
        internal readonly DatabaseTableDescriptor? DatabaseTableDescriptor;

        internal GefyraSocket
        (
            ref GefyraTableDescriptor gtd,
            ref DatabaseTableDescriptor? dtd
        )
        {
            TableDescriptor = gtd;
            HasDatabaseTableDescriptor = (DatabaseTableDescriptor = dtd) != null;
        }

        internal void GetColumnDescriptor(ref DatabaseColumnDescriptor? dcd, out GefyraColumnDescriptor? gcd)
        {
            if (dcd == null) { gcd = null; return; }
            String s = dcd.Name;
            GetColumnDescriptor(ref s, out gcd);
        }

        internal void GetColumnDescriptor(ref MemberInfo? mi, out GefyraColumnDescriptor? gcd)
        {
            TableDescriptor.GetColumnDescriptor(ref mi, out gcd);
        }

        internal void GetColumnDescriptor(ref String? s, out GefyraColumnDescriptor? gcd)
        {
            TableDescriptor.GetColumnDescriptor(ref s, out gcd);
        }

        internal void GetColumnsDescriptors(out GefyraColumnDescriptor[]? gcda)
        {
            TableDescriptor.GetColumnsDescriptors(out gcda);
        }

        internal void GetDatabaseColumnDescriptor(ref GefyraColumn? gc, out DatabaseColumnDescriptor? dcd)
        {
            if (gc == null) { dcd = null; return; }
            GefyraColumnDescriptor gcd = gc.Descriptor;
            GetDatabaseColumnDescriptor(ref gcd, out dcd);
        }

        internal void GetDatabaseColumnDescriptor(ref GefyraColumnDescriptor? gcd, out DatabaseColumnDescriptor? dcd)
        {
            if (gcd == null) { dcd = null; return; }
            String s = gcd.Name;
            GetDatabaseColumnDescriptor(ref s, out dcd);
        }

        internal void GetDatabaseColumnDescriptor(ref String? s, out DatabaseColumnDescriptor? dcd)
        {
            if (s == null || !HasDatabaseTableDescriptor) { dcd = null; return; }
            dcd = DatabaseTableDescriptor.GetColumnDescriptor(s);
        }

        internal void GetDatabaseColumnsDescriptors(out DatabaseColumnDescriptor[]? dcda)
        {
            if (!HasDatabaseTableDescriptor) { dcda = null; return; }
            dcda = DatabaseTableDescriptor.GetColumnsDescriptors();
        }
    }
}

