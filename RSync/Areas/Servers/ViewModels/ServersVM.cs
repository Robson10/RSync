using Prism.Commands;
using Prism.Mvvm;
using RSync.Areas.Servers.Views;
using RSync.Domain.Model;
using RSync.Helpers;
using RSync.Logic;
using System.Collections.Generic;
using System.Linq;

namespace RSync.Areas.Servers.ViewModels
{
    /// <summary>
    /// Servers window view model.
    /// </summary>
    public class ServersVM : BindableBase
    {
        /// <summary>
        /// List of pinned servers to application.
        /// </summary>
        private List<Server> servers;

        /// <summary>
        /// Propertis to list of pinned servers to application.
        /// </summary>
        public List<Server> Servers
        {
            get
            {
                if (servers == null)
                {
                    servers = ServerLogic.GetAllServers(Singleton.UserId).ToList();
                }
                return servers;
            }
            set => SetProperty(ref servers, value);
        }

        /// <summary>
        /// Add new server command.
        /// </summary>
        public DelegateCommand AddServerCmd { get; set; }

        /// <summary>
        /// Delete selected server command.
        /// </summary>
        public DelegateCommand DeleteServerCmd { get; set; }

        /// <summary>
        /// Edit current server command.
        /// </summary>
        public DelegateCommand EditServerCmd { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public ServersVM()
        {
            AddServerCmd = new DelegateCommand(AddServer);
        }

        private void AddServer()
        {
            AddServerV addServerV = new AddServerV();
            if (addServerV.ShowDialog() == true)
            {
                Servers = ServerLogic.GetAllServers(Singleton.UserId).ToList();
            }
        }
    }
}