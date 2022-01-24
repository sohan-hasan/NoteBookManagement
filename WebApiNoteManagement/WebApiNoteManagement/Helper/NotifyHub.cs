using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiNoteManagement.Helper
{
    public class NotifyHub : Hub<ITypeHubClient>
    {
    }
}
