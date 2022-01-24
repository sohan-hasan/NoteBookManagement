using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiNoteManagement.Helper
{
    public interface ITypeHubClient
    {
        Task BroadcastMessage(Message message);
    }
}
