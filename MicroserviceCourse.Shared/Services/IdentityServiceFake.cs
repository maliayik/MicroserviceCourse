using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroserviceCourse.Shared.Services
{
    public class IdentityServiceFake : IIdentityService
    {
        public Guid GetUserId => Guid.Parse("a47e742c-301a-47c0-b61d-ae6ac0a01f54");

        public string UserName => "Ahmet";
    }
}
