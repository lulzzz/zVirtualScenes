﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zvs.Entities;

namespace zvs.Processor.Backup
{
    public enum Command_Types
    {
        Unknown = 0,
        Builtin = 1,
        Device = 2,
        DeviceType = 3, 
        JavaScript = 4
    }

    [Serializable]
    public class StoredCMDBackup
    {
        public Command_Types CommandType;
        public string UniqueIdentifier;
        public string Argument;
        public int NodeNumber;

        public static implicit operator StoredCMDBackup(StoredCommand m)
        {
            StoredCMDBackup bcmd = new StoredCMDBackup();

            if (m.Command is BuiltinCommand)
                bcmd.CommandType = Command_Types.Builtin;
            else if (m.Command is DeviceTypeCommand)
                bcmd.CommandType = Command_Types.DeviceType;
            else if (m.Command is DeviceCommand)
                bcmd.CommandType = Command_Types.Device;
            else if (m.Command is JavaScriptCommand)
                bcmd.CommandType = Command_Types.JavaScript;

            if (m.Command != null)
                bcmd.UniqueIdentifier = m.Command.UniqueIdentifier;

            if (m.Device != null)
                bcmd.NodeNumber = m.Device.NodeNumber;

            bcmd.Argument = m.Argument;

            return bcmd;
        }

        /// <summary>
        /// Return null if failed to restore command.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="backupStoredCMD"></param>
        /// <returns></returns>
        public static StoredCommand RestoreStoredCommand(zvsContext context, StoredCMDBackup backupStoredCMD)
        {
            if (backupStoredCMD.CommandType == Command_Types.Device ||
                backupStoredCMD.CommandType == Command_Types.DeviceType)
            {
                Device d = context.Devices.FirstOrDefault(o => o.NodeNumber == backupStoredCMD.NodeNumber);
                if (d == null)
                    return null;

                Command c = null;
                if (backupStoredCMD.CommandType == Command_Types.Device)
                    c = d.Commands.FirstOrDefault(o => o.UniqueIdentifier == backupStoredCMD.UniqueIdentifier);
                if (backupStoredCMD.CommandType == Command_Types.DeviceType)
                    c = d.Type.Commands.FirstOrDefault(o => o.UniqueIdentifier == backupStoredCMD.UniqueIdentifier);

                if (c == null)
                    return null;

                StoredCommand sc = new StoredCommand();
                sc.Device = d;
                sc.Argument = backupStoredCMD.Argument;
                sc.Command = c;
                context.StoredCommands.Add(sc);
                context.SaveChanges();
                return sc;
            }
            else if (backupStoredCMD.CommandType == Command_Types.Builtin ||
                     backupStoredCMD.CommandType == Command_Types.JavaScript)
            {
                Command c = null;
                if (backupStoredCMD.CommandType == Command_Types.Builtin)
                    c = context.BuiltinCommands.FirstOrDefault(o => o.UniqueIdentifier == backupStoredCMD.UniqueIdentifier);

                if (backupStoredCMD.CommandType == Command_Types.JavaScript)
                    c = context.JavaScriptCommands.FirstOrDefault(o => o.UniqueIdentifier == backupStoredCMD.UniqueIdentifier);
                
                if (c == null)
                    return null;

                StoredCommand sc = new StoredCommand();
                sc.Argument = backupStoredCMD.Argument;
                sc.Command = c;
                context.StoredCommands.Add(sc);
                context.SaveChanges();
                return sc;
            }
            
            return null;
        }
    }
}