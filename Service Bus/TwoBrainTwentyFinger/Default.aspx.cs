using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.ServiceBus.Management;

namespace TwoBrainTwentyFinger
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ManagementClient managementClient = new ManagementClient(ConnectionManager.ServiceBusConnectionString());
            string queuePath = ConnectionManager.queuePath();
            if(!managementClient.QueueExistsAsync(queuePath).Result)
            {
                QueueDescription queueDescription = new QueueDescription(queuePath)
                {
                    Status = EntityStatus.Active,
                    AutoDeleteOnIdle = TimeSpan.FromMinutes(300),
                    Path = queuePath
                };
                managementClient.CreateQueueAsync(queueDescription);
            }
            string action = null;
            while(action=="Exit")
            {

            }
                       
            //Response.Write(ConnectionManager.ServiceBusConnectionString());
        }
    }
}