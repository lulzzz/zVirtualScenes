﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace zvs.Entities
{
    [Table("SceneCommands", Schema = "ZVS")]
    public partial class SceneCommand : IIdentity
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        //No actual navigational property here
        private StoredCommand _StoredCommand;
        public virtual StoredCommand StoredCommand
        {
            get
            {
                return _StoredCommand;
            }
            set
            {
                if (value != _StoredCommand)
                {
                    _StoredCommand = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public int SceneId { get; set; }
        private Scene _Scene;
        public virtual Scene Scene
        {
            get
            {
                return _Scene;
            }
            set
            {
                if (value != _Scene)
                {
                    _Scene = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private int? _SortOrder;
        public int? SortOrder
        {
            get
            {
                return _SortOrder;
            }
            set
            {
                if (value != _SortOrder)
                {
                    _SortOrder = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
