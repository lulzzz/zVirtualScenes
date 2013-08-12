﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zvs.Entities;
using System.Data.Entity;

namespace zvs.Processor
{
    public class DeviceTypeBuilder : AdapterBuilder
    {
        protected zvsContext Context { get; set; }
        public DeviceTypeBuilder(zvsAdapter zvsAdapter, Core core, zvsContext context)
            : base(zvsAdapter, core)
        {
            Context = context;
        }

        public async Task<int> RegisterAsync(DeviceType deviceType)
        {
            //Does device type exist? 
            var existing_dt = await Context.DeviceTypes.Include(o => o.Commands)
                .FirstOrDefaultAsync(o =>
                o.Adapter.AdapterGuid == Adapter.AdapterGuid
                && o.UniqueIdentifier == deviceType.UniqueIdentifier);

            if (existing_dt == null)
            {
                existing_dt = deviceType;
                var adapter = await Context.Adapters.FirstOrDefaultAsync(o => o.AdapterGuid == Adapter.AdapterGuid);

                if (adapter != null)
                    adapter.DeviceTypes.Add(existing_dt);
            }
            else
            {
                existing_dt.Name = deviceType.Name;
                existing_dt.ShowInList = deviceType.ShowInList;

                foreach (DeviceTypeCommand dtc in deviceType.Commands)
                {
                    DeviceTypeCommand existing_dtc = await Context.DeviceTypeCommands
                        .Include(o=> o.Options)
                        .SingleOrDefaultAsync(o =>
                            o.DeviceTypeId == existing_dt.Id &&
                            o.UniqueIdentifier == dtc.UniqueIdentifier);

                    if (existing_dtc == null)
                    {
                        existing_dt.Commands.Add(dtc);
                    }
                    else
                    {
                        existing_dtc.Name = dtc.Name;
                        existing_dtc.Help = dtc.Help;
                        existing_dtc.CustomData1 = dtc.CustomData1;
                        existing_dtc.CustomData2 = dtc.CustomData2;
                        existing_dtc.ArgumentType = dtc.ArgumentType;
                        existing_dtc.Description = dtc.Description;

                        Context.CommandOptions.RemoveRange(existing_dtc.Options.ToList());
                        existing_dtc.Options.Clear();
                        dtc.Options.ToList().ForEach(o => existing_dtc.Options.Add(o));
                    }
                }
            }

            var result = await Context.TrySaveChangesAsync();
            if (result.HasError)
                Core.log.Error(result.Message);

            return existing_dt.Id;
        }

    }

    public class CommandOptionComparer : IEqualityComparer<CommandOption>
    {
        public bool Equals(CommandOption x, CommandOption y)
        {
            return x.Name == y.Name;
        }

        public int GetHashCode(CommandOption obj)
        {
            return 37 * obj.Name.GetHashCode() + 19 * obj.Name.GetHashCode();
        }
    }
}