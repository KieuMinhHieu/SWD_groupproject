using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModel
{
    public  class JwtDTO
    {
        public string UserId { get; set; } = default!;
        public string RoleName { get; set; } = default!;    
    }
}
