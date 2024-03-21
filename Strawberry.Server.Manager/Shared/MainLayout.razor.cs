using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;

namespace Strawberry.Server.Manager.Shared
{
    public partial class MainLayout : IDisposable
    {
        [Inject] IJSRuntime JSRuntime { get; set; }
        [Inject] NavigationManager NavigationManager { get; set; }

        private Timer LogoutTimer { get; set; }

        protected override void OnAfterRender(bool firstRender)
        {
            if (firstRender)
            {
                this.LogoutTimer = new Timer(1000 * 60 * 30);
                this.LogoutTimer.AutoReset = false;
                this.LogoutTimer.Elapsed += RunLogout;

                this.JSRuntime.InvokeVoidAsync("timecheck", DotNetObjectReference.Create(this));
            }
        }

        [JSInvokable]
        public void ResetTimeDelay()
        {
            this.LogoutTimer.Stop();
            this.LogoutTimer.Start();
        }

        private void RunLogout(object sender, ElapsedEventArgs e)
        {
            NavigationManager.NavigateTo("logout", true);
        }

        public void Dispose()
        {
            if (this.LogoutTimer != null)
            {
                this.LogoutTimer.Dispose();
                this.LogoutTimer = null;
            }
        }
    }
}
