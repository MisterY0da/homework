using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iMessengerCoreAPI.Models;

namespace iMessengerCoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RGDialogsClientsController : ControllerBase
    {
        [HttpPost]
        public Guid GetDialogId(List<Guid> clientGuids)
        {
            var rGDialogsClients = new RGDialogsClients();
            var dialogsClientsList = rGDialogsClients.Init();

            var dialogIds = new List<Guid>();

            foreach (var dc in dialogsClientsList)
            {
                foreach (var clientGuid in clientGuids)
                {
                    if (dc.IDClient.Equals(clientGuid) && dialogIds.Contains(dc.IDRGDialog) == false)
                    {
                        dialogIds.Add(dc.IDRGDialog);
                        break;
                    }

                    else if (dc.IDClient.Equals(clientGuid) == false && dialogIds.Contains(dc.IDRGDialog) == true)
                    {
                        dialogIds.Remove(dc.IDRGDialog);
                    }
                }
            }

            if (dialogIds.Count == 1)
            {
                return dialogIds[0];
            }

            else
            {
                return Guid.Empty;
            }
        }
    }
}
