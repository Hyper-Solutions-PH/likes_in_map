using Map.Common;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Map.Hubs
{
    public class MapHub : Hub
    {
        #region Data Members

        static List<LocationDetails> LastLocations = new List<LocationDetails>();
        #endregion

        #region SendLocation
        public async Task Send(object location)
        {
            AddLocationInCache(location);
            await Clients.All.SendAsync("heart", location);
        }
        #endregion

        #region LastLocations
        public override async Task OnConnectedAsync()
        {
            await Clients.Caller.SendAsync("last_hearts", LastLocations);
            await base.OnConnectedAsync();
        }
        #endregion

        #region AddInCache
        private void AddLocationInCache(object location)
        {
            LastLocations.Add(new LocationDetails { Location = location});
            if (LastLocations.Count > 1000)
                LastLocations.RemoveAt(0);
        }
        #endregion
    }
}